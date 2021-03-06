﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Glee = Microsoft.Glee.Drawing;
using GleeUI = Microsoft.Glee.GraphViewerGdi;
using EDA = ProjetoGrafos.DataStructure;

namespace ProjetoGrafos
{
    /// <summary>
    /// Classe que representa o form principal da aplicação.
    /// </summary>
    public partial class Principal : Form
    {

        #region Atributos

        /// <summary>
        /// Instância do grafo a ser utilizada no formulário.
        /// </summary>
        private EDA.Graph graph;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do form principal.
        /// </summary>
        public Principal()
        {
            InitializeComponent();
            this.graph = new EDA.Graph();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Atribui aos controles o conteúdo do grafo.
        /// </summary>
        private void SetGraphControls(EDA.Graph graphToDraw, bool drawGraph = true)
        {
            List<EDA.Edge> edges = new List<EDA.Edge>();
            EDA.Node[] nodes = graphToDraw.Nodes;
            // Limpa controles..
            txtNodeName.Clear();
            cmbNodeFrom.Items.Clear();
            cmbNodeTo.Items.Clear();
            cmbNodeNeighbourhood.Items.Clear();
            lstArcs.Items.Clear();
            // Carrega nós e agrupa arcos..
            foreach (EDA.Node node in nodes)
            {
                edges.AddRange(node.Edges);
                // Adiciona nós ao combo..
                cmbNodeFrom.Items.Add(node);
                cmbNodeTo.Items.Add(node);
                cmbNodeNeighbourhood.Items.Add(node);
            }
            // Adiciona os arcos ao listbox..
            foreach (EDA.Edge edge in edges)
            {
                lstArcs.Items.Add(edge);
            }
            if (drawGraph)
            {
                DrawGraph(graphToDraw, null);
            }
        }

        /// <summary>
        /// Desenha o grafo atual.
        /// </summary>
        private void DrawGraph(EDA.Graph graphToDraw, EDA.Node[] highlightedNodes)
        {
            List<EDA.Edge> edges = new List<EDA.Edge>();
            Glee.Graph drawingGraph = new Glee.Graph("Grafo - EDA2");
            // Adiciona nós ao grafo..
            foreach (EDA.Node node in graphToDraw.Nodes)
            {
                Glee.Node drawingNode = drawingGraph.AddNode(node.Name);
                drawingNode.Attr.Shape = Glee.Shape.Circle;
                if (highlightedNodes != null && Array.IndexOf(highlightedNodes, node) >= 0)
                {
                    drawingNode.Attr.Color = Glee.Color.Red;
                }
                // Consolida os arcos..
                edges.AddRange(node.Edges);
            }
            foreach (EDA.Edge edge in edges)
            {
                Glee.Edge drawingEdge = drawingGraph.AddEdge(edge.From.Name, edge.To.Name);
                drawingEdge.Attr.Label = String.Format("{0:F4}", edge.Cost);
            }
            // Gera controle de desenho..
            GleeUI.GViewer viewer = new GleeUI.GViewer();
            viewer.NavigationVisible = false;
            viewer.OutsideAreaBrush = Brushes.White;
            viewer.RemoveToolbar();
            viewer.Graph = drawingGraph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlGraph.Controls.Clear();
            pnlGraph.Controls.Add(viewer);
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Clique do botão AddNode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNodeName.Text.Trim()))
            {
                this.graph.AddNode(txtNodeName.Text);
            }
            SetGraphControls(this.graph);
        }

        /// <summary>
        /// Clique do botão remover nó.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNodeName.Text.Trim()))
            {
                this.graph.RemoveNode(txtNodeName.Text);
            }
            SetGraphControls(this.graph);
        }

        /// <summary>
        /// Clique do botão adicionar arco.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddArc_Click(object sender, EventArgs e)
        {
            if (cmbNodeTo.SelectedItem != null && cmbNodeFrom.SelectedItem != null && !String.IsNullOrEmpty(txtCost.Text.Trim()))
            { 
                string nodeTo = (cmbNodeTo.SelectedItem as EDA.Node).Name;
                string nodeFrom = (cmbNodeFrom.SelectedItem as EDA.Node).Name;
                double cost = Double.Parse(txtCost.Text);
                // Adiciona arco..
                this.graph.AddEdge(nodeTo, nodeFrom, cost);
                this.graph.AddEdge(nodeFrom, nodeTo, cost);
            }
            SetGraphControls(this.graph);
        }

        /// <summary>
        /// Clique do botão que limpa arcos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveArcs_Click(object sender, EventArgs e)
        {
            // Adiciona nós ao grafo..
            foreach (EDA.Node node in this.graph.Nodes)
            {
                node.Edges.Clear();
            }
            SetGraphControls(this.graph);
        }

        /// <summary>
        /// Clique do botão de exibir vizinhos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowNeighbours_Click(object sender, EventArgs e)
        {
            if (cmbNodeNeighbourhood.SelectedItem != null)
            {
                string nodeFrom = (cmbNodeNeighbourhood.SelectedItem as EDA.Node).Name; 
                EDA.Node[] neighbours = this.graph.GetNeighbours(nodeFrom);
                SetGraphControls(this.graph, false);
                DrawGraph(this.graph, neighbours);
            }
        }

        /// <summary>
        /// Clique do botão de validar caminhos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPath_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPath.Text.Trim()))
            {
                EDA.Node[] pathNodes = null;
                string[] path = txtPath.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                // Verifica se o caminho existe..
                if (this.graph.IsValidPath(ref pathNodes, path))
                {
                    MessageBox.Show("O caminho é válido", "Validação de Caminho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("O caminho é inválido", "Validação de Caminho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SetGraphControls(this.graph, false);
                DrawGraph(this.graph, pathNodes);
            }
        }

        #endregion

        private void btnPrim_Click(object sender, EventArgs e)
        {
            EDA.Graph primGraph = this.graph.Prim();
            if (primGraph != null)
            {
                SetGraphControls(primGraph);
            }
        }

        private void btnKruskal_Click(object sender, EventArgs e)
        {
            EDA.Graph kruskalGraph = this.graph.Kruskal();
            if (kruskalGraph != null)
            {
                SetGraphControls(kruskalGraph);
            }
        }

        private void btnOriginal_Click(object sender, EventArgs e)
        {
            SetGraphControls(this.graph);
        }

        private void button1_Click(object sender, EventArgs e)
        {

//            if (!String.IsNullOrEmpty(txtNodeName.Text.Trim()))
//           {
            this.graph.AddNode("1");
            this.graph.AddNode("2");
            this.graph.AddNode("3");
            this.graph.AddNode("4");
            this.graph.AddNode("5");

            this.graph.AddEdge("1", "2", 50);
            this.graph.AddEdge("2", "1", 50);
            this.graph.AddEdge("1", "3", 30);
            this.graph.AddEdge("3", "1", 30);
            this.graph.AddEdge("4", "1", 5);
            this.graph.AddEdge("1", "4", 5);
            this.graph.AddEdge("5", "1", 3);
            this.graph.AddEdge("1", "5", 3);
            this.graph.AddEdge("2", "3", 45);
            this.graph.AddEdge("3", "2", 45);
            this.graph.AddEdge("2", "4", 90);
            this.graph.AddEdge("4", "2", 90);
            this.graph.AddEdge("2", "5", 7);
            this.graph.AddEdge("5", "2", 7);
            this.graph.AddEdge("3", "4", 9);
            this.graph.AddEdge("4", "3", 9);
            this.graph.AddEdge("3", "5", 10);
            this.graph.AddEdge("5", "3", 10);
            this.graph.AddEdge("4", "5", 13);
            this.graph.AddEdge("5", "4", 13);
            //            }
            SetGraphControls(this.graph);
        }
    }
}
