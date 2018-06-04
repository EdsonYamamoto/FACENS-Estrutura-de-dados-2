using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxos.DataStructure
{
    public class Graph
    {

        private List<Node> nodes;

        public List<Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        public Graph()
        {
            nodes = new List<Node>();
        }

        public Node AddNode(string name, object info)
        {
            Node n = new Node(name, info);
            nodes.Add(n);
            return n;
        }

        public Node AddNode(string name)
        {
            return AddNode(name, null);
        }

        public void AddEdge(string n1, string n2, double value)
        {
            Node node1 = Find(n1);
            Node node2 = Find(n2);
            node1.AddEdge(node2, value);
        }

        public Node Find(string name)
        {
            return nodes.Find(n => n.Name == name);
        }

           

    }
}
