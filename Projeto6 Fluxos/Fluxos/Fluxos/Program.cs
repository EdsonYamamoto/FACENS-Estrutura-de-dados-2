using Fluxos.controler;
using Fluxos.DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxos
{
    class Program
    {
        static void Main(string[] args)
        {
            bool modoCompleto = false;

            string[] dimensoes = new string[2];
            string[] aresta = new string[3];
            string[] resultado = new string[2];
            int n, m;

            string n1, n2;
            double cost;
            Graph g = new Graph();
            if (modoCompleto)
            {
                Console.WriteLine("Escreva a quantidade de Nós :");
                n = armazenaInt();
                Console.WriteLine("Escreva a quantidade de Arestas :");
                m = armazenaInt();
                for (int i = 0; i < n; i++)
                    g.AddNode(i.ToString());

                for (int i = 0; i < m; i++)
                {

                    Console.WriteLine("Escolha o nó de partida de 0 até " + n + " :");
                    n1 = Console.ReadLine();
                    Console.WriteLine("Escolha o nó de partida de 0 até " + n + " :");
                    n2 = Console.ReadLine();
                    Console.WriteLine("Coloque o custo :");
                    cost = armazenaInt();

                    g.AddEdge(n1, n2, cost);
                }

                Console.WriteLine("Escolha o nó de inicio: ");
                resultado[0] = Console.ReadLine();
                Console.WriteLine("Escolha o nó de fim:");
                resultado[1] = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Escreva a quantidade de Nós e arestas separados por espaço:\n");
                dimensoes = Console.ReadLine().Split(' ');
                n = Convert.ToInt32(dimensoes[0]);
                m = Convert.ToInt32(dimensoes[1]);
                g = new Graph();
                for (int i = 0; i < n; i++)
                {
                    g.AddNode(i.ToString());
                }
                for (int i = 0; i < m; i++)
                {
                    Console.WriteLine("Escreva a nó from, nó to e cost separados por espaço:\n");
                    aresta = Console.ReadLine().Split(' ');
                    double value = Convert.ToInt32(aresta[2]);
                    g.AddEdge(aresta[0], aresta[1], value);
                }
                Console.WriteLine("Escreva nó inicial e nó final\n");
                resultado = Console.ReadLine().Split(' ');

            }
            double maxFlow = FluxoMax.MaxFlow(g, resultado[0], resultado[1]);
            Console.WriteLine(maxFlow);
            Console.ReadLine();
        }

        private static int armazenaInt()
        {
            string valor = Console.ReadLine();
            if (!int.TryParse(valor, out int n))
                throw new System.ArgumentException("Parametro precisa ser int", "falta ser inteiro");
            return n;
        }
    }
}
