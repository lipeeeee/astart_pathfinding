using System.Runtime.CompilerServices;

namespace astart_pathfinding
{
    /*
        1. Draw matrix
        
    */

    /*
        - Start maximized
    */

    public partial class Form1 : Form
    {
        public int[,] matrix;
        
        public Form1()
        {
            InitializeComponent();

            // Non-Nullable warning
            matrix = new int[0,0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Coordinates
            this.MaximumSize = new Size(this.Width, this.Height);
            matrix = new int[this.Width, this.Height];

            utils.fillBidemensionalMatrix(matrix, globals.matrixValues["empty"]);
            // utils.debugMatrixValues(matrix);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            updateDimensions();
            renderMatrix();
        }

        private void drawGrid()
        {
            SolidBrush myBrush = new(Color.Black);
            Graphics formGraphics = this.CreateGraphics();
            Pen p = new(myBrush);

            // Draw matrix grid 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Vertical
                formGraphics.DrawLine(p, i * globals.cellSize, 0, i * globals.cellSize, matrix.GetLength(0) * globals.cellSize);

                // Horizontal
                formGraphics.DrawLine(p, 0, i * globals.cellSize, matrix.GetLength(1) * globals.cellSize, i * globals.cellSize);
            }

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        // Color mapping
        private void drawMatrixValues()
        {
            SolidBrush blackBrush = new(Color.Black);
            SolidBrush redBrush = new(Color.Red);
            SolidBrush greenBrush = new(Color.Green);
            SolidBrush emptyBrush = new(Color.WhiteSmoke);
            Graphics formGraphics = this.CreateGraphics();

            int cur_x = 0, cur_y = 0;

            // Color draw matrix values
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == globals.matrixValues["empty"]) 
                    {
                        formGraphics.FillRectangle(emptyBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["wall"])
                    {
                        formGraphics.FillRectangle(blackBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["start"])
                    {
                        formGraphics.FillRectangle(greenBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["end"])
                    {
                        formGraphics.FillRectangle(redBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else
                        throw new Exception("matrix unbound value");

                    cur_x += globals.cellSize;
                }
                cur_x = 0;
                cur_y += globals.cellSize;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Brute force to know which cell clicked on
            int[] ij = utils.getCell(matrix, e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                matrix[ij[0], ij[1]] = globals.matrixValues["wall"];
            else
                matrix[ij[0], ij[1]] = globals.matrixValues["empty"];

            renderMatrix();
        }

        private void renderMatrix()
        {
            drawMatrixValues();
            drawGrid();
        }

        private void updateDimensions()
        {
            lblMatrixEndpoints.Text = this.Width + "," + this.Height;      
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // ONLY USE THIS TO DEBUG, THIS CAUSES onPaint() FUNCTION TO BE CALLED
            // WHEN X OR Y GOES A DIGIT DOWN (100->99, 1000->999)
            // lblMousePos.Text = e.X + ", " + e.Y;
        }
    }
}