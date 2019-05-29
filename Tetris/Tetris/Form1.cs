using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{

    public partial class Form1 : Form
    {
        public const int width = 15, height = 25, k = 15;
        public int[,] shape = new int[2, 4];
        public int[,] field = new int[width, height];
        public Bitmap bitfield = new Bitmap(k * (width + 1) + 1, k * (height + 3) + 1);
        public Graphics gr;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }




        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < width; i++)
                field[i, height - 1] = 1;
            for (int i = 0; i < height; i++)
            {
                field[0, i] = 1;
                field[width - 1, i] = 1;
            }


        }
        public void SetShape()
        {
            Random x = new Random(DateTime.Now.Millisecond);
            switch (x.Next(7))
            {
                case 0: shape = new int[,] { { 2, 3, 4, 5 }, { 8, 8, 8, 8 } }; break;
                case 1: shape = new int[,] { { 2, 3, 2, 3 }, { 8, 8, 9, 9 } }; break;
                case 2: shape = new int[,] { { 2, 3, 4, 4 }, { 8, 8, 8, 9 } }; break;
                case 3: shape = new int[,] { { 2, 3, 4, 4 }, { 8, 8, 8, 7 } }; break;
                case 4: shape = new int[,] { { 3, 3, 4, 4 }, { 7, 8, 8, 9 } }; break;
                case 5: shape = new int[,] { { 3, 3, 4, 4 }, { 9, 8, 8, 7 } }; break;
                case 6: shape = new int[,] { { 3, 4, 4, 4 }, { 8, 7, 8, 9 } }; break;


            }
        }
        public void FillField()
        {
            gr.Clear(Color.Black); 
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (field[i, j] == 1)
                    { 
                        gr.FillRectangle(Brushes.Green, i * k, j * k, k, k);
                        gr.DrawRectangle(Pens.Black, i * k, j * k, k, k);
                    }
            
        }
    }
}
