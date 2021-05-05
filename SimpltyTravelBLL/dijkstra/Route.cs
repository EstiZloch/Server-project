using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpltyTravelBLL.dijkstra
{
    class Route
    {
        public int From { get; private set; }
        public int To { get; private set; }
        public double Distance { get; private set; }

        public Route(int from, int to, double distance)
        {
            this.From = from;
            this.To = to;
            this.Distance = distance;
        }
    }
}
