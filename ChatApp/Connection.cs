using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp
{
    public class Connection
    {
        static Socket _listenerSocket;
        static Socket _senderSocket;

        /*public static Socket GetActiveSocket(User user)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket senderSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Thread listnerThread = new Thread(new ThreadStart(() => { listenerSocket = GetListenerSocket(user); }));
            listnerThread.Start();
            Thread senderThread = new Thread(new ThreadStart(() => { listenerSocket = GetSenderSocket(user); }));
            senderThread.Start();

            if(String.Compare(user.myPort, user.clientPort,StringComparison.InvariantCulture) > 0)
            {
                
                return listenerSocket;
            }
            else
            {
                return senderSocket;
            }
            
        }*/

        public static Socket GetListenerSocket(User user)
        {
            IPEndPoint endPoint = new IPEndPoint(user.IP, Int32.Parse(user.myPort));
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endPoint);
            listener.Listen(10);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket = listener.Accept();
                // Sending Host Name to connected peer machine !
                var hostName = new Message
                {
                    text = user.myName
                };
                var hostNameInBytes = DataModification.StringEncoding(hostName);
                socket.Send(hostNameInBytes);

            }
            catch
            {
                Monitor.DisplayLineEnd("Error: Unable to connect to Peer!");
            }
            _listenerSocket = socket;
            return socket;
        }

        public static void Close(Socket socket)
        {
            /*
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            */
            
            _listenerSocket.Shutdown(SocketShutdown.Receive);
            //_listenerSocket.Close();
            _senderSocket.Shutdown(SocketShutdown.Send);
            //_senderSocket.Close();
            

            Monitor.DisplayLineEnd("Connection Terminated!");
        }

        public static Socket GetSenderSocket(User user)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(user.IP, Int32.Parse(user.clientPort));

            while(true)
            {
                try
                {
                    socket.Connect(endPoint);
                    // Receiving Client Name from connected peer machine
                    var receivedPeerName = new Byte[1024];
                    var nameByteLength = socket.Receive(receivedPeerName);
                    var peerName = DataModification.ByteDecoding(receivedPeerName, nameByteLength);
                    user.clientName = peerName.text;

                    Monitor.DisplayLineEnd("_________Connected to "+user.clientName+"!_________");
                    break;
                }
                catch
                {
                    Monitor.DisplayLineEnd("Waiting for Connection. Retrying again in 3 seconds...");
                    Thread.Sleep(3000);
                }
            }
            _senderSocket = socket;
            return socket;
        }

        public static IPAddress GetHostIPAddress()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            return ipAddr;
        }

       
    }
}
