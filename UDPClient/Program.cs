using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. 建立Socket
            Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //2. 傳送數據, while loop
            while (true)
            {
                EndPoint serverIPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.43.96"), 1122);
                string message = Console.ReadLine();
                byte[] dataBuffer = Encoding.UTF8.GetBytes(message);
                udpClient.SendTo(dataBuffer, serverIPEndPoint);
            }

            udpClient.Close();
        }
    }
}
