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

                            worker.AddSocket(clientSocket);

                            workerList.Add(worker);

                            worker.Start();


                            log.Info("Add Worker, index: " + (workerList.Count - 1));
                            continue;
                        }


                        worker = workerList[ workerList.Count - 1 ];

                        if (worker.SocketCount < maxSocketCountPerWorker)
                        {
                            worker.AddSocket(clientSocket);

                            log.Info("Add Socket: index: " + (worker.SocketCount - 1) + "  Worker Index: " + workerList.IndexOf(worker));
                            continue;
                        }


                        if (workerList.Count < maxWorkerCount)
                        {

                            worker = new Worker(workerList, ioCallback);

                            worker.AddSocket(clientSocket);

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
