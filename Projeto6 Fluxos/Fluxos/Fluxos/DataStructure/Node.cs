using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxos.DataStructure
{
    public class Node
    {

        private string name;
        private object info;
        private List<Edge> edges;
        private bool visited;
        private Node parent;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Node Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }
        public Boolean Visited
        {
            get
            {
                return visited;
            }

            set
            {
                visited = value;
            }
        }
        public object Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }

        public List<Edge> Edges
        {
            get
            {
                return edges;
            }

            set
            {
                edges = value;
            }
        }

        public Node(string name, object info)
        {
            this.edges = new List<Edge>();
            this.name = name;
            this.info = info;
        }

        public void AddEdge(Node n, double value)
        {
            Edge e = new Edge(this, n, value);
            edges.Add(e);
        }

    }
}
