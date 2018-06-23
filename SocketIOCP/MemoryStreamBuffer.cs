using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace SocketIOCP
{
    public class MemoryStreamBuffer
    {
        private MemoryStream stream;
        private long effectLength;

        public MemoryStreamBuffer()
        {
            this.stream = new MemoryStream();
            this.effectLength = 0;
        }

        public MemoryStream Stream
        {
            get { return this.stream; }
        }

        public long EffectLength
        {
            get { return this.effectLength; }
            set { this.effectLength = value; }
        }
    }
}
