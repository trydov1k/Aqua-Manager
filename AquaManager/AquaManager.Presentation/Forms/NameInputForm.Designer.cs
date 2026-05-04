namespace AquaManager.Presentation.Forms
{
    partial class NameInputForm
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
            lblIcon = new Label();
            lblPrompt = new Label();
            txtName = new TextBox();
            lblHint = new Label();
            btnSkip = new Button();
            btnOk = new Button();
            SuspendLayout();
            // 
            // lblIcon
            // 
            lblIcon.Font = new Font("Segoe UI Emoji", 32F);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(15, 15);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(80, 60);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "🐠";
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPrompt
            // 
            lblPrompt.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPrompt.ForeColor = Color.LightCyan;
            lblPrompt.Location = new Point(10, 30);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(400, 30);
            lblPrompt.TabIndex = 1;
            lblPrompt.Text = "Дайте имя рыбке:";
            lblPrompt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(40, 70, 130);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 13F);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(40, 100);
            txtName.MaxLength = 15;
            txtName.Name = "txtName";
            txtName.Size = new Size(350, 31);
            txtName.TabIndex = 2;
            txtName.Text = "Гуппи";
            txtName.Enter += txtName_Enter;
            txtName.Leave += txtName_Leave;
            // 
            // lblHint
            // 
            lblHint.Font = new Font("Segoe UI", 8F);
            lblHint.ForeColor = Color.LightGray;
            lblHint.Location = new Point(10, 130);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(400, 20);
            lblHint.TabIndex = 3;
            lblHint.Text = "Макс. 15 символов. Оставьте пустым для имени по умолчанию.";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSkip
            // 
            btnSkip.BackColor = Color.FromArgb(60, 60, 100);
            btnSkip.DialogResult = DialogResult.Cancel;
            btnSkip.FlatAppearance.BorderColor = Color.Gray;
            btnSkip.FlatStyle = FlatStyle.Flat;
            btnSkip.ForeColor = Color.LightGray;
            btnSkip.Location = new Point(40, 190);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(120, 34);
            btnSkip.TabIndex = 4;
            btnSkip.Text = "Пропустить";
            btnSkip.UseVisualStyleBackColor = false;
            btnSkip.Click += btnSkip_Click;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(0, 140, 200);
            btnOk.DialogResult = DialogResult.OK;
            btnOk.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.ForeColor = Color.White;
            btnOk.Location = new Point(255, 190);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(140, 34);
            btnOk.TabIndex = 5;
            btnOk.Text = "✔ Подтвердить";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 40, 80);
            ClientSize = new Size(424, 241);
            Controls.Add(btnOk);
            Controls.Add(btnSkip);
            Controls.Add(lblHint);
            Controls.Add(txtName);
            Controls.Add(lblIcon);
            Controls.Add(lblPrompt);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Назовите рыбку";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIcon;
        private Label lblPrompt;
        private TextBox txtName;
        private Label lblHint;
        private Button btnSkip;
        private Button btnOk;
    }
}