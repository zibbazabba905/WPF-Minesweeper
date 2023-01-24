using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Sweeper04
{
    public class GridSizes
    {
        public List<GridSizeComponent> Sizes{ get; set; }

        public GridSizes()
        {
            Sizes = new List<GridSizeComponent>();
            Sizes.Add(MakeSize("Small", 10, 10));
            Sizes.Add(MakeSize("Medium", 20, 20));
            Sizes.Add(MakeSize("Large", 40, 30));
        }
        private GridSizeComponent MakeSize(string name, int Xin, int Yin)
        {
            Tuple size = new Tuple(Xin, Yin);
            GridSizeComponent local = new GridSizeComponent()
            {
                Name = name,
                Dimensions = size,
            };
            return local;
        }
    }
}
