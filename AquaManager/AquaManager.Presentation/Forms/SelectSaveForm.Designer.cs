namespace AquaManager.Presentation.Forms
{
    partial class SelectSaveForm
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
            lblTitle = new Label();
            listPanel = new Panel();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Выберите сохранение:";
            // 
            // listPanel
            // 
            listPanel.AutoScroll = true;
            listPanel.BackColor = Color.FromArgb(22, 44, 88);
            listPanel.Location = new Point(20, 40);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(410, 300);
            listPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(60, 60, 90);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatAppearance.BorderColor = Color.Gray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(340, 362);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 34);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // SelectSaveFormm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 30, 60);
            ClientSize = new Size(444, 411);
            Controls.Add(btnCancel);
            Controls.Add(listPanel);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.LightCyan;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectSaveFormm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Загрузка сохранения";
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Panel listPanel;
        private Button btnCancel;
    }
}