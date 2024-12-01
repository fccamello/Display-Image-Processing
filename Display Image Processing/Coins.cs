using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcess2;
using static System.Net.Mime.MediaTypeNames;

namespace Display_Image_Processing
{
    public partial class Coins : Form
    {
        Bitmap loaded, processed;
        List<int> coinsArea = new List<int>();
        bool[,] visited_pixels;
        double TotalValue = 0;
        int peso5 = 0, peso1 = 0, cent25 = 0, cent10 = 0, cent5 = 0;

        public Coins()
        {
            InitializeComponent();
        }

        private int getCoinArea(Point start_point)
        {
            int area = 1;

            Queue<Point> myQueue = new Queue<Point>();
            myQueue.Enqueue(start_point);
            visited_pixels[start_point.X, start_point.Y] = true;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (myQueue.Count > 0)
            {
                Point currentPoint = myQueue.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    int newX = currentPoint.X + dx[i];
                    int newY = currentPoint.Y + dy[i];

                    if (newX >= 0 && newX < loaded.Width && newY >= 0 && newY < loaded.Height && !visited_pixels[newX, newY])
                    {

                        Color pixelColor = loaded.GetPixel(newX, newY);
                        if (isBlack(pixelColor))
                        {
                            visited_pixels[newX, newY] = true;
                            myQueue.Enqueue(new Point(newX, newY));

                            area++;
                        }
                    }
                }

            }

            return area;
        }


        private bool isBlack(Color p)
        {
            return p.R == 0 && p.G == 0 && p.B == 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void getCoinAreaValue(int coinSizeThreshold)
        {
            coinsArea.Sort();

            List<List<int>> groupedCoins = new List<List<int>>();
            List<int> currentGroup = new List<int> { coinsArea[0] };


            for (int i = 1; i < coinsArea.Count; i++)
            {
                if (coinsArea[i] - currentGroup[0] <= coinSizeThreshold)
                {
                    currentGroup.Add(coinsArea[i]);
                }
                else
                {

                    groupedCoins.Add(new List<int>(currentGroup));
                    currentGroup.Clear();
                    currentGroup.Add(coinsArea[i]);
                }
            }


            groupedCoins.Add(new List<int>(currentGroup));



            peso5 = groupedCoins[4].Count;
            peso1 = groupedCoins[3].Count;
            cent25 = groupedCoins[2].Count;
            cent10 = groupedCoins[1].Count;
            cent5 = groupedCoins[0].Count;

            label1.Text = "5 Peso: " + peso5.ToString() + " pcs = ₱" + (5 * peso5).ToString();
            label2.Text = "1 Peso: " + peso1.ToString() + " pcs = ₱" + peso1.ToString();
            label3.Text = "25 Cents: " + cent25.ToString() + " pcs = ₱" + (0.25 * cent25).ToString();
            label4.Text = "10 Cents: " + cent10.ToString() + " pcs = ₱" + (0.10 * cent10).ToString();
            label5.Text = "5 Cents: " + cent5.ToString() + " pcs = ₱" + (0.05 * cent5).ToString();

            TotalValue = 5 * peso5 + peso1 + 0.25 * cent25 + 0.10 * cent10 + 0.05 * cent5;
            label6.Text = "Total: ₱" + TotalValue.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BitmapFilter.GaussianBlur(loaded, 10);

            BitmapFilter.GrayScale(loaded);

            BitmapFilter.Binary(loaded, 200);

            visited_pixels = new bool[loaded.Width, loaded.Height];

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {

                    Color current_pixel = loaded.GetPixel(x, y);
                    Point current_point = new Point(x, y);

                    if (isBlack(current_pixel) && !visited_pixels[current_point.X, current_point.Y])
                    {
                        int new_coin_area = getCoinArea(current_point);

                        if (new_coin_area > 50)
                        {
                            coinsArea.Add(new_coin_area);
                        }


                    }
                }
            }

            int coinSizeThreshold = 300;
            getCoinAreaValue(coinSizeThreshold);
        }
    }
}







