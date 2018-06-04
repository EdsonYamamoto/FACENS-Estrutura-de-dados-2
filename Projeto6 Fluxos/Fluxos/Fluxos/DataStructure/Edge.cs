using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxos.DataStructure
{
    public class Edge
    {

        private Node from, to;
        private double value;

        public Node From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        public Node To
        {
            get
            {
                return to;
            }

            set
            {
                to = value;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public Edge(Node from, Node to, double value)
        {
            this.from = from;
            this.to = to;
            this.value = value;
        }

    }
}
