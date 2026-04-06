namespace AquaManager.Presentation.Forms
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
            aquarium = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)aquarium).BeginInit();
            SuspendLayout();
            // 
            // aquarium
            // 
            aquarium.Image = Properties.Resources.Аквариум_1;
            aquarium.ImageLocation = "";
            aquarium.InitialImage = Properties.Resources.Аквариум_2;
            aquarium.Location = new Point(100, 80);
            aquarium.Name = "aquarium";
            aquarium.Size = new Size(820, 590);
            aquarium.SizeMode = PictureBoxSizeMode.StretchImage;
            aquarium.TabIndex = 0;
            aquarium.TabStop = false;
            aquarium.Click += pictureBox1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(1008, 729);
            Controls.Add(aquarium);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AquaManager";
            ((System.ComponentModel.ISupportInitialize)aquarium).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox aquarium;
    }
}