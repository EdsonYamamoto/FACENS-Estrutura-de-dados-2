using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        private Stack<Node> pilha;
        private Queue<Node> fila;
        private List<Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.fila = new Queue<Node>();
            this.pilha = new Stack<Node>();
            this.nodes = new List<Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>não encontre nada.</retur
        /// <returns>O nó encontrado ou nulo caso ns>
        private Node Find(string name)
        {
            foreach (Node a in this.nodes)
            {
                if (a.Name == name)
                    return a;
            }

            return null;
        }

        public List<Node> ShortestPath(string begin, string end)
        {
            return null;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            Node no = Find(begin);
            foreach (Node n in nodes)
                n.Visited = false;
            no.Visited = true;
            List<Node> nos = new List<Node>();
            nos.Add(no);
            fila.Enqueue(no);

            while (fila.Count != 0)
            {
                no = fila.Dequeue();
                foreach (Edge e in no.Edges)
                {
                    if (e.To.Visited == false)
                    {
                        nos.Add(e.To);
                        fila.Enqueue(e.To);
                        e.To.Visited = true;
                    }
                }
            }

            /*
            foreach (Node noz in nos)
            {
                //Console.WriteLine(noz.Name+" "+noz.Info);
                if (noz.Info.ToString() == "S")
                    
            }
            */
            
            return nos;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            Node no = Find(begin);
            pilha.Push(no);
            no.Visited = true;
            List<Node> nos = new List<Node>();
            nos.Add(no);
            

            while (pilha.Count > 0)
            {
                no = pilha.Pop();
                nos.Add(no);
                foreach (Edge e in no.Edges)
                {
                    if (!e.To.Visited)
                    {
                        e.To.Visited = true;
                        pilha.Push(e.To);
                    }
                }
            }


            return nos;
        }
        
        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            this.nodes.Add(new Node(name, info));
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node noVem = Find(nameFrom);
            Node noVai = Find(nameTo);

            noVem.AddEdge(noVai, cost);
        }

        #endregion
    }
}
