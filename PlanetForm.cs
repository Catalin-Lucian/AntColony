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
            foreach (var edge in _ownerAgent.edges)
                DrawEdge(g, edge);

            // Draw all nodes in the graph
            foreach (var node in _ownerAgent.nodes.Values)
                DrawNode(g, node, node.resource);

            // draw the base node
            DrawBase(g, _ownerAgent.baseNode);

            // draw the ants
            foreach (var antPair in _ownerAgent.antsPositions)
                DrawAnt(g, antPair.Key, antPair.Value);

            // display the buffer image 
            var pbg = pictureBox.CreateGraphics();
            pbg.DrawImage(_doubleBufferImage, 0, 0);
        }

        private static void DrawBase(Graphics g, Node nodeBase)
        {
            g.FillEllipse(Brushes.Red, nodeBase.position.x, nodeBase.position.y, 18, 18);
        }

        private static void DrawNode(Graphics g, Node node, int resource)
        {
            // calculate a shade of green based on the resource value
            if (resource > 20) resource = 20;
            var p = resource / 20.0;
            var brush = new SolidBrush(Color.FromArgb(0, (int)(255 * p), 0));

            // Draw the node
            g.FillEllipse(brush, node.position.x, node.position.y, 18, 18);
            // draw name above node
            g.DrawString(node.name, new Font("Arial", 8), Brushes.Black, node.position.x, node.position.y - 10);
            // draw resource below node
            g.DrawString(node.resource.ToString(), new Font("Arial", 8), Brushes.Black, node.position.x, node.position.y + 20);
        }

        private static void DrawEdge(Graphics g, Edge edge)
        {
            // decide on color based on weight
            Pen pen;
            if (edge.Weight == 0) pen = new Pen(Color.LightGray, 1);
            else if (edge.Weight < Utils.MaxWeight / 2) pen = new Pen(Color.Green, 2);
            else pen = new Pen(Color.Red, 2);

            // Draw the edge
            g.DrawLine(pen, edge.A.position.x + 9, edge.A.position.y + 9, edge.B.position.x + 9, edge.B.position.y + 9);
        }

        private static void DrawAnt(Graphics g, string antName, Position pos)
        {
            // Draw the ant
            g.FillEllipse(Brushes.HotPink, pos.x + 4, pos.y + 4, 10, 10);
        }
    }
}