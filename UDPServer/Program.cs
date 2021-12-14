using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDPServer
{
    class Program
    {
        private static Socket udpServer;
        static void Main(string[] args)
        {
            //1. 建立Socket
            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //2. Bind 本機IP + port
            EndPoint ipEndPoint = new IPEndPoint(new IPAddress(new byte[] { 192, 168, 43, 96 }), 1122);
            udpServer.Bind(ipEndPoint);

            //3. 接收數據
            new Thread(ReceiveMessage) { IsBackground = true }.Start();

            Console.Read();
        }

        static void ReceiveMessage()
        {
            while (true)
            {
                EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataBuffer = new byte[1024];
                //3. UDP不需監聽連線, 直接接收數據
                int length = udpServer.ReceiveFrom(dataBuffer, ref remoteEndPoint);//這個方法會把數據的來源(IP + Port)放到第2個參數
                string message = Encoding.UTF8.GetString(dataBuffer, 0, length);
                Console.WriteLine("從IP: " + (remoteEndPoint as IPEndPoint).Address.ToString() + ":" + (remoteEndPoint as IPEndPoint).Port + "收到數據: " + message);
            }
        }
    }
}
