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
            return null;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            Node noInicio = Find(begin);
            List<Node> lista = new List<Node>();
            vaiProfundo(lista, noInicio);
            return lista;
        }

        public void vaiProfundo(List<Node> lista, Node n)
        {
            n.Visited = true;
            lista.Add(n);
            foreach (Edge e in n.Edges)
            {
                if (e.To.Visited != true)
                {
                    vaiProfundo(lista, e.To);
                }
            }
            n.Visited = true;
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
