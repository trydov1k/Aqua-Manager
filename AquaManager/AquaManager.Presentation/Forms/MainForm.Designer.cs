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
            lblMoney = new Label();
            picAquarium = new PictureBox();
            lblWaterPercent = new Label();
            flpFishList = new FlowLayoutPanel();
            btnFeedAll = new Button();
            btnFeedingMode = new Button();
            btnChangeWater = new Button();
            btnRemoveDead = new Button();
            btnShop = new Button();
            btnSave = new Button();
            btnLoad = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pbWaterCleanliness = new ProgressBar();
            cmbAquariums = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picAquarium).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblMoney
            // 
            lblMoney.Location = new Point(601, 0);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(296, 30);
            lblMoney.TabIndex = 1;
            lblMoney.Text = "250 монет";
            lblMoney.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picAquarium
            // 
            picAquarium.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picAquarium.Image = Properties.Resources.Аквариум_1;
            picAquarium.Location = new Point(54, 70);
            picAquarium.Name = "picAquarium";
            picAquarium.Size = new Size(900, 400);
            picAquarium.SizeMode = PictureBoxSizeMode.StretchImage;
            picAquarium.TabIndex = 4;
            picAquarium.TabStop = false;
            // 
            // lblWaterPercent
            // 
            lblWaterPercent.Location = new Point(529, 0);
            lblWaterPercent.Name = "lblWaterPercent";
            lblWaterPercent.Padding = new Padding(5, 0, 0, 0);
            lblWaterPercent.Size = new Size(66, 30);
            lblWaterPercent.TabIndex = 5;
            lblWaterPercent.Text = "78%";
            lblWaterPercent.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpFishList
            // 
            flpFishList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flpFishList.AutoScroll = true;
            flpFishList.Location = new Point(54, 481);
            flpFishList.Name = "flpFishList";
            flpFishList.Size = new Size(900, 110);
            flpFishList.TabIndex = 6;
            // 
            // btnFeedAll
            // 
            btnFeedAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnFeedAll.Location = new Point(3, 3);
            btnFeedAll.Name = "btnFeedAll";
            btnFeedAll.Size = new Size(174, 63);
            btnFeedAll.TabIndex = 7;
            btnFeedAll.Text = "Покормить всех";
            btnFeedAll.UseVisualStyleBackColor = true;
            btnFeedAll.Click += btnFeedAll_Click;
            // 
            // btnFeedingMode
            // 
            btnFeedingMode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnFeedingMode.Location = new Point(183, 3);
            btnFeedingMode.Name = "btnFeedingMode";
            btnFeedingMode.Size = new Size(174, 63);
            btnFeedingMode.TabIndex = 8;
            btnFeedingMode.Text = "Режим кормления";
            btnFeedingMode.UseVisualStyleBackColor = true;
            btnFeedingMode.Click += btnFeedingMode_Click;
            // 
            // btnChangeWater
            // 
            btnChangeWater.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnChangeWater.Location = new Point(363, 3);
            btnChangeWater.Name = "btnChangeWater";
            btnChangeWater.Size = new Size(174, 63);
            btnChangeWater.TabIndex = 9;
            btnChangeWater.Text = "Сменить воду";
            btnChangeWater.UseVisualStyleBackColor = true;
            btnChangeWater.Click += btnChangeWater_Click;
            // 
            // btnRemoveDead
            // 
            btnRemoveDead.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveDead.Location = new Point(543, 3);
            btnRemoveDead.Name = "btnRemoveDead";
            btnRemoveDead.Size = new Size(174, 63);
            btnRemoveDead.TabIndex = 10;
            btnRemoveDead.Text = "Убрать мертвых";
            btnRemoveDead.UseVisualStyleBackColor = true;
            btnRemoveDead.Click += btnRemoveDead_Click;
            // 
            // btnShop
            // 
            btnShop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnShop.Location = new Point(723, 3);
            btnShop.Name = "btnShop";
            btnShop.Size = new Size(174, 63);
            btnShop.TabIndex = 11;
            btnShop.Text = "Магазин";
            btnShop.UseVisualStyleBackColor = true;
            btnShop.Click += btnShop_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(134, 683);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(200, 35);
            btnSave.TabIndex = 12;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(674, 683);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(200, 35);
            btnLoad.TabIndex = 13;
            btnLoad.Text = "Загрузить";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel1.Controls.Add(lblMoney, 3, 0);
            tableLayoutPanel1.Controls.Add(pbWaterCleanliness, 1, 0);
            tableLayoutPanel1.Controls.Add(lblWaterPercent, 2, 0);
            tableLayoutPanel1.Controls.Add(cmbAquariums, 0, 0);
            tableLayoutPanel1.Location = new Point(54, 15);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(900, 30);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // pbWaterCleanliness
            // 
            pbWaterCleanliness.Location = new Point(302, 3);
            pbWaterCleanliness.Name = "pbWaterCleanliness";
            pbWaterCleanliness.Size = new Size(221, 24);
            pbWaterCleanliness.Style = ProgressBarStyle.Continuous;
            pbWaterCleanliness.TabIndex = 0;
            // 
            // cmbAquariums
            // 
            cmbAquariums.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbAquariums.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAquariums.FormattingEnabled = true;
            cmbAquariums.Location = new Point(3, 3);
            cmbAquariums.Name = "cmbAquariums";
            cmbAquariums.Size = new Size(293, 23);
            cmbAquariums.TabIndex = 6;
            cmbAquariums.SelectedIndexChanged += cmbAquariums_SelectedIndexChanged;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Controls.Add(btnFeedAll, 0, 0);
            tableLayoutPanel2.Controls.Add(btnFeedingMode, 1, 0);
            tableLayoutPanel2.Controls.Add(btnChangeWater, 2, 0);
            tableLayoutPanel2.Controls.Add(btnRemoveDead, 3, 0);
            tableLayoutPanel2.Controls.Add(btnShop, 4, 0);
            tableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel2.Location = new Point(54, 608);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(900, 69);
            tableLayoutPanel2.TabIndex = 15;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(1008, 729);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(flpFishList);
            Controls.Add(picAquarium);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AquaManager";
            ((System.ComponentModel.ISupportInitialize)picAquarium).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblMoney;
        private ProgressBar pbWaterCleanliness;
        private PictureBox picAquarium;
        private Label lblWaterPercent;
        private FlowLayoutPanel flpFishList;
        private Button btnFeedAll;
        private Button btnFeedingMode;
        private Button btnChangeWater;
        private Button btnRemoveDead;
        private Button btnShop;
        private Button btnSave;
        private Button btnLoad;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cmbAquariums;
        private TableLayoutPanel tableLayoutPanel2;
    }
}