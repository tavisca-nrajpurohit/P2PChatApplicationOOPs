using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp
{
    public class Conversation
    {
        public static void Begin(User user)
        {
            //var socket = Connection.GetActiveSocket(user);
            Thread sendingThread = new Thread(new ThreadStart(() => SendMessages(user)));
            sendingThread.Start();
            Thread listeningThread = new Thread(new ThreadStart(() => ReceiveMessages(user)));
            listeningThread.Start();
        }
        public static void SendMessages(User user)
        {
            var socket = Connection.GetSenderSocket(user);
            while(true)
            {
                try
                {
                    if(!socket.Connected)
                    {
                        break;
                    }
                    var message = new Message
                    {
                        text = Console.ReadLine()
                    };
                    var messageInBytes = DataModification.StringEncoding(message);
                    socket.Send(messageInBytes);
                    if (message.text.IndexOf("exit") > -1)
                    {
                        Connection.Close(socket);
                        break;
                    }
                }
                catch
                {
                    break;
                }                            
            }
        }
        public static void ReceiveMessages(User user)
        {
            var socket = Connection.GetListenerSocket(user);
            while(true)
            {
                try
                {
                    var receivedByteMessage = new Byte[1024];
                    var messageByteLength = socket.Receive(receivedByteMessage);
                    var message = DataModification.ByteDecoding(receivedByteMessage, messageByteLength);
                    Monitor.DisplayLineEnd(">>" + user.clientName + " : " + message.text);
                    if (message.text.IndexOf("exit") > -1)
                    {
                        Connection.Close(socket);
                        break;
                    }
                }
                catch
                {
                    break;
                }                
            }
            
        }
    }
}
