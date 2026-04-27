namespace AquaManager.Presentation.Forms
{
    partial class ShopForm
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
            flpItems = new FlowLayoutPanel();
            lblMoney = new Label();
            SuspendLayout();
            // 
            // flpItems
            // 
            flpItems.AutoScroll = true;
            flpItems.FlowDirection = FlowDirection.TopDown;
            flpItems.Location = new Point(15, 20);
            flpItems.Name = "flpItems";
            flpItems.Size = new Size(400, 380);
            flpItems.TabIndex = 0;
            flpItems.WrapContents = false;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMoney.ForeColor = Color.DarkGreen;
            lblMoney.Location = new Point(15, 420);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(120, 19);
            lblMoney.TabIndex = 1;
            lblMoney.Text = "У вас 250 монет";
            // 
            // ShopForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 461);
            Controls.Add(lblMoney);
            Controls.Add(flpItems);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShopForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Магазин";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpItems;
        private Label lblMoney;
    }
}