using Fluxos.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxos.controler
{
    class FluxoMax
    {
        private static Graph g;

        /// <summary>
        /// função base que retorna o fluxo máximo do grafo
        /// </summary>
        /// <param name="grafo"></param>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public static double MaxFlow(Graph grafo, string inicio, string fim)
        {
            double maxFlow = 0, minFlow = 0;
            g = grafo;
            List<Edge> caminho;

            while ((caminho = BuscaLargura(Find(inicio), Find(fim))) != null)
            {
                minFlow = MinRemaing(caminho);
                maxFlow += minFlow;

                AtualizaGrafo(caminho, minFlow);
            }

            return maxFlow;
        }

        /// <summary>
        /// Função que retorna um determinado nó do grafo
        /// </summary>
        private static Node Find(string name)
        {
            Node n = null;
            foreach (Node no in g.Nodes)
                if (no.Name == name)
                {
                    return no;
                }
            return n;
        }


        /// <summary>
        /// seta os itens que foram visitados a partir de um nó especifico
        /// </summary>
        /// <param name="value"></param>
        private static void SetVisitedAs(bool value)
        {
            foreach (Node no in g.Nodes)
                foreach (Edge e in no.Edges)
                    e.To.Visited = value;
        }

        /// <summary>
        /// Retira todos os parents dos nos
        /// </summary>
        private static void SetParentAsNULL()
        {
            foreach (Node no in g.Nodes)
                no.Parent = null;
        }

        /// <summary>
        /// realiza a busca para encontrar o fluxo máximo
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public static List<Edge> BuscaLargura(Node inicio, Node fim)
        {
            Queue<Node> fila = new Queue<Node>();

            inicio.Visited = true;
            fila.Enqueue(inicio);

            BuscaLargura(fila, fim);
            SetVisitedAs(false);

            List<Edge> listaCaminho = ConvertCaminho(fim);

            SetParentAsNULL();

            return listaCaminho;
        }

        /// <summary>
        /// Busca em largura pega o menor caminho.
        /// Capacidade máxima de um caminho.
        /// Realizar a busca em ordem alfabetica.
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="fim"></param>
        private static void BuscaLargura(Queue<Node> fila, Node fim)
        {
            Node node = fila.Dequeue();
            if (node.Name.Equals(fim.Name)) return;

            //para cada lista de nós ordenar pelo valor da aresta e executa uma nova ordenação dos valores com o novo nó
            foreach (Edge edge in node.Edges.OrderByDescending(e => e.Value).ThenBy(e => Convert.ToInt32(e.To.Name)))
            {
                if (!edge.To.Visited)
                {
                    edge.To.Visited = true;
                    edge.To.Parent = node;
                    fila.Enqueue(edge.To);
                }
            }

            if (fila.Count == 0)
                return;
            BuscaLargura(fila, fim);
        }

        /// <summary>
        /// retornar a lista de arestas do caminho por onde percorreu 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static List<Edge> ConvertCaminho(Node n)
        {
            if (n.Parent == null)
                return null;

            List<Edge> listaCaminho = new List<Edge>();

            while (n.Parent != null)
            {
                listaCaminho.Add(n.Parent.Edges.Single(e => e.To.Name.Equals(n.Name)));
                n = n.Parent;
            }

            return listaCaminho;
        }

        /// <summary>
        /// pega o caminho e retorna o meno custo da aresta como fluxo minimo do caminho
        /// </summary>
        /// <param name="listaCaminho"></param>
        /// <returns></returns>
        private static double MinRemaing(List<Edge> listaCaminho)
        {
            double min = Double.MaxValue;

            listaCaminho.ForEach(e =>
            {
                if (e.Value < min)
                    min = e.Value;
            });

            return min;
        }

        /// <summary>
        /// Rescreve o grafo com as informações obtidas pelo caminho e o fluxo minimo do caminho
        /// </summary>
        /// <param name="listaCaminho"></param>
        /// <param name="minFlow"></param>
        private static void AtualizaGrafo(List<Edge> listaCaminho, double minFlow)
        {
            foreach (Edge c in listaCaminho)
            {
                foreach (Node n in g.Nodes)
                {
                    Edge aux = n.Edges.SingleOrDefault(e => e.Equals(c));
                    if (aux == null)
                        continue;

                    aux.Value -= minFlow;
                    aux.To.AddEdge(aux.From, minFlow);

                    if (aux.Value == 0)
                        aux.From.Edges.Remove(aux);
                    

                    break;
                };
            }
        }
    }
}
