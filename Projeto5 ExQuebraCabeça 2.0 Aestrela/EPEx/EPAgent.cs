using System;
using System.Collections.Generic;
using System.Linq;
using EP.DataStructure;
using ProjetoGrafos.DataStructure;

namespace EP
{
    /// <summary>
    /// EPAgent - searchs solution for the eight puzzle problem
    /// </summary>
    public class EightPuzzle : Graph
    {
        private int tamLado;
        private int[] initState;
        private int[] target;
        private HashSet<string> caminhosEncontrados = new HashSet<string>();

        /// <summary>
        /// Creating the agent and setting the initialstate plus target
        /// </summary>
        /// <param name="InitialState"></param>
        public EightPuzzle(int[] InitialState, int[] Target)
        {
            initState = InitialState;
            target = Target;
            tamLado = Convert.ToInt32(Math.Sqrt(InitialState.Count()));
        }

        /// <summary>
        /// Accessor for the solution
        /// </summary>
        public int[] GetSolution()
        {
            return FindSolution();
        }

        /// <summary>
        /// Função principal de busca
        /// </summary>
        /// <returns></returns>
        private int[] FindSolution()
        {
            Stack<DadosPrioridade> filaPrioridade = new Stack<DadosPrioridade>();
            DadosPrioridade dadosDoNoPrioridade = new DadosPrioridade();
            Node noInicial = new Node(GetAName(initState), initState, 0);

            dadosDoNoPrioridade.nivel = noInicial.Nivel;
            dadosDoNoPrioridade.avaliacaoEstado = FuncAvaliacao(noInicial);
            dadosDoNoPrioridade.no = noInicial;

            filaPrioridade.Push(dadosDoNoPrioridade);
            caminhosEncontrados.Add(noInicial.Name);
            List<Node> sucessores = null;

            while (filaPrioridade.Count > 0)
            {
                
                dadosDoNoPrioridade = filaPrioridade.Pop();
                Node n = dadosDoNoPrioridade.no;

                if (TargetFound((int[])n.Info))
                {
                    return BuildAnswer(n);
                }

                sucessores = RetornaSucessores(n);
                foreach (Node node in sucessores)
                {
                    if (!caminhosEncontrados.Contains(node.Name))
                    {
                        caminhosEncontrados.Add(node.Name);
                        dadosDoNoPrioridade = new DadosPrioridade();
                        dadosDoNoPrioridade.nivel = noInicial.Nivel;
                        dadosDoNoPrioridade.avaliacaoEstado = FuncAvaliacao(node);
                        dadosDoNoPrioridade.no = node;
                        filaPrioridade.Push(dadosDoNoPrioridade);
                    }
                }
            }

            return null;
        }

        private string GetAName(int[] info)
        {
            string name = "";
            for (int i = 0; i < info.Length; ++i)
            {
                name += info[i].ToString() + ",";
            }
            name += "#";

            return name.Replace(",#", "");
        }

        private int RetornaPosicaoVazia(int[] info)
        {
            foreach (int i in info)
                if (info[i] == 0) return i;

            return 0;
        }

        private List<Node> RetornaSucessores(Node n)
        {
            List<Node> lstSucessores = new List<Node>();

            int posicaoVazia = RetornaPosicaoVazia((int[])n.Info);

            // Peça vazia para a esquerda
            if (posicaoVazia % tamLado != 0)
            {
                int[] novoSucessor = new int[tamLado * tamLado];
                Array.Copy((int[])n.Info, novoSucessor, tamLado * tamLado);

                novoSucessor[posicaoVazia - 1] = novoSucessor[posicaoVazia];
                novoSucessor[posicaoVazia] = ((int[])n.Info)[posicaoVazia - 1];

                Node aux = new Node(GetAName(novoSucessor), novoSucessor, n.Nivel + 1);
                aux.AddEdge(n, novoSucessor[posicaoVazia]);
                lstSucessores.Add(aux);
            }
            // Peça vazia para a direita
            if (posicaoVazia % tamLado != tamLado - 1)
            {
                int[] novoSucessor = new int[tamLado * tamLado];
                Array.Copy((int[])n.Info, novoSucessor, tamLado * tamLado);

                novoSucessor[posicaoVazia + 1] = novoSucessor[posicaoVazia];
                novoSucessor[posicaoVazia] = ((int[])n.Info)[posicaoVazia + 1];

                Node aux = new Node(GetAName(novoSucessor), novoSucessor, n.Nivel + 1);
                aux.AddEdge(n, novoSucessor[posicaoVazia]);
                lstSucessores.Add(aux);
            }
            // Peça vazia para cima
            if ((posicaoVazia / tamLado) > 0)
            {
                int[] novoSucessor = new int[tamLado * tamLado];
                Array.Copy((int[])n.Info, novoSucessor, tamLado * tamLado);

                novoSucessor[posicaoVazia - tamLado] = novoSucessor[posicaoVazia];
                novoSucessor[posicaoVazia] = ((int[])n.Info)[posicaoVazia - tamLado];

                Node aux = new Node(GetAName(novoSucessor), novoSucessor, n.Nivel + 1);
                aux.AddEdge(n, novoSucessor[posicaoVazia]);
                lstSucessores.Add(aux);
            }
            // Peça vazia para baixo
            if ((posicaoVazia / tamLado) < tamLado - 1)
            {
                int[] novoSucessor = new int[tamLado * tamLado];
                Array.Copy((int[])n.Info, novoSucessor, tamLado * tamLado);

                novoSucessor[posicaoVazia + tamLado] = novoSucessor[posicaoVazia];
                novoSucessor[posicaoVazia] = ((int[])n.Info)[posicaoVazia + tamLado];

                Node aux = new Node(GetAName(novoSucessor), novoSucessor, n.Nivel + 1);
                aux.AddEdge(n, novoSucessor[posicaoVazia]);
                lstSucessores.Add(aux);
            }

            return lstSucessores;
        }

        private int[] BuildAnswer(Node n)
        {
            List<int> resultado = new List<int>();

            while (n.Edges.Count > 0)
            {
                resultado.Add((int)n.Edges[0].Cost);
                n = n.Edges[0].To;
            }

            resultado.Reverse();

            return resultado.ToArray();
        }

        private bool TargetFound(int[] info)
        {
            return info.SequenceEqual(target);
        }

        private int FuncAvaliacao(Node n)
        {
            int[] blocoAtual = (int[])n.Info;
            int totalErro = 0;

            for (int i = 0; i < tamLado * tamLado; i++)
            {
                for (int j = 0; j < tamLado * tamLado; j++)
                {
                    if (blocoAtual[i] != target[j]) continue;

                    totalErro += Math.Abs(i % tamLado - j % tamLado) + Math.Abs(i / tamLado - j / tamLado);
                    break;
                }
            }

            return totalErro;
        }
    }
}