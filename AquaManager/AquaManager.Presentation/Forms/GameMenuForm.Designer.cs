namespace AquaManager.Presentation.Forms
{
    partial class GameMenuForm
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
            sep = new Panel();
            btnContinue = new Button();
            btnSave = new Button();
            btnLoad = new Button();
            btnTutorial = new Button();
            btnNewGame = new Button();
            btnExit = new Button();
            lblVersion = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DeepSkyBlue;
            lblTitle.Location = new Point(0, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🐠  Aqua Manager";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sep
            // 
            sep.BackColor = Color.FromArgb(0, 100, 180);
            sep.Location = new Point(40, 88);
            sep.Name = "sep";
            sep.Size = new Size(280, 2);
            sep.TabIndex = 1;
            // 
            // btnContinue
            // 
            btnContinue.BackColor = Color.FromArgb(0, 130, 200);
            btnContinue.FlatAppearance.BorderColor = Color.FromArgb(40, 170, 240);
            btnContinue.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 160, 230);
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.ForeColor = Color.White;
            btnContinue.Location = new Point(60, 110);
            btnContinue.Name = "btnContinue";
            btnContinue.Padding = new Padding(8, 0, 0, 0);
            btnContinue.Size = new Size(240, 46);
            btnContinue.TabIndex = 2;
            btnContinue.Text = "▶  Продолжить игру";
            btnContinue.TextAlign = ContentAlignment.MiddleLeft;
            btnContinue.UseVisualStyleBackColor = false;
            btnContinue.Click += btnContinue_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 110, 60);
            btnSave.FlatAppearance.BorderColor = Color.FromArgb(40, 150, 100);
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 140, 90);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(60, 170);
            btnSave.Name = "btnSave";
            btnSave.Padding = new Padding(8, 0, 0, 0);
            btnSave.Size = new Size(240, 46);
            btnSave.TabIndex = 3;
            btnSave.Text = "💾  Сохранить игру";
            btnSave.TextAlign = ContentAlignment.MiddleLeft;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.FromArgb(0, 80, 150);
            btnLoad.FlatAppearance.BorderColor = Color.FromArgb(40, 120, 190);
            btnLoad.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 110, 180);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.ForeColor = Color.White;
            btnLoad.Location = new Point(60, 225);
            btnLoad.Name = "btnLoad";
            btnLoad.Padding = new Padding(8, 0, 0, 0);
            btnLoad.Size = new Size(240, 46);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "📂  Загрузить игру";
            btnLoad.TextAlign = ContentAlignment.MiddleLeft;
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnTutorial
            // 
            btnTutorial.BackColor = Color.FromArgb(0, 140, 160);
            btnTutorial.FlatAppearance.BorderColor = Color.FromArgb(40, 180, 200);
            btnTutorial.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 170, 190);
            btnTutorial.FlatStyle = FlatStyle.Flat;
            btnTutorial.ForeColor = Color.White;
            btnTutorial.Location = new Point(60, 280);
            btnTutorial.Name = "btnTutorial";
            btnTutorial.Padding = new Padding(8, 0, 0, 0);
            btnTutorial.Size = new Size(240, 46);
            btnTutorial.TabIndex = 5;
            btnTutorial.Text = "📖  Начать обучение";
            btnTutorial.TextAlign = ContentAlignment.MiddleLeft;
            btnTutorial.UseVisualStyleBackColor = false;
            btnTutorial.Click += btnTutorial_Click;
            // 
            // btnNewGame
            // 
            btnNewGame.BackColor = Color.FromArgb(100, 60, 0);
            btnNewGame.FlatAppearance.BorderColor = Color.FromArgb(140, 100, 40);
            btnNewGame.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 90, 30);
            btnNewGame.FlatStyle = FlatStyle.Flat;
            btnNewGame.ForeColor = Color.White;
            btnNewGame.Location = new Point(60, 335);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Padding = new Padding(8, 0, 0, 0);
            btnNewGame.Size = new Size(240, 46);
            btnNewGame.TabIndex = 6;
            btnNewGame.Text = "🆕  Новая игра";
            btnNewGame.TextAlign = ContentAlignment.MiddleLeft;
            btnNewGame.UseVisualStyleBackColor = false;
            btnNewGame.Click += btnNewGame_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(140, 20, 20);
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(180, 60, 60);
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(130, 50, 50);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(60, 405);
            btnExit.Name = "btnExit";
            btnExit.Padding = new Padding(8, 0, 0, 0);
            btnExit.Size = new Size(240, 46);
            btnExit.TabIndex = 7;
            btnExit.Text = "🚪  Выйти из игры";
            btnExit.TextAlign = ContentAlignment.MiddleLeft;
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // lblVersion
            // 
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.DimGray;
            lblVersion.Location = new Point(0, 460);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(360, 20);
            lblVersion.TabIndex = 8;
            lblVersion.Text = "Aqua Manager v1.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 30, 60);
            ClientSize = new Size(360, 516);
            Controls.Add(lblVersion);
            Controls.Add(btnExit);
            Controls.Add(btnNewGame);
            Controls.Add(btnTutorial);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(btnContinue);
            Controls.Add(sep);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(0, 7, 0, 7);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Меню";
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Panel sep;
        private Button btnContinue;
        private Button btnSave;
        private Button btnLoad;
        private Button btnTutorial;
        private Button btnNewGame;
        private Button btnExit;
        private Label lblVersion;
    }
}