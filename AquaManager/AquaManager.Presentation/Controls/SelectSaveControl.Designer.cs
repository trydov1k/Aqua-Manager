namespace AquaManager.Presentation.Controls
{
    partial class SelectSaveControl
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
            lblIcon = new Label();
            lblName = new Label();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // lblIcon
            // 
            lblIcon.Font = new Font("Segoe UI Emoji", 14F);
            lblIcon.ForeColor = Color.White;
            lblIcon.Location = new Point(6, 6);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(34, 28);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "💾";
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblIcon.Click += lblIcon_Click;
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 9.5F);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(44, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(270, 40);
            lblName.TabIndex = 1;
            lblName.Text = "gamesave";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            lblName.Click += lblName_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(130, 20, 20);
            btnDelete.FlatAppearance.BorderColor = Color.DarkRed;
            btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 30, 30);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Emoji", 11F);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(347, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(36, 28);
            btnDelete.TabIndex = 2;
            btnDelete.TabStop = false;
            btnDelete.Text = "🗑";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            btnDelete.MouseEnter += btnDelete_MouseEnter;
            // 
            // SelectSaveControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 58, 110);
            Controls.Add(btnDelete);
            Controls.Add(lblName);
            Controls.Add(lblIcon);
            Cursor = Cursors.Hand;
            Name = "SelectSaveControl";
            Size = new Size(393, 40);
            Click += SelectSaveControl_Click;
            MouseEnter += SelectSaveControl_MouseEnter;
            MouseLeave += SelectSaveControl_MouseLeave;
            ResumeLayout(false);
        }

        #endregion

        private Label lblIcon;
        private Label lblName;
        private Button btnDelete;
    }
}
