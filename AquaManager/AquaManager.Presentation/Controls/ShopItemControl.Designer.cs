namespace AquaManager.Presentation.Controls
{
    partial class ShopItemControl
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
            lblPrice = new Label();
            lblDesc = new Label();
            btnBuy = new Button();
            ((System.ComponentModel.ISupportInitialize)pbIcon).BeginInit();
            SuspendLayout();
            // 
            // pbIcon
            // 
            pbIcon.Location = new Point(5, 5);
            pbIcon.Name = "pbIcon";
            pbIcon.Size = new Size(50, 50);
            pbIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pbIcon.TabIndex = 0;
            pbIcon.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblName.Location = new Point(65, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(53, 17);
            lblName.TabIndex = 1;
            lblName.Text = "Рыба 1";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.ForeColor = Color.DarkBlue;
            lblPrice.Location = new Point(65, 35);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(56, 15);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "40 монет";
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Font = new Font("Segoe UI", 7F);
            lblDesc.ForeColor = Color.Gray;
            lblDesc.Location = new Point(65, 52);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(93, 12);
            lblDesc.TabIndex = 3;
            lblDesc.Text = "Описание рыбки 1";
            // 
            // btnBuy
            // 
            btnBuy.BackColor = Color.LightGreen;
            btnBuy.FlatStyle = FlatStyle.Flat;
            btnBuy.Location = new Point(290, 20);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(80, 30);
            btnBuy.TabIndex = 4;
            btnBuy.Text = "Купить";
            btnBuy.UseVisualStyleBackColor = false;
            btnBuy.Click += btnBuy_Click;
            // 
            // ShopItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnBuy);
            Controls.Add(lblDesc);
            Controls.Add(lblPrice);
            Controls.Add(lblName);
            Controls.Add(pbIcon);
            Name = "ShopItemControl";
            Size = new Size(380, 80);
            ((System.ComponentModel.ISupportInitialize)pbIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbIcon;
        private Label lblName;
        private Label lblPrice;
        private Label lblDesc;
        private Button btnBuy;
    }
}
