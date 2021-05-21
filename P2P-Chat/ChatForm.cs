using P2P_Chat.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2P_Chat
{
    public partial class ChatForm : Form
    {
        public static readonly Color blue = Color.FromArgb(0, 122, 204);
        public static readonly Color red = Color.FromArgb(164, 22, 26);
        public static readonly Color green = Color.FromArgb(8, 28, 21);
        public static readonly Color grey = Color.FromArgb(73, 80, 87);
        public static readonly Color purple = Color.FromArgb(64, 0, 64);

        public static ChatForm instance;
        private string appVersion;
        private string appName;
        private Client client;
        private Messages queue;
        public string currentName;
        public bool saveOnExit;
        private List<string> userList;
        private string lastMessage = "";

        public ChatForm()
        {
            InitializeComponent();
            userList = new List<string>();
            queue = new Messages();
            queue.MessageQueued += new EventHandler(MessageQueuedEvent);
            client = new Client();
            client.Loading();
            client.ReceivedMessage += new EventHandler<MessageEvent>(MessageReceivedEvent);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            appName = fileVersionInfo.FileDescription;
            appVersion = fileVersionInfo.ProductVersion;
            appVersion = appVersion.Remove(appVersion.Length - 2);

            RichChat.AppendText($"[{DateTime.Now:HH:mm:ss}] <HOST> Welcome to P2P-Chat ({appVersion})\n");
            RichChat.AppendText($"[{DateTime.Now:HH:mm:ss}] <HOST> Type '/help' to show command list");

            currentName = client.Handle;
            saveOnExit = client.SaveOnQuitConfig;

            client.Transmit("", Net.Message.JOIN);
            this.Text = appName + " (" + currentName + "@" + appVersion + ")";
            LabelTitle.Text = this.Text;

            ChatForm.instance = this;
        }

        #region EVENT FORMS

        private void TextMessageBox_Enter(object sender, EventArgs e)
        {
            MessageBar.LineColor = Color.FromArgb(64, 0, 64);
        }

        private void TextMessageBox_Leave(object sender, EventArgs e)
        {
            MessageBar.LineColor = Color.DarkGray;
        }

        private void TextMessageBox_TextChanged(object sender, EventArgs e)
        {
            LeftCharLabel.Text = (140 - TextMessageBox.Text.Length).ToString();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveOnExit)
            {
                FileStream fs = new FileStream($"{Environment.CurrentDirectory}\\{DateTime.Now:yyyy-MM-dd}_log.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, ASCIIEncoding.UTF8);
                sw.Write(RichChat.Text + "\r\n");
                sw.Close();
                fs.Close();
            }
            client.Transmit(string.Empty, Net.Message.QUIT);
        }

        private void TickerTime_Tick(object sender, EventArgs e)
        {
            ClockLabel.Text = $"{DateTime.Now:HH:mm:ss}";
        }

        public void PingTimer_Tick(object sender = null, EventArgs e = null)
        {
            client.Transmit(string.Empty, Net.Message.PING);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int timestamp = (int)t.TotalSeconds;
            int timeout = timestamp - 120;
            userList.ForEach(delegate (string value)
            {
                string[] check = value.Split(',');
                if (Convert.ToInt32(check[1]) < timeout)
                    UpdateOnlineList(3, Convert.ToInt32(check[0]));
            });
            OnlineLabel.Text = "(" + OnlineList.Items.Count.ToString() + ")";
        }

        public void RichChat_TextChanged(object sender = null, EventArgs e = null)
        {
            ScrollToBottom(RichChat);
        }

        private void TextMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (TextMessageBox.Text == string.Empty || string.IsNullOrEmpty(TextMessageBox.Text) || TextMessageBox.Text.Length <= 0 || TextMessageBox.Text.Length > 140 || TextMessageBox.Text.Trim().Length == 0 || TextMessageBox.Text == lastMessage)
                {
                    e.Handled = true;
                    return;
                }                    
                else
                {
                    if (TextMessageBox.Text.Trim()[0] == '/')
                    {
                        if (TextMessageBox.Text == "/help")
                        {
                            RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST>\n");
                            RichChat.AppendText("\t/clear - Clears chat messages\n");
                            RichChat.AppendText("\t/nick - Change your nickname\n");
                            RichChat.AppendText("\t/me - Send private message\n");
                            RichChat.AppendText("\t/quit - Left chat \n");
                            RichChat.AppendText("\t/left - Left chat and save messages log\n");
                            RichChat.AppendText("\t/color - Change your nickname's color\n");
                        }
                        else if (TextMessageBox.Text.Trim().StartsWith("/nick "))
                        {
                            string pseudo = TextMessageBox.Text.Trim().Remove(0, 6);
                            RichChat.SelectionStart = RichChat.TextLength;
                            RichChat.SelectionLength = 0;
                            RichChat.SelectionColor = Color.Red;
                            if (pseudo.Contains(" "))
                                RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> You should have a nickname, right?");
                            else if (pseudo.Length > 10)
                                RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> Nickname too long!");
                            else if (pseudo.Length < 2)
                                RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> Nickname too short!");
                            else
                            {
                                client.Transmit(pseudo, Net.Message.RENAME);
                                currentName = client.Handle = pseudo;
                                LabelTitle.Text = this.Text = instance.Text = appName + " (" + currentName + "@" + appVersion + ")";
                                saveConfig();
                            }
                            RichChat.SelectionColor = RichChat.ForeColor;
                        }
                        else if (TextMessageBox.Text.Trim().StartsWith("/me "))
                        {
                            string ctx = TextMessageBox.Text.Trim().Remove(0, 4);
                            client.Transmit(ctx, Net.Message.ACTION);
                        }
                        else if (TextMessageBox.Text == "/quit")
                            Application.Exit();
                        else if (TextMessageBox.Text == "/clear")
                            RichChat.Text = string.Empty;
                        else if (TextMessageBox.Text == "/left")
                        {
                            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "_log.log", FileMode.Append);
                            StreamWriter sw = new StreamWriter(fs, ASCIIEncoding.UTF8);
                            sw.Write(RichChat.Text + "\r\n");
                            sw.Close();
                            fs.Close();
                            RichChat.Text = null;
                            Application.Exit();
                        }
                        else if (TextMessageBox.Text.Trim().StartsWith("/color "))
                        {
                            string clr = TextMessageBox.Text.Trim().Remove(0, 7);
                            string strcolor = "";
                            switch (clr)
                            {
                                case "red":
                                    client.SelectedColor = red;
                                    strcolor = "RED";
                                    break;

                                case "blue":
                                    client.SelectedColor = blue;
                                    strcolor = "BLUE";
                                    break;

                                case "green":
                                    client.SelectedColor = green;
                                    strcolor = "GREEN";
                                    break;

                                case "grey":
                                    client.SelectedColor = grey;
                                    strcolor = "GREY";
                                    break;

                                case "purple":
                                    client.SelectedColor = purple;
                                    strcolor = "PURPLE";
                                    break;

                                default:
                                    client.SelectedColor = Color.Black;
                                    strcolor = "BLACK";
                                    break;
                            }
                            RichChat.SelectionColor = client.SelectedColor;
                            RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> Color changed to ");
                            RichChat.SelectionFont = new Font(RichChat.SelectionFont, FontStyle.Bold);
                            RichChat.AppendText($"{strcolor}");
                            RichChat.SelectionColor = RichChat.ForeColor;
                            RichChat.SelectionFont = new Font(RichChat.SelectionFont, FontStyle.Regular);
                        }
                        TextMessageBox.Text = string.Empty;
                    }
                    else
                    {
                        client.Transmit(TextMessageBox.Text, Net.Message.MESSAGE);
                        TextMessageBox.Text = string.Empty;
                        
                    }
                }
                e.Handled = true;
            }
        }

        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 277;
        private const int SB_PAGEBOTTOM = 7;
        public static void ScrollToBottom(RichTextBox MyRichTextBox)
        {
            SendMessage(MyRichTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        public void UpdateChat()
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(UpdateChat));
                else
                {
                    Net.Message msg = queue.Dequeue();
                    msg.Write();
                    FlashWindow(this.Handle, false);
                }
            } 
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error to Update Chat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetMessage(int action, string user, string content = null, Color color = default)
        {
            color = Color.Black;
            RichChat.SelectionStart = RichChat.TextLength;
            RichChat.SelectionLength = 0;
            switch (action)
            {
                case Net.Message.RENAME:
                    RichChat.SelectionColor = blue;
                    RichChat.AppendText("\r\n( ! ) ");
                    RichChat.SelectionColor = color;
                    RichChat.AppendText(user);
                    RichChat.SelectionColor = blue;
                    RichChat.AppendText(" change the nickname to ");
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Bold);
                    RichChat.SelectionColor = color;
                    RichChat.AppendText(content);
                    break;

                case Net.Message.JOIN:
                    RichChat.SelectionColor = purple;
                    RichChat.AppendText("\r\n@: ");
                    RichChat.SelectionColor = color;
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Bold);
                    RichChat.AppendText(user);
                    RichChat.SelectionColor = purple;
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Regular);
                    RichChat.AppendText(" joined");
                    break;

                case Net.Message.QUIT:
                    RichChat.SelectionColor = red;
                    RichChat.AppendText("\r\n@: ");
                    RichChat.SelectionColor = color;
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Bold);
                    RichChat.AppendText(user);
                    RichChat.SelectionColor = red;
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Regular);
                    RichChat.AppendText(" left");
                    break;

                case Net.Message.ACTION:
                    RichChat.SelectionColor = grey;
                    RichChat.AppendText("\r\n$ ");
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Italic);
                    RichChat.SelectionColor = color;
                    RichChat.AppendText(user);
                    RichChat.SelectionColor = grey;
                    RichChat.AppendText(" " + content);
                    break;

                case Net.Message.MESSAGE:
                    RichChat.AppendText($"\r\n[{DateTime.Now:HH:mm:ss}] <");
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Bold);
                    RichChat.SelectionColor = color;
                    RichChat.AppendText(user);
                    RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Regular);
                    RichChat.AppendText(">");
                    if (content.StartsWith(">"))
                        RichChat.SelectionColor = purple;
                    else
                        RichChat.SelectionColor = RichChat.ForeColor;
                    RichChat.AppendText(content);
                    break;
            }
            RichChat.SelectionFont = new Font(RichChat.Font, FontStyle.Regular);
            RichChat.SelectionColor = RichChat.ForeColor;
        }

        public void UpdateOnlineList(int status, int user, string name = default)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int timestamp = (int)t.TotalSeconds;
            string display = $"🔅 {name} ({user})";
            int index = OnlineList.FindString($"({user})");
            switch (status)
            {
                case 1:
                    if (index < 0)
                    {
                        OnlineList.Items.Add(display);
                        userList.Add($"{user},{timestamp}");
                    }
                    break;

                case 2:
                    if (index >= 0)
                    {
                        OnlineList.Items[index] = display;
                        int temp = userList.FindIndex(x => x.StartsWith($"{user},"));
                        userList[temp] = $"{user},{timestamp}";
                    }
                    break;

                case 3:
                    int removeID = userList.FindIndex(x => x.StartsWith($"{user},"));
                    if (index >= 0)
                    {
                        OnlineList.Items.RemoveAt(index);
                        userList.RemoveAt(removeID);
                    }
                    break;
            }
            OnlineLabel.Text = "(" + OnlineList.Items.Count.ToString() + ")";
        }

        public void PingUpdate(int user)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int timestamp = (int)t.TotalSeconds;
            int index = userList.FindIndex(x => x.StartsWith($"{user},"));
            if (index >= 0)
                userList[index] = $"{user},{timestamp}";
        }

        void MessageQueuedEvent(object sender, EventArgs e)
        {
            UpdateChat();
        }

        void MessageReceivedEvent(object sender, MessageEvent e)
        {
            queue.Add(e.ReceivedMessage);
            try
            {
                if (e.ReceivedMessage.TextContent.Contains(client.Handle) && e.ReceivedMessage.TypeMessage == Net.Message.MESSAGE)
                    notifyIcon.ShowBalloonTip(2000, e.ReceivedMessage.NickName, e.ReceivedMessage.TextContent, ToolTipIcon.None);
            }
            catch
            {
                // ;(
            }
        }

        private void saveConfig()
        {
            FileStream fs = new FileStream($"{Environment.CurrentDirectory}\\config.conf", FileMode.Truncate);
            StreamWriter sw = new StreamWriter(fs, ASCIIEncoding.UTF8);
            sw.Write(currentName + "\n" + Convert.ToString(this.saveOnExit) + "\n" + client.SelectedColor);
            sw.Close();
            fs.Close();
        }

        public void SetConfig(int key, string value)
        {
            switch(key)
            {
                case 1:
                    client.SelectedColor = Color.Black;
                    break;

                case 2:
                    if (currentName != value)
                    {
                        RichChat.SelectionColor = red;
                        if (value.Length == 0 || value == null || value.Contains(" "))
                            RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> You should have a nickname, right?");
                        else if (value.Length > 10)
                            RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> Nickname too long!");
                        else if (value.Length < 2)
                            RichChat.AppendText($"\n[{DateTime.Now:HH:mm:ss}] <HOST> Nickname too short!");
                        else
                        {
                            client.Transmit(value, Net.Message.RENAME);
                            client.Handle = value;
                            currentName = client.Handle;
                            LabelTitle.Text = this.Text = instance.Text = appName + " (" + currentName + "@" + appVersion + ")";
                        }
                        RichChat.SelectionColor = RichChat.ForeColor;
                    }
                    break;

                case 3:
                    saveOnExit = Convert.ToBoolean(value);
                    break;
            }
            saveConfig();
        }
    }
}
