namespace P2P_Chat
{
    partial class ChatForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ClockLabel = new System.Windows.Forms.Label();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.vSeparator1 = new P2P_Chat.Controls.VSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.OnlineList = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TextMessageBox = new System.Windows.Forms.TextBox();
            this.RichChat = new System.Windows.Forms.RichTextBox();
            this.MessageBar = new P2P_Chat.Controls.HSeparator();
            this.label2 = new System.Windows.Forms.Label();
            this.LeftCharLabel = new System.Windows.Forms.Label();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.OnlineLabel = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TickerTime = new System.Windows.Forms.Timer(this.components);
            this.PingTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.ClockLabel);
            this.panel1.Controls.Add(this.LabelTitle);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 26);
            this.panel1.TabIndex = 0;
            // 
            // ClockLabel
            // 
            this.ClockLabel.AutoSize = true;
            this.ClockLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClockLabel.ForeColor = System.Drawing.Color.White;
            this.ClockLabel.Location = new System.Drawing.Point(521, 6);
            this.ClockLabel.Name = "ClockLabel";
            this.ClockLabel.Size = new System.Drawing.Size(64, 16);
            this.ClockLabel.TabIndex = 1;
            this.ClockLabel.Text = "HH:MM:SS";
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = System.Drawing.Color.White;
            this.LabelTitle.Location = new System.Drawing.Point(4, 6);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(27, 16);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "title";
            // 
            // vSeparator1
            // 
            this.vSeparator1.LineColor = System.Drawing.Color.LightGray;
            this.vSeparator1.LineWidth = 1;
            this.vSeparator1.Location = new System.Drawing.Point(161, 31);
            this.vSeparator1.Name = "vSeparator1";
            this.vSeparator1.Size = new System.Drawing.Size(28, 407);
            this.vSeparator1.TabIndex = 1;
            this.vSeparator1.Text = "vSeparator1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "ONLINE";
            // 
            // OnlineList
            // 
            this.OnlineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OnlineList.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnlineList.FormattingEnabled = true;
            this.OnlineList.ItemHeight = 17;
            this.OnlineList.Location = new System.Drawing.Point(12, 72);
            this.OnlineList.Name = "OnlineList";
            this.OnlineList.Size = new System.Drawing.Size(143, 357);
            this.OnlineList.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(-1, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 10);
            this.panel2.TabIndex = 1;
            // 
            // TextMessageBox
            // 
            this.TextMessageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextMessageBox.Location = new System.Drawing.Point(195, 407);
            this.TextMessageBox.MaxLength = 140;
            this.TextMessageBox.Name = "TextMessageBox";
            this.TextMessageBox.Size = new System.Drawing.Size(348, 15);
            this.TextMessageBox.TabIndex = 4;
            this.TextMessageBox.TextChanged += new System.EventHandler(this.TextMessageBox_TextChanged);
            this.TextMessageBox.Enter += new System.EventHandler(this.TextMessageBox_Enter);
            this.TextMessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextMessageBox_KeyPress);
            this.TextMessageBox.Leave += new System.EventHandler(this.TextMessageBox_Leave);
            // 
            // RichChat
            // 
            this.RichChat.BackColor = System.Drawing.Color.White;
            this.RichChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichChat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RichChat.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichChat.Location = new System.Drawing.Point(195, 72);
            this.RichChat.Name = "RichChat";
            this.RichChat.ReadOnly = true;
            this.RichChat.Size = new System.Drawing.Size(365, 329);
            this.RichChat.TabIndex = 5;
            this.RichChat.Text = "";
            this.RichChat.TextChanged += new System.EventHandler(this.RichChat_TextChanged);
            // 
            // MessageBar
            // 
            this.MessageBar.LineColor = System.Drawing.Color.DarkGray;
            this.MessageBar.LineWidth = 2;
            this.MessageBar.Location = new System.Drawing.Point(195, 413);
            this.MessageBar.Name = "MessageBar";
            this.MessageBar.Size = new System.Drawing.Size(348, 23);
            this.MessageBar.TabIndex = 6;
            this.MessageBar.Text = "hSeparator1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(195, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "CHAT";
            // 
            // LeftCharLabel
            // 
            this.LeftCharLabel.AutoSize = true;
            this.LeftCharLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftCharLabel.Location = new System.Drawing.Point(547, 409);
            this.LeftCharLabel.Name = "LeftCharLabel";
            this.LeftCharLabel.Size = new System.Drawing.Size(26, 16);
            this.LeftCharLabel.TabIndex = 8;
            this.LeftCharLabel.Text = "140";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(563, 72);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(10, 337);
            this.vScrollBar1.TabIndex = 9;
            // 
            // OnlineLabel
            // 
            this.OnlineLabel.AutoSize = true;
            this.OnlineLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlineLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnlineLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.OnlineLabel.Location = new System.Drawing.Point(69, 43);
            this.OnlineLabel.Name = "OnlineLabel";
            this.OnlineLabel.Size = new System.Drawing.Size(24, 17);
            this.OnlineLabel.TabIndex = 10;
            this.OnlineLabel.Text = "(3)";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // TickerTime
            // 
            this.TickerTime.Enabled = true;
            this.TickerTime.Tick += new System.EventHandler(this.TickerTime_Tick);
            // 
            // PingTimer
            // 
            this.PingTimer.Enabled = true;
            this.PingTimer.Interval = 40000;
            this.PingTimer.Tick += new System.EventHandler(this.PingTimer_Tick);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(587, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OnlineLabel);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.LeftCharLabel);
            this.Controls.Add(this.TextMessageBox);
            this.Controls.Add(this.MessageBar);
            this.Controls.Add(this.RichChat);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.OnlineList);
            this.Controls.Add(this.vSeparator1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChatForm";
            this.Text = "P2P-Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.VSeparator vSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox OnlineList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TextMessageBox;
        private System.Windows.Forms.RichTextBox RichChat;
        private Controls.HSeparator MessageBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LeftCharLabel;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Label OnlineLabel;
        private System.Windows.Forms.Label LabelTitle;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label ClockLabel;
        private System.Windows.Forms.Timer TickerTime;
        private System.Windows.Forms.Timer PingTimer;
    }
}

