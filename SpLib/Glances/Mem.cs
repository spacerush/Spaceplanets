using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Glances
{
    public class Mem
    {
        public long available { get; set; }
        public long used { get; set; }
        public long cached { get; set; }
        public decimal percent { get; set; }
        public long free { get; set; }
        public long inactive { get; set; }
        public long active { get; set; }
        public long shared { get; set; }
        public long total { get; set; }
        public long buffers { get; set; }
    }
}
