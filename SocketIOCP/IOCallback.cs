using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

namespace SocketIOCP
{
    //public delegate void IOCallback( MemoryStreamBuffer buffer, Socket socket );

    public delegate void IOCallback(byte[] bytes, int size, Socket socket);
}
