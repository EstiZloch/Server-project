using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpltyTravelBLL
{
    class Node
    {
        public int Name { get; private set; }
        public double Value { get; set; }
        public Node PreviousNode { get; set; }

        public Node(int code, double value = int.MaxValue, Node previousNode = null)
        {
            this.Name = code;
            this.Value = value;
            this.PreviousNode = previousNode;
        }
    }
}
