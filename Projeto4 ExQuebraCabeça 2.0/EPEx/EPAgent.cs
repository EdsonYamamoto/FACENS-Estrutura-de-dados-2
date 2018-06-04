using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using ProjetoGrafos.DataStructure;

namespace EP
{
    /// <summary>
    /// EPAgent - searchs solution for the eight puzzle problem
    /// </summary>
    public class EightPuzzle : Graph
    {
        private int[] initState;
        private int[] target;
        private int tamanho;
        private Dictionary<string, Node> dictNos = new Dictionary<string, Node>();

        /// <summary>
        /// Creating the agent and setting the initialstate plus target
        /// </summary>
        /// <param name="InitialState"></param>
        public EightPuzzle(int[] InitialState, int[] Target, int tamanho)
        {
            initState = InitialState;
            target = Target;
            this.tamanho = tamanho;
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
            Queue<Node> fila = new Queue<Node>();
            Node n0 = new Node(getName(initState), initState, 0);
            List<Node> lstSucesso = null;

            fila.Enqueue(n0);

            while (fila.Count > 0)
            {
                Node n = fila.Dequeue();

                if (!dictNos.ContainsKey(n.Name))
                {
                    dictNos.Add(n.Name, n);

                    if (n.Name == getName(target)) return BuildAnswer(n);

                    lstSucesso = GetSucessors(n);

                    if (lstSucesso != null)
                        foreach (Node node in lstSucesso) fila.Enqueue(node);
                }
            }

            return null;

        }

        private List<Node> GetSucessors(Node n)
        {
            List<Node> retorno = new List<Node>();
            int posicao_branco = BuscaBranco((int[])n.Info);

            int x = posicao_branco / tamanho;
            int y = posicao_branco % tamanho;

            if (x - 1 >= 0)
            {
                int[] v = (int[])((int[])n.Info).Clone();
                int pos_troca = (x - 1) * tamanho + y;

                v[posicao_branco] = v[pos_troca];
                v[pos_troca] = 0;

                Node novo = new Node(getName(v), v,0);
                novo.AddEdge(n, v[posicao_branco]);

                retorno.Add(novo);
            }

            if (x + 1 < tamanho)
            {
                int[] v = (int[])((int[])n.Info).Clone();
                int pos_troca = (x + 1) * tamanho + y;

                v[posicao_branco] = v[pos_troca];
                v[pos_troca] = 0;

                Node novo = new Node(getName(v), v, 0);
                novo.AddEdge(n, v[posicao_branco]);

                retorno.Add(novo);
            }

            if (y - 1 >= 0)
            {
                int[] v = (int[])((int[])n.Info).Clone();
                int pos_troca = (y - 1) + (tamanho * x);

                v[posicao_branco] = v[pos_troca];
                v[pos_troca] = 0;

                Node novo = new Node(getName(v), v, 0);
                novo.AddEdge(n, v[posicao_branco]);

                retorno.Add(novo);
            }

            if (y + 1 < tamanho)
            {
                int[] v = (int[])((int[])n.Info).Clone();
                int pos_troca = (y + 1) + (tamanho * x);

                v[posicao_branco] = v[pos_troca];
                v[pos_troca] = 0;

                Node novo = new Node(getName(v), v, 0);
                novo.AddEdge(n, v[posicao_branco]);

                retorno.Add(novo);
            }

            return retorno;
            //throw new NotImplementedException();
        }

        private string getName(int[] posicao)
        {
            string ret = "";

            foreach (int i in posicao) ret += i.ToString();

            return ret;
        }

        private int BuscaBranco(int[] posicao)
        {
            foreach (int i in posicao)
                if (posicao[i] == 0) return i;

            return 0;
        }

        private int[] BuildAnswer(Node n)
        {
            string result = "";

            while (n.Edges.Count > 0)
            {
                result = n.Edges[0].Cost.ToString() + ";" + result;
                n = n.Edges[0].To;
            }

            string[] splitResult = result.Split(';');
            int[] v = new int[splitResult.Length];

            for (int i = 0; i < splitResult.Length - 1; i++)
                v[i] = Convert.ToInt32(splitResult[i]);

            return v;
        }

        private bool TargetFound(Node n)
        {
            throw new NotImplementedException();
        }
    }
}

