using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

namespace SocketIOCP
{
    class Job
    {
        private Socket socket;
        private MemoryStreamBuffer buffer;
        private IOCallback ioCallback;


        public Job(Socket socket, IOCallback ioCallback)
        {
            this.socket = socket;

            this.buffer = new MemoryStreamBuffer();

            this.ioCallback = ioCallback;
        }

        public Socket Socket
        {
            get { return this.socket; }
        }

        public MemoryStreamBuffer Buffer
        {
            get { return this.buffer; }
        }

        public IOCallback IOCallback
        {
            get { return this.ioCallback; }
        }

        public DateTime lastAvailableTime = DateTime.MinValue;
    }
}
