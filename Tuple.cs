using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper04
{
    public struct Tuple
    {
        public Tuple(int inX, int inY)
        {
            x = inX;
            y = inY;
        }
        public int x { get; set; }
        public int y { get; set; }
        public override string ToString()
        {
            return $"{x}:{y}";
        }
    }
}
