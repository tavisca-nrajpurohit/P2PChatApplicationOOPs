using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public class User
    {
        public String myName;
        public String myPort;
        public String clientName = "Client";
        public String clientPort;
        public IPAddress IP;

        public void SetDetails()
        {
            IP = Connection.GetHostIPAddress();
            Monitor.DisplayLineEnd("Host(your) Ip Address : " + IP);
            Monitor.Display("Input your Port : ");
            this.myPort = Console.ReadLine();
            Monitor.Display("Input your Name : ");
            this.myName = Console.ReadLine();
            Monitor.Display("Input Other User-Machine's Port : ");
            this.clientPort = Console.ReadLine();
        }
    }
}
