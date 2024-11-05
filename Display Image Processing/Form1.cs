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
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pixelCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y<loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
                pictureBox2.Image = processed;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
            saveFileDialog1.ShowDialog();
        }

  
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

                string filePath = saveFileDialog1.FileName;

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        processed.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 2:
                        processed.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        processed.Save(filePath);
                        break;
                }
            
        }

        private void greyscalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int grey;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    grey = (int) (pixel.R + pixel.G + pixel.B)/3;
                    Color average = Color.FromArgb(grey,grey,grey);
                    processed.SetPixel(x, y, average);
                }
                pictureBox2.Image = processed;
            }
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color invert = Color.FromArgb(255-pixel.R, 255-pixel.G, 255-pixel.B);
                    processed.SetPixel(x, y, invert);
                }
                pictureBox2.Image = processed;
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Hist(ref loaded, ref processed);
            pictureBox2.Image = processed;

        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Sepia(ref loaded, ref processed);
            pictureBox2.Image = processed;

        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
