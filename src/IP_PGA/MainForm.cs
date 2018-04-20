using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IP_PGA
{
    public partial class MainForm : Form
    {
        Bitmap originalImage;
        PGA.ParallelGeneticAlgorithm PGA;
        string defaultPath = @"Data\DefaultPic.img";

        public MainForm()
        {
            InitializeComponent();

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            this.Text = "Image Processing by Genetic Algorithm (Behzad Khosravifar)";

            PGA = new PGA.ParallelGeneticAlgorithm();
            PGA.PicBox = this.pictureBoxDynamic;
            PGA.lblFitness = this.lblFitness;
            PGA.lblGeneration = this.lblGeneration;
            PGA.btnPauseResume = this.btnPauseResume;
            PGA.btnStartStop = this.btnStartStop;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (PGA.State != IP_PGA.PGA.ProcessState.Running && PGA.State != IP_PGA.PGA.ProcessState.Paused) // Start
            {
                if (txtPath.Text != string.Empty)
                {
                    //
                    // Load Picture from Path
                    // 
                    originalImage = new Bitmap(txtPath.Text);
                    //
                    // Convert ARGB to Gray
                    //
                    IP_PGA.PGA.Chromosome.OriginalImageMatrix = ARGB2Gray(ref originalImage);
                    //
                    // Set Image Size for all
                    //
                    IP_PGA.PGA.Chromosome.imageHeight = originalImage.Height;
                    IP_PGA.PGA.Chromosome.imageWidth = originalImage.Width;
                    //
                    // Run Algorithm
                    //
                    PGA.Start();
                    btnStartStop.Text = "&Stop";
                }
                else if (System.IO.File.Exists(defaultPath))
                {
                    txtPath.Text = defaultPath;
                    btnStartStop_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Browse a Picture for Process on it.", "Empty Path Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Stop
            {
                PGA.Stop();
                btnStartStop.Text = "&Start";
            }
        }

        private void btnPauseResume_Click(object sender, EventArgs e)
        {
            try
            {
                if (PGA.State == IP_PGA.PGA.ProcessState.Paused)
                {
                    PGA.Resume();
                    btnPauseResume.Text = "&Pause";
                }
                else if (PGA.State == IP_PGA.PGA.ProcessState.Running)
                {
                    PGA.Pause();
                    btnPauseResume.Text = "&Resume";
                }
            }
            catch { }
        }

        /// <summary>
        /// Convert ARGB Picture to Gray Picture
        /// </summary>
        /// <param name="ARGB">Original Picture</param>
        private byte[,] ARGB2Gray(ref Bitmap ARGB)
        {
            byte[,] Pixels = new byte[ARGB.Width, ARGB.Height];

            for (int x = 0; x < ARGB.Width; x++)
                for (int y = 0; y < ARGB.Height; y++)
                {
                    Color pixel = ARGB.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    ARGB.SetPixel(x, y, Color.FromArgb(0, avg, avg, avg));
                    Pixels[x, y] = (byte)avg;
                }

            return Pixels;
        }

        private void chkParallel_CheckedChanged(object sender, EventArgs e)
        {
            PGA.ParallelProcess = chkParallel.Checked;
        }

        private void chkGraphicalDisplay_CheckedChanged(object sender, EventArgs e)
        {
            PGA.DisplayGraphical = chkGraphicalDisplay.Checked;
        }

    }
}
