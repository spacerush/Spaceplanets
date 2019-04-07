using System;
using System.Collections.Generic;
using System.Text;

namespace SpLib.Glances
{
    public class Cpu
    {
        public decimal softirq { get; set; }
        public decimal iowait { get; set; }
        public int interrupts { get; set; }
        public decimal system { get; set; }
        public int soft_interrupts { get; set; }
        public decimal time_since_update { get; set; }
        public decimal idle { get; set; }
        public decimal user { get; set; }
        public decimal guest_nice { get; set; }
        public decimal irq { get; set; }
        public decimal cpucore { get; set; }
        public decimal syscalls { get; set; }
        public decimal total { get; set; }
        public decimal steal { get; set; }
        public decimal ctx_switches { get; set; }
        public decimal nice { get; set; }
    }
}
