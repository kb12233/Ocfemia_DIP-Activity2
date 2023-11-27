using System.Windows.Forms;

namespace Ocfemia_ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loadedImage;
        Bitmap processedImage;
        Bitmap imageB, imageA, colorgreen, resultImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loadedImage = new Bitmap(openFileDialog1.FileName);
            imageB = new Bitmap(openFileDialog1.FileName);
            loadedPictureBox.Image = loadedImage;
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    pixel = loadedImage.GetPixel(i, j);
                    processedImage.SetPixel(i, j, pixel);
                }
            }
            processedPictureBox.Image = processedImage;
            resultPictureBox.Image = null;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            int gray;
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    pixel = loadedImage.GetPixel(i, j);
                    gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    processedImage.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            processedPictureBox.Image = processedImage;
            resultPictureBox.Image = null;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    pixel = loadedImage.GetPixel(i, j);
                    processedImage.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            processedPictureBox.Image = processedImage;
            resultPictureBox.Image = null;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            int gray;
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    pixel = loadedImage.GetPixel(i, j);
                    gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    processedImage.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            Color sample;
            int[] hisdata = new int[256];
            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    sample = processedImage.GetPixel(i, j);
                    hisdata[sample.R]++;
                }
            }
            Bitmap mydata = new Bitmap(265, 800);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    mydata.SetPixel(i, j, Color.White);
                }
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(hisdata[i] / 5, 800); j++)
                {
                    mydata.SetPixel(i, j, Color.Black);
                }
            }

            processedPictureBox.Image = mydata;
            resultPictureBox.Image = null;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processedImage = new Bitmap(loadedImage.Width, loadedImage.Height);

            for (int i = 0; i < loadedImage.Width; i++)
            {
                for (int j = 0; j < loadedImage.Height; j++)
                {
                    Color pixel = loadedImage.GetPixel(i, j);
                    int sepiaR = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaG = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaB = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    sepiaR = Math.Max(0, Math.Min(sepiaR, 255));
                    sepiaG = Math.Max(0, Math.Min(sepiaG, 255));
                    sepiaB = Math.Max(0, Math.Min(sepiaB, 255));

                    processedImage.SetPixel(i, j, Color.FromArgb(sepiaR, sepiaG, sepiaB));
                }
            }

            processedPictureBox.Image = processedImage;
            resultPictureBox.Image = null;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG Image|.jpg|PNG Image|.png|All Files|.";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            processedPictureBox.Image.Save(saveFileDialog1.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            loadedImage = new Bitmap(openFileDialog2.FileName);
            loadedPictureBox.Image = imageB;
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            processedImage = new Bitmap(openFileDialog3.FileName);
            processedPictureBox.Image = imageA;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color mygreen = imageB.GetPixel(0, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            resultImage = new Bitmap(imageB.Width, imageB.Height);

            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractValue = Math.Abs(grey - greygreen);

                    if (subtractValue > threshold)
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }

                    else
                    {
                        resultImage.SetPixel(x, y, backpixel);
                    }
                }
            }

            resultPictureBox.Image = resultImage;
        }
    }
}