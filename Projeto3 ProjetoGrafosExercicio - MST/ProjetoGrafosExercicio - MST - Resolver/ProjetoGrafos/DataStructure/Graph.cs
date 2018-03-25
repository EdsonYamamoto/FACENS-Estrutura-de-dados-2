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
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        private Node Find(string name)
        {
            Node n = null;
            foreach (Node node in nodes)
            {
                if (node.Name == name)
                    n = node;
            }
            return n;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            if(Find(name)==null)
                AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node n = Find(name);
            if (n != null)
            {
                nodes.Remove(n);
                foreach (Node node in nodes)
                {
                    for (int i = 0; i < node.Edges.Count; i++)
                    {
                        Edge e = node.Edges[i];
                        if (e.To.Name == name)
                        {
                            node.Edges.Remove(e);
                            i--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node nFrom = Find(from);
            Node nTo = Find(to);
            if (nFrom != null && nTo != null)
            {
                nFrom.AddEdge(nTo, cost);
            }
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node n = Find(from);
            Node[] neighbours = null;
            if (n != null)
            {
                neighbours = new Node[n.Edges.Count];
                int i = 0;
                foreach (Edge e in n.Edges)
                {
                    neighbours[i++] = e.To;
                }
            }
            return neighbours;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            bool valid = true;
            if (path.Length > 0)
            {
                List<Node> pathNodes = new List<Node>();
                Node n = Find(path[0]);
                if (n == null)
                    return false;
                for (int i = 1; i < path.Length; i++)
                {
                    Node[] neighbours = GetNeighbours(n.Name);
                    foreach (Node node in neighbours)
                    {
                        if (node.Name == path[i])
                            n = node;
                    }
                    if (n.Name != path[i])
                        return false;
                }
            }
            return valid;
        }

        public Graph Prim()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(this.nodes.Count);
            return Prim(this.nodes[index].Name);
        }

        public Graph Kruskal()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int index = rnd.Next(this.nodes.Count);
            return Kruskal(this.nodes[index].Name);
        }
        /*
         o algoritmo de prim pega um vertice aleatorio e através dele armazena-se a aresta com menor valor
         sendo feito isso até o grafo novo estar com o mesmo tamanho
        */

        public Graph Prim(string name)
        {
            Graph arvore = new Graph();
            Node no = null;
            Random rdn = new Random();
            Edge arestaMenorCusto = null;
            List<Node> listaNos = new List<Node>();
            List<Edge> listaArestas = new List<Edge>();
            int contadorNos=-1;

            no = this.nodes[rdn.Next(this.nodes.Count)];
            arvore.AddNode(no.Name);
            listaNos.Add(no);
            if (no.Edges.Count != 0)
            {
                do{
                    foreach (Node n in listaNos)
                    {
                        if (n.Edges.Count != 0)
                        {
                            no = this.Find(n.Name);
                            foreach (Edge e in n.Edges)
                            {
                                if (arestaMenorCusto == null)
                                {
                                    arestaMenorCusto = e;
                                }
                                if (e.Cost < arestaMenorCusto.Cost && !listaNos.Contains(e.To))
                                {
                                    arestaMenorCusto = e;
                                }
                            }
                        }
                    }
                    if (!listaNos.Contains(arestaMenorCusto.To))
                    {
                        listaArestas.Add(arestaMenorCusto);
                        listaNos.Add(arestaMenorCusto.To);
                        arvore.AddNode(arestaMenorCusto.To.Name);
                        arvore.AddEdge(arestaMenorCusto.From.Name, arestaMenorCusto.To.Name, arestaMenorCusto.Cost);
                    }
                    contadorNos++;
                    arestaMenorCusto = null;
                } while (arvore.nodes.Count != contadorNos);
            }
            return arvore;
        }
        /*
            algoritmo de kruskal se baseia em guardar primeiro a aresta de menor custo.
            procurar pela proxima aresta de menor custo e gravar na arvore.
        */
        public Graph Kruskal(string name)
        {
            Graph arvore = new Graph();
            Edge arestaMenorCusto=null;
            List<Edge> listaArestas = new List<Edge>();
            List<Edge> listaMenorCustoArestas = new List<Edge>();
            foreach (Node n in this.nodes)
                foreach (Edge e in n.Edges)
                    listaArestas.Add(e);
            
            foreach (Node n in this.nodes)
                arvore.AddNode(n.Name);

            while(listaArestas.Count!= 0)
            {
                arestaMenorCusto = listaArestas[0];
                foreach (Edge e in listaArestas)
                    if (e.Cost < arestaMenorCusto.Cost)
                        arestaMenorCusto = e;
                listaMenorCustoArestas.Add(arestaMenorCusto);
                listaArestas.Remove(arestaMenorCusto);
            }
            
            foreach (Edge e in listaMenorCustoArestas)
            {
                if(arvore.Find(e.From.Name).Edges.Count==0)
                arvore.AddEdge(e.From.Name, e.To.Name, e.Cost);
            }
            
            return arvore;
        }
        
        #endregion

    }
}
