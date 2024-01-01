using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntColony
{
    public partial class PlanetForm : Form
    {
        private Bitmap _doubleBufferImage;
        private PlanetAgent _ownerAgent;

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
            Utils.XPointSize = pictureBox.Width / Utils.XPoints;
            Utils.YPointSize = pictureBox.Height / Utils.YPoints;
            DrawPlanet();
        }

        private void DrawPlanet()
        {

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
            foreach (var edge in _ownerAgent.Edges)
                DrawEdge(g, edge);

            // Draw all nodes in the graph
            foreach (var node in _ownerAgent.Nodes.Values)
                DrawNode(g, node, node.Resource);

            // draw the base node
            DrawBase(g, _ownerAgent.BaseNode);

            // draw the ants
            foreach (var antPair in _ownerAgent.AntsPositions)
                DrawAnt(g, antPair.Key, antPair.Value);

            // display the buffer image 
            var pbg = pictureBox.CreateGraphics();
            pbg.DrawImage(_doubleBufferImage, 0, 0);
        }

        private static void DrawBase(Graphics g, Node nodeBase)
        {
            g.FillEllipse(Brushes.Red, nodeBase.Pos.X, nodeBase.Pos.Y, 18, 18);
        }

        private static void DrawNode(Graphics g, Node node, int resource)
        {
            if (resource > 20) resource = 20;
            var p = resource / 20.0;
            var brush = new SolidBrush(Color.FromArgb(0, (int)(255 * p), 0));

            // Draw the node
            g.FillEllipse(brush, node.Pos.X, node.Pos.Y, 18, 18);
            // draw name above node
            g.DrawString(node.Name, new Font("Arial", 8), Brushes.Black, node.Pos.X, node.Pos.Y - 10);
            // draw resource below node
            g.DrawString(node.Resource.ToString(), new Font("Arial", 8), Brushes.Black, node.Pos.X, node.Pos.Y + 20);
        }

        private static void DrawEdge(Graphics g, Edge edge)
        {
            // decide on color based on weight
            Color color;
            if (edge.Weight == 0)
                color = Color.LightGray;
            else if (edge.Weight < 5)
                color = Color.Green;
            else
                color = Color.Red;

            Pen pen = new Pen(color, 2);
            // Draw the edge
            g.DrawLine(pen, edge.A.Pos.X + 9, edge.A.Pos.Y + 9, edge.B.Pos.X + 9, edge.B.Pos.Y + 9);
        }

        private static void DrawAnt(Graphics g, string antName, Position pos)
        {
            // Draw the ant
            g.FillEllipse(Brushes.Blue, pos.X + 4, pos.Y + 4, 10, 10);
            // draw name above ant
            //g.DrawString(antName, new Font("Arial", 8), Brushes.Black, pos.X, pos.Y - 10);
        }
    }
}