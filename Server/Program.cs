using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using System.IO;

using SocketIOCP;

using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Server
{
    class Program
    {

        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        static void Main(string[] args)
        {
            
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(ip, 9527));  //绑定IP地址：端口  
            socket.Listen(10);    //设定最多10个排队连接请求  

            log.Info("启动监听 " + socket.LocalEndPoint.ToString() + " 成功");


            //  使用  SocketIOCP
            Listener listener = new Listener(socket, ProcessBytes);
            listener.Start();

            
            Console.ReadLine();

        }

        

        private static void ProcessBytes( byte[] bytes, int size, Socket socket )
        {

            MemoryStream stream = new MemoryStream();
            stream.Write(bytes, 0, size);

            bytes = new byte[stream.Length];

            stream.Position = 0;

            stream.Read(bytes, 0, bytes.Length);

            
            string content = Encoding.ASCII.GetString(bytes);

            log.Info("处理 客户端  " + socket.RemoteEndPoint.ToString() + " 消息 （ IO回调  Process Bytes ）\r\n" 
                                    + "size: " + size + "\r\n" + content);

            //    实际 的  业务处理  要 放到 一个 新线程  中  处理
            //    IO 回调  只负责 数据接收 和 初步的 解析 
            //    IO 回调  接收到 一个 完整的 Request 数据， 就应该把 数据 Pass 给 新线程 来 处理
            //    业务处理   由  新线程   负责

            socket.Send(Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\n"
                + "Date: Mon, 27 Jul 2009 12:28:53 GMT\r\n"
                + "Server: Apache\r\n"
                + "Last-Modified: Wed, 22 Jul 2009 19:15:56 GMT\r\n"
                + "ETag: \"34aa387-d-1568eb00\"\r\n"
                + "Accept-Ranges: bytes\r\n"
                + "Content-Length: 13\r\n"
                + "Vary: Accept-Encoding\r\n"
                + "Content-Type: text/plain\r\n\r\n"
                + "Hello World !"));
        }
    }
}
