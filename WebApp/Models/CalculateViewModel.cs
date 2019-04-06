using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CalculateViewModel
    {
        public int X;
        public int Y;
        public int XbyY {
            get
            {
                return X / Y;
            }
        }
        public CalculateViewModel(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
