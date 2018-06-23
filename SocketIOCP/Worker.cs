using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

using System.Threading;

using log4net;

namespace SocketIOCP
{
    
    public class Worker
    {

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //private List<Socket> socketList = new List<Socket>();
        private List<Job> jobList = new List<Job>();

        private IOCallback ioCallback;
        private List<Worker> workerList;

        internal DateTime currentLoopBeginTime; 


        public Worker(List<Worker> workerList,  IOCallback ioCallback)
        {
            this.workerList = workerList;
            this.ioCallback = ioCallback;

            this.currentLoopBeginTime = DateTime.Now;
        }


        public void Start()
        {
            Thread thread = new Thread( new ThreadStart( Work ) );
            thread.Start();
        }

        private void Work()
        {
            Job job;

            Socket socket;

            byte[] bytes;

            int size;

            int i = 0;

            while (true)
            {
                lock(this.workerList)
                {
                    if (this.jobList.Count == 0)
                    {
                        this.workerList.Remove(this);

                        log.Info("Remove Worker, index: " + this.workerList.Count);
                        break;
                    }

                    if (i >= this.jobList.Count)
                    {
                        i = 0;

                        if ((DateTime.Now - this.currentLoopBeginTime).TotalMilliseconds <= 2 )
                            Thread.Sleep(1);
                    }
                       

                    if (i == 0)
                    {
                        this.currentLoopBeginTime = DateTime.Now;
                    }

                    job = this.jobList[i];
                }

                

                socket = job.Socket;
                
                // 通过 socket 接收数据  

                //  这里 为什么 判断了  socket.Available > 0  之后，仍然 要 用 try 来 判断 客户端 是否 已 关闭连接
                //  因为， 按照 msdn 的 说法， 如果 客户端 连接 已关闭，则 调用 Available 属性 时 会 抛出 异常
                //  但是， 实际上 并不会 抛出 异常。 所以如果不用 try 来 判断 客户端 连接 是否 已 关闭 的 话
                //  客户端 关闭 连接后， 程序并不知道， 还会 一直 循环执行下去 。

                if ( socket.Available > 0 )
                {
                    bytes = new byte[socket.Available];
                }
                else
                {
                    //bytes = new byte[1];
                    i++;
                    continue;
                }
                    
                        
                try
                {

                    size = socket.Receive(bytes);

                    //job.lastAvailableTime = DateTime.Now;


                    log.Info("Worker Index: " + this.workerList.IndexOf(this) + "\r\n"
                                    + "Socket Index: " + i + "\r\n接收 客户端 " + socket.RemoteEndPoint.ToString()
                                    + " 消息\r\n" + "size: " + size + "\r\n" + Encoding.ASCII.GetString(bytes, 0, size));


                }
                catch(SocketException ex)
                {
                    //   ex.ErrorCode == 10035  表示 Socket 在 非阻塞模式（non-blocking）下 当前没有可读取的数据时，抛出的异常
                    if (ex.ErrorCode == 10035)
                    {
                        i++;
                        continue;
                    }

                    //   ex.ErrorCode == 10060  表示 读取数据 超时， 超时时间 由 socket.ReceiveTimeout 设定
                    //if ( ex.ErrorCode == 10060)
                    //{
                    //    i++;
                    //    continue;
                    //}

                    log.Error("Conn close: " + ex.Message);
                    
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                    lock (this.workerList)
                    {
                        jobList.Remove(job);
                    }

                    continue;
                }
                        
                try
                {
                    job.IOCallback(bytes, size, socket);
                }
                catch (Exception ex)
                {
                    log.Error("Execute Callback Error: " + ex.Message);
                }


                i++;

                
            }  
        }
        
        internal void AddJob(Job job)
        {
            this.jobList.Add(job);
        }

        internal int JobCount
        {
            get { return this.jobList.Count; }
        }
    }
}
