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
            foreach (Node a in nodes)
            {
                if (a.Name == name)
                    return a;
            }

            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            Node n = Find(name);
            if(n==null)
                this.nodes.Add(new Node(name,null));
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            Node n = Find(name);
            if (n == null)
                this.nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node n = Find(name);
            
                foreach(Node no in nodes)
                {
                    for(int i = 0; i < no.Edges.Count; i++)
                    {
                        Console.WriteLine(i);
                        if (no.Edges[i].From == n || no.Edges[i].To == n)
                        {
                            no.Edges.Remove(no.Edges[i]);
                        i--;
                        }
                    }
                }
            
            nodes.Remove(n);
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Find(from).AddEdge(Find(to), cost);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node[] nos = new Node[0];

            foreach (Node n in nodes)
            {
                foreach (Edge e in n.Edges)
                {
                    if (e.To.Name == from)
                    {
                        Array.Resize(ref nos, nos.Length + 1);
                        nos[nos.Length - 1] = n;
                    }
                }
            }
            return nos;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            bool passou;
            Node n;
            for (int i = 0; i < path.Length-1; i++)
            {
                passou = false;
                foreach(Node nozinho in GetNeighbours(path[i + 1]))
                {

                    if (Find(path[i]) == nozinho)
                    {
                        passou = true;
                        Array.Resize(ref nodes, Nodes.Length + 1);
                        nodes[nodes.Length - 1] = nozinho;
                    }

                }
                if (!passou)
                    return false;
            }
            return true;
            
        }

        #endregion

    }
}
