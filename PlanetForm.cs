﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntColony
{
    public partial class PlanetForm : Form
    {
        private PlanetAgent _ownerAgent;
        private Bitmap _doubleBufferImage;

        public PlanetForm()
        {
            InitializeComponent();
        }

        public void SetOwner(PlanetAgent a)
        {
            _ownerAgent = a;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawPlanet();
        }

        public void UpdatePlanetGUI()
        {
            DrawPlanet();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            DrawPlanet();
        }

        private void DrawPlanet()
        {
            // force the window to be sized as Utils.SizeX and Utils.SizeY
            Width = Utils.SizeX;
            Height = Utils.SizeY;
            
            // force the picture box to use Utils.SizeX and Utils.SizeY as size
            pictureBox.Width = Utils.SizeX;
            pictureBox.Height = Utils.SizeY;
            

            if (_doubleBufferImage != null)
            {
                _doubleBufferImage.Dispose();
                GC.Collect(); // prevents memory leaks
            }

            _doubleBufferImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            var g = Graphics.FromImage(_doubleBufferImage);
            g.Clear(Color.White);

            if (_ownerAgent == null)
                return;
            
            
            // Draw all edges in the graph
            foreach (var edge in _ownerAgent.Edges.Values)
            {
                DrawEdge(g, edge, Brushes.Black);
            }
                
            // Draw all nodes in the graph
            foreach (var node in _ownerAgent.Nodes.Values)
            {
                DrawNode(g, node, Brushes.Black);
            }
            
            // draw the base node
            DrawBase(g, _ownerAgent.BaseNode);

            // display the buffer image 
            Graphics pbg = pictureBox.CreateGraphics();
            pbg.DrawImage(_doubleBufferImage, 0, 0);
        }

        void DrawBase(Graphics g, Node nodeBase)
        {
            DrawNode(g, nodeBase, Brushes.Red);
        }
        
        void DrawNode(Graphics g, Node node, Brush brush)
        {
            // Draw the node
            g.FillEllipse(brush,node._x, node._y, 10, 10 );
        }
        
        void DrawEdge(Graphics g, Edge edge, Brush brush)
        {
            // Draw the edge
            g.DrawLine(Pens.DarkGray, edge.NodeA._x, edge.NodeA._y, edge.NodeB._x, edge.NodeB._y);
        }
    }
}