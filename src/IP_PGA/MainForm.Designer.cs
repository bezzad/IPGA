namespace IP_PGA
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxDynamic = new System.Windows.Forms.PictureBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnPauseResume = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.lblFitness = new System.Windows.Forms.Label();
            this.chkParallel = new System.Windows.Forms.CheckBox();
            this.chkGraphicalDisplay = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDynamic)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDynamic
            // 
            this.pictureBoxDynamic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDynamic.BackColor = System.Drawing.Color.White;
            this.pictureBoxDynamic.Location = new System.Drawing.Point(0, 38);
            this.pictureBoxDynamic.Name = "pictureBoxDynamic";
            this.pictureBoxDynamic.Size = new System.Drawing.Size(784, 475);
            this.pictureBoxDynamic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDynamic.TabIndex = 0;
            this.pictureBoxDynamic.TabStop = false;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowser.Location = new System.Drawing.Point(12, 527);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(116, 23);
            this.btnBrowser.TabIndex = 1;
            this.btnBrowser.Text = "Original Pic Browser";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartStop.Location = new System.Drawing.Point(616, 527);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 2;
            this.btnStartStop.Text = "&Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnPauseResume
            // 
            this.btnPauseResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPauseResume.Location = new System.Drawing.Point(697, 527);
            this.btnPauseResume.Name = "btnPauseResume";
            this.btnPauseResume.Size = new System.Drawing.Size(75, 23);
            this.btnPauseResume.TabIndex = 3;
            this.btnPauseResume.Text = "Pause";
            this.btnPauseResume.UseVisualStyleBackColor = true;
            this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPath.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPath.Location = new System.Drawing.Point(134, 529);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(326, 20);
            this.txtPath.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "JPEG image files|*.jpg|PNG image files|*.png|Bitmap image files|*.bmp";
            this.openFileDialog1.Title = "Select Image File for Process";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Generation: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(341, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Elite Fitness: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGeneration
            // 
            this.lblGeneration.AutoSize = true;
            this.lblGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblGeneration.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblGeneration.Location = new System.Drawing.Point(122, 11);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(88, 18);
            this.lblGeneration.TabIndex = 7;
            this.lblGeneration.Text = "0000000000";
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFitness
            // 
            this.lblFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFitness.AutoSize = true;
            this.lblFitness.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblFitness.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFitness.Location = new System.Drawing.Point(461, 11);
            this.lblFitness.Name = "lblFitness";
            this.lblFitness.Size = new System.Drawing.Size(88, 18);
            this.lblFitness.TabIndex = 8;
            this.lblFitness.Text = "0000000000";
            this.lblFitness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkParallel
            // 
            this.chkParallel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkParallel.AutoSize = true;
            this.chkParallel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkParallel.Location = new System.Drawing.Point(466, 529);
            this.chkParallel.Name = "chkParallel";
            this.chkParallel.Size = new System.Drawing.Size(132, 19);
            this.chkParallel.TabIndex = 9;
            this.chkParallel.Text = "Parallel Processing";
            this.chkParallel.UseVisualStyleBackColor = true;
            this.chkParallel.CheckedChanged += new System.EventHandler(this.chkParallel_CheckedChanged);
            // 
            // chkGraphicalDisplay
            // 
            this.chkGraphicalDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGraphicalDisplay.AutoSize = true;
            this.chkGraphicalDisplay.Checked = true;
            this.chkGraphicalDisplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGraphicalDisplay.Location = new System.Drawing.Point(651, 13);
            this.chkGraphicalDisplay.Name = "chkGraphicalDisplay";
            this.chkGraphicalDisplay.Size = new System.Drawing.Size(108, 17);
            this.chkGraphicalDisplay.TabIndex = 10;
            this.chkGraphicalDisplay.Text = "Display Graphical";
            this.chkGraphicalDisplay.UseVisualStyleBackColor = true;
            this.chkGraphicalDisplay.CheckedChanged += new System.EventHandler(this.chkGraphicalDisplay_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.chkGraphicalDisplay);
            this.Controls.Add(this.chkParallel);
            this.Controls.Add(this.lblFitness);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnPauseResume);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.pictureBoxDynamic);
            this.MinimumSize = new System.Drawing.Size(780, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Processing by Genetic Algorithm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDynamic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDynamic;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnPauseResume;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.Label lblFitness;
        private System.Windows.Forms.CheckBox chkParallel;
        private System.Windows.Forms.CheckBox chkGraphicalDisplay;
    }
}

