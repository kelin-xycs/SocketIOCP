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
    public class Listener
    {

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //internal ChainList<Worker> workerList = new ChainList<Worker>();
        //internal Q<Job> jobQueue = new Q<Job>();
        private Q<Socket> socketQueue = new Q<Socket>();

        //private Q<Socket> socketQueue = new Q<Socket>();

        private Socket socket;

        private IOCallback ioCallback;

        private int tryCount = 5;

        //private int maxSocketCountPerWorker = 200;
        //private int maxWorkerCount = 100;


        public Listener(Socket socket, IOCallback ioCallback)
        {
            this.socket = socket;
            this.ioCallback = ioCallback;
        }
        

        public void Start()
        {
            Thread thread = new Thread( ListenClientConnect );
            thread.Start();

            Thread thread2 = new Thread(Dispatch);
            thread2.Start();

        }

        private void Dispatch()
        {
            int jobCount;

            Socket socket;

            Worker worker;

            Job job;

            while (true)
            {
                socket = null;

                for(int i=0; i<=this.tryCount; i++)
                {
                    lock (this.socketQueue)
                    {
                        socket = this.socketQueue.Get();
                    }

                    if (socket != null)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                
                if (socket == null)
                {
                    continue;
                }

                job = new Job(socket, this.ioCallback);

                worker = new Worker(this, job);

                worker.Start();
                //log.Info("Dispatch while.");

                //jobCount = GetJobCount();

                //log.Info("Dispatch Worker jobCount: " + jobCount);

                //if (jobCount == 0)
                //{
                //    continue;
                //}

                ////job = this.jobQueue.Get();

                ////job = new Job(socket, this.ioCallback);

                ////this.jobQueue.Add(job);

                ////if (this.workerList.Count >= 2)
                ////    continue;

                //worker = new Worker(this);

                //lock(this.workerList)
                //{
                //    this.workerList.Add(worker);
                //}
               

                //worker.Start();

                log.Info("add worker: ");
                //log.Info("add worker: workerList Count: " + this.workerList.Count);

            }
        }

        private void ListenClientConnect()
        {

            Worker worker;
            Job job;

            try
            {

                while (true)
                {
                    //log.Info("ListenClientConnect while.");

                    Socket clientSocket = socket.Accept();
                    clientSocket.Blocking = false;
                    //clientSocket.ReceiveTimeout = 1;


                    //log.Info("ListenClientConnect will add job.");

                    lock( this.socketQueue )
                    {
                        this.socketQueue.Add(clientSocket);
                    }
                    //job = new Job(clientSocket, this.ioCallback);
                    //lock( this.jobQueue )
                    //{
                    //    this.jobQueue.Add(job);
                    //}


                    log.Info("ListenClientConnect socketQueue Count." + this.socketQueue.Count);
                    

                    
                    //lock (workerList)
                    //{
                    //    if (workerList.Count == 0)
                    //    {

                    //        worker = new Worker(workerList, ioCallback);

                    //        job = new Job(clientSocket, this.ioCallback);
                    //        worker.AddJob( job );

                    //        workerList.Add(worker);

                    //        worker.Start();


                    //        log.Info("Add Worker, index: " + (workerList.Count - 1));
                    //        continue;
                    //    }


                    //    worker = workerList[ workerList.Count - 1 ];

                    //    //if (worker.JobCount < maxSocketCountPerWorker)
                    //    //{
                    //    //    job = new Job(clientSocket, this.ioCallback);
                    //    //    worker.AddJob(job);

                    //    //    log.Info("Add Socket: index: " + (worker.JobCount - 1) + "  Worker Index: " + workerList.IndexOf(worker));
                    //    //    continue;
                    //    //}

                    //    if ((DateTime.Now - worker.currentLoopBeginTime).TotalMilliseconds < 40)
                    //    {
                    //        job = new Job(clientSocket, this.ioCallback);
                    //        worker.AddJob(job);

                    //        log.Info("Add Socket: index: " + (worker.JobCount - 1) + "  Worker Index: " + workerList.IndexOf(worker));
                    //        continue;
                    //    }


                    //    if (workerList.Count < maxWorkerCount)
                    //    {

                    //        worker = new Worker(workerList, ioCallback);

                    //        job = new Job(clientSocket, this.ioCallback);
                    //        worker.AddJob(job);

                    //        workerList.Add(worker);

                    //        worker.Start();


                    //        log.Info("Add Worker, index: " + (workerList.Count - 1));
                    //        continue;
                    //    }
                    //}


                    //log.Info("Socket count is exceeded.");

                    //socket.Send(Encoding.ASCII.GetBytes("Socket count is exceeded."));
                    //socket.Shutdown(SocketShutdown.Both);
                    //socket.Close();

                }
            }
            catch(Exception ex)
            {
                log.Error("ListenClientConnect while  Error: " + ex.ToString());
            }
        }

    }
}
