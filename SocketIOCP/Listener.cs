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


        private List<Worker> workerList = new List<Worker>();

        private Socket socket;

        private IOCallback ioCallback;

        private int maxSocketCountPerWorker = 200;
        private int maxWorkerCount = 100;


        public Listener(Socket socket, IOCallback ioCallback)
        {
            this.socket = socket;
            this.ioCallback = ioCallback;
        }
        

        public void Start()
        {
            Thread thread = new Thread( ListenClientConnect );
            thread.Start();
        }


        private void ListenClientConnect()
        {

            Worker worker;
            Job job;

            try
            {

                while (true)
                {
                    log.Info("ListenClientConnect while.");

                    Socket clientSocket = socket.Accept();
                    clientSocket.Blocking = false;
                    //clientSocket.ReceiveTimeout = 1;


                    lock (workerList)
                    {
                        if (workerList.Count == 0)
                        {

                            worker = new Worker(workerList, ioCallback);

                            job = new Job(clientSocket, this.ioCallback);
                            worker.AddJob( job );

                            workerList.Add(worker);

                            worker.Start();


                            log.Info("Add Worker, index: " + (workerList.Count - 1));
                            continue;
                        }


                        worker = workerList[ workerList.Count - 1 ];

                        //if (worker.JobCount < maxSocketCountPerWorker)
                        //{
                        //    job = new Job(clientSocket, this.ioCallback);
                        //    worker.AddJob(job);

                        //    log.Info("Add Socket: index: " + (worker.JobCount - 1) + "  Worker Index: " + workerList.IndexOf(worker));
                        //    continue;
                        //}

                        if ((DateTime.Now - worker.currentLoopBeginTime).TotalMilliseconds < 40)
                        {
                            job = new Job(clientSocket, this.ioCallback);
                            worker.AddJob(job);

                            log.Info("Add Socket: index: " + (worker.JobCount - 1) + "  Worker Index: " + workerList.IndexOf(worker));
                            continue;
                        }


                        if (workerList.Count < maxWorkerCount)
                        {

                            worker = new Worker(workerList, ioCallback);

                            job = new Job(clientSocket, this.ioCallback);
                            worker.AddJob(job);

                            workerList.Add(worker);

                            worker.Start();


                            log.Info("Add Worker, index: " + (workerList.Count - 1));
                            continue;
                        }
                    }


                    log.Info("Socket count is exceeded.");

                    socket.Send(Encoding.ASCII.GetBytes("Socket count is exceeded."));
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                }
            }
            catch(Exception ex)
            {
                log.Error("ListenClientConnect while  Error: " + ex.ToString());
            }
        }

    }
}
