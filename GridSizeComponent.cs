using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeper04
{
    public struct GridSizeComponent
    {
        public GridSizeComponent(string inName, Tuple inDimensions)
        {
            Name = inName;
            Dimensions = inDimensions;
        }
        public string Name { get; set; }
        public Tuple Dimensions { get; set; }
        public override string ToString()
        {
            return $"{Name}, {Dimensions.x}:{Dimensions.y}" + base.ToString();
        }
    }
}
