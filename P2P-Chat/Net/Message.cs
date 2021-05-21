using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace P2P_Chat.Net
{
    #region MESSAGE CLASS
    [Serializable]
    public class Message
    {
        #region DEFINE TYPE MESSAGES

        public const int RENAME = 1;
        public const int JOIN = 2;
        public const int QUIT = 3;
        public const int ACTION = 4;
        public const int PING = 6;
        public const int MESSAGE = 5;

        #endregion

        public int IP { get; set; }
        public int ProtocolVersion { get; set; }
        public string NickName { get; set; }
        public int TypeMessage { get; set; }
        public string TextContent { get; set; }

        public Message(int ip = 0, int protocolversion = 0, string nickname = "", int typemessage = Message.MESSAGE, string textcontent = "")
        {
            this.IP = ip;
            this.ProtocolVersion = protocolversion;
            this.NickName = nickname;
            this.TypeMessage = typemessage;
            this.TextContent = textcontent;
        }

        public string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public void Write()
        {
            if (Client.PROTOCOL_VERSION != this.ProtocolVersion)
                return;

            switch(this.TypeMessage)
            {
                case Message.RENAME:
                    ChatForm.instance.UpdateOnlineList(2, this.IP, this.TextContent);
                    ChatForm.instance.SetMessage(RENAME, this.NickName, this.TextContent);
                    break;

                case Message.JOIN:
                    ChatForm.instance.UpdateOnlineList(1, this.IP, this.NickName);
                    ChatForm.instance.SetMessage(JOIN, this.NickName);
                    ChatForm.instance.PingTimer_Tick();
                    break;

                case Message.QUIT:
                    ChatForm.instance.UpdateOnlineList(3, this.IP, this.NickName);
                    ChatForm.instance.SetMessage(QUIT, this.NickName);
                    break;

                case Message.ACTION:
                    ChatForm.instance.SetMessage(ACTION, this.NickName, this.TextContent);
                    break;

                case Message.PING:
                    ChatForm.instance.UpdateOnlineList(PING, this.IP, this.NickName);
                    ChatForm.instance.PingUpdate(this.IP);
                    break;

                case Message.MESSAGE:
                    ChatForm.instance.SetMessage(MESSAGE, this.NickName, this.TextContent);
                    break;

                default:
                    break;
            }
            ChatForm.instance.RichChat_TextChanged();
        }
    }

    #endregion

    #region MESSAGE QUEUE

    public class Messages : Queue<Message>
    {
        public event EventHandler MessageQueued;

        public void Add (Message msg)
        {
            base.Enqueue(msg);
            MessageQueued?.Invoke(this, new EventArgs());
        }
    }

    #endregion

    #region MESSAGE EVENT

    public class MessageEvent : EventArgs
    {
        public Message ReceivedMessage { get; set; }
        
        public MessageEvent(string content)
        {
            try
            {
                List<string> list = new List<string>();
                JObject json = JObject.Parse(content);
                foreach (var e in json)
                    list.Add(e.Value.ToString());
                ReceivedMessage = new Message(
                    int.Parse(list[0]),
                    int.Parse(list[1].ToString()),
                    list[2],
                    int.Parse(list[3]),
                    list[4]);
            }
            catch
            {
                ChatForm.instance.notifyIcon.ShowBalloonTip(2000, "Error", "There were problems sending message, check your internet connection : " + content, System.Windows.Forms.ToolTipIcon.None);
            }
        }
    }

    #endregion
}
