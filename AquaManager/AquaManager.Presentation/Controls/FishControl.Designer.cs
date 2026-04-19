namespace AquaManager.Presentation.Controls
{
    partial class FishControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            pbIcon = new PictureBox();
            lblName = new Label();
            pbHunger = new ProgressBar();
            lblHungerPercent = new Label();
            ((System.ComponentModel.ISupportInitialize)pbIcon).BeginInit();
            SuspendLayout();
            // 
            // pbIcon
            // 
            pbIcon.Location = new Point(5, 10);
            pbIcon.Name = "pbIcon";
            pbIcon.Size = new Size(48, 48);
            pbIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pbIcon.TabIndex = 0;
            pbIcon.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.Location = new Point(60, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(40, 15);
            lblName.TabIndex = 1;
            lblName.Text = "label1";
            // 
            // pbHunger
            // 
            pbHunger.Location = new Point(60, 35);
            pbHunger.Name = "pbHunger";
            pbHunger.Size = new Size(120, 15);
            pbHunger.Style = ProgressBarStyle.Continuous;
            pbHunger.TabIndex = 2;
            pbHunger.Value = 100;
            // 
            // lblHungerPercent
            // 
            lblHungerPercent.AutoSize = true;
            lblHungerPercent.Location = new Point(185, 33);
            lblHungerPercent.Name = "lblHungerPercent";
            lblHungerPercent.Size = new Size(35, 15);
            lblHungerPercent.TabIndex = 3;
            lblHungerPercent.Text = "100%";
            // 
            // FishControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblHungerPercent);
            Controls.Add(pbHunger);
            Controls.Add(lblName);
            Controls.Add(pbIcon);
            Name = "FishControl";
            Size = new Size(228, 66);
            ((System.ComponentModel.ISupportInitialize)pbIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbIcon;
        private Label lblName;
        private ProgressBar pbHunger;
        private Label lblHungerPercent;
    }
}
