using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat.Net
{
    public class Client
    {
        #region DEFINE CONSTS

        public const int PROTOCOL_VERSION = 2;
        private const int PORT = 42692; // Any port above 3000 is valid, ports below 3000 is reserved to OS use
        private const string MULTICAST_ADDR = "224.0.0.251";

        #endregion

        #region PRIVATE ATTRIBUTES

        private UdpClient udpClient;
        private UdpClient udpListener;
        private UdpState udpState;

        #endregion

        public static Client Instance { get; set; }

        public event EventHandler<MessageEvent> ReceivedMessage;
        public string Handle { get; set; }
        public int TypeMessage { get; set; }
        public bool SaveOnQuitConfig { get; set; }
        public Color SelectedColor { get; set; }
        public int ClientIP { get; set; }
        public Array Listerners { get; set; }
        public bool Listening { get; set; }

        public Client()
        {
            Client.Instance = this;
            string IP;
            int index = 0;
            do
            {
                IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[index].ToString();
                this.ClientIP = IPAddress.Parse(IP).GetAddressBytes()[3];
                index++;
            } while (this.ClientIP == 0 || ClientIP == 76 || IPAddress.Parse(IP).GetAddressBytes()[2] == 42 || IPAddress.Parse(IP).GetAddressBytes()[2] == 43);
            // 4º Octect : 0 | 76 = Local Loop IPs
            // 3º Octect : 42 | 43 = Default Android Threating

            this.Handle = Environment.MachineName;
            this.SelectedColor = Color.Black;
            udpClient = new UdpClient();
            udpClient.JoinMulticastGroup(IPAddress.Parse(MULTICAST_ADDR));
            udpClient.MulticastLoopback = true;
            udpState = new UdpState();
            Listen();
        }

        public void Loading()
        {
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\config.conf", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs, ASCIIEncoding.UTF8);
            Handle = Convert.ToString(sr.ReadLine());

            if (Handle == null || Handle.Contains(" ") || Handle.Length > 10 || Handle.Length < 2)
                Handle = Environment.MachineName;

            string SaveOnQuitConfigString = sr.ReadLine();
            if (SaveOnQuitConfigString != "True" && SaveOnQuitConfigString != "False")
                SaveOnQuitConfigString = "True";
            SaveOnQuitConfig = Convert.ToBoolean(SaveOnQuitConfigString);
            SelectedColor = Color.Black;
            sr.Close();
            fs.Close();
        }

        public void Transmit (string content, int typemessage)
        {
            Message msg = new Message(
                ip: this.ClientIP, 
                protocolversion: PROTOCOL_VERSION, 
                nickname: this.Handle,
                typemessage: typemessage,
                textcontent: content
            );
            string json = msg.ToJSON();
            byte[] send = Encoding.UTF8.GetBytes(json);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(MULTICAST_ADDR), PORT);

            udpClient.BeginSend(send, send.Length, endPoint, new AsyncCallback(this.SendCallback), udpClient);
        }

        private void SendCallback (IAsyncResult async)
        {
            UdpClient u = (UdpClient)async.AsyncState;
            u.EndSend(async);
        }

        private void Listen()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
                udpListener = new UdpClient(endPoint);
                udpState = new UdpState();
                udpState.EndPoint = endPoint;
                udpState.UdpClient = udpListener;
                udpListener.JoinMulticastGroup(IPAddress.Parse(MULTICAST_ADDR));
                udpListener.Ttl = 1;
                udpListener.BeginReceive(new AsyncCallback(ReceiveCallback), udpState);
            }
            catch
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT - 1);
                udpListener = new UdpClient(endPoint);
                udpState = new UdpState();
                udpState.EndPoint = endPoint;
                udpState.UdpClient = udpListener;
                udpListener.JoinMulticastGroup(IPAddress.Parse(MULTICAST_ADDR));
                udpListener.Ttl = 1;
                udpListener.BeginReceive(new AsyncCallback(ReceiveCallback), udpState);
            }
        }

        public void ReceiveCallback (IAsyncResult async)
        {
            udpListener.BeginReceive(new AsyncCallback(ReceiveCallback), udpState);
            UdpClient client = ((UdpState)async.AsyncState).UdpClient;
            IPEndPoint endPoint = ((UdpState)async.AsyncState).EndPoint;
            byte[] receiveBytes = client.EndReceive(async, ref endPoint);
            string receiveString = Encoding.UTF8.GetString(receiveBytes);
            OnMessageReceived(new MessageEvent(receiveString));
        }

        protected virtual void OnMessageReceived (MessageEvent e)
        {
            ReceivedMessage?.Invoke(this, e);
        }
    }

    #region UDP STATE

    public class UdpState
    {
        public IPEndPoint EndPoint { get; set; }
        public UdpClient UdpClient { get; set; }
    }

    #endregion
}
