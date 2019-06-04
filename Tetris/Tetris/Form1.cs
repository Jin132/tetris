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
            gr = Graphics.FromImage(bitfield);
            InitializeComponent();
            
            for (int i = 0; i < width; i++)
                field[i, height - 1] = 1;
            for (int i = 0; i < height; i++)
            {
                field[0, i] = 1;
                field[width - 1, i] = 1;
            }
            SetShape();
            FillField();

        }
        public bool FindMistake()
        {
            for (int i = 0; i < 4; i++)
                if (shape[0, i] >= width || shape[1, i] >= height ||
                    shape[1, i] <= 0 || shape[0, i] <= 0 ||
                    field[shape[0, i], shape[1, i]] == 1)
                    return true;
            return false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    for (int i = 0; i < 4; i++)
                        shape[0, i]--;
                    if (FindMistake())
                        for (int i = 0; i < 4; i++)
                            shape[0, i]++;
                    break;
                case Keys.D:
                    for (int i = 0; i < 4; i++)
                        shape[0, i]++;
                    if (FindMistake())
                        for (int i = 0; i < 4; i++)
                            shape[0, i]--;
                    break;
                case Keys.S:
                    for (int i = 0; i < 4; i++)
                        shape[1, i]++;
                    if (FindMistake())
                        for (int i = 0; i < 4; i++)
                            shape[1, i]--;
                    FillField();
                    break;
                case Keys.Space:
                    var shapeT = new int[2, 4];
                    Array.Copy(shape, shapeT, shape.Length);
                    int maxx = 0, maxy = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (shape[0, i] > maxx)
                            maxx = shape[0, i];
                        if (shape[1, i] > maxy)
                            maxy = shape[1, i];
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        int temp = shape[1, i];
                        shape[1, i] = maxy - (maxx - shape[0, i]) - 1;
                        shape[0, i] = maxx - (3 - (maxy - temp)) + 1;
                    }
                    if (FindMistake())
                        Array.Copy(shapeT, shape, shape.Length);
                    break;
            }
        }

        private void TickTimer_Tick(object sender, System.EventArgs e)
        {
            int cross = 0;
            if (field[8, 3] == 1)
                Environment.Exit(0);
            for (int i = 0; i < 4; i++)
                shape[1, i]++;

            for (int i = height - 2; i > 2; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[j, i] == 1)
                        cross++;
                }
                if (cross == width)
                    for (int k = i; k > 1; k--)
                        for (int l = 1; l < width - 1; l++)
                            field[l, k] = field[l, k - 1];
            }

            if (FindMistake())
            {
                for (int i = 0; i < 4; i++)
                    field[shape[0, i],--shape[1, i]]++;
                SetShape();
            }
            FillField();
        }

        public void SetShape()
        {
            Random x = new Random(DateTime.Now.Millisecond);
            switch (x.Next(7))
            {
                case 0: shape = new int[,] { { 8, 8, 8, 8 }, { 2, 3, 4, 5 } }; break;
                case 1: shape = new int[,] { { 8, 8, 9, 9 }, { 2, 3, 2, 3 } }; break;
                case 2: shape = new int[,] { { 8, 8, 8, 9 }, { 2, 3, 4, 4 } }; break;
                case 3: shape = new int[,] { { 8, 8, 8, 7 }, { 2, 3, 4, 4 } }; break;
                case 4: shape = new int[,] { { 7, 8, 8, 9 }, { 3, 3, 4, 4 } }; break;
                case 5: shape = new int[,] { { 9, 8, 8, 7 }, { 3, 3, 4, 4 } }; break;
                case 6: shape = new int[,] { { 8, 7, 8, 9 }, { 3, 4, 4, 4 } }; break;


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
            for (int i = 0; i < 4; i++)
            {
                gr.FillRectangle(Brushes.Red, shape[0, i] * k, shape[1, i] * k, k, k);
                gr.DrawRectangle(Pens.Black, shape[0, i] * k, shape[1, i] * k, k, k);
            }
            pictureBox1.Image = bitfield;
        }
    }
}
