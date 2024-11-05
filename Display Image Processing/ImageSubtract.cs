using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display_Image_Processing
{
    public partial class ImageSubtract : Form

    {

        Bitmap imageB, imageA, resultImage;

        public ImageSubtract()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = imageB;

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox4.Image = imageA;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            resultImage = new Bitmap(imageB.Width, imageB.Height);
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;

            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue < threshold)
                    {
                        resultImage.SetPixel(x, y, backpixel);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
                }
            }

            pictureBox5.Image = resultImage;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                
        }
    }
}
