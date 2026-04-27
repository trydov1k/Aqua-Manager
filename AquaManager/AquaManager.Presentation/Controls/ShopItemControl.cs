
namespace AquaManager.Presentation.Controls
{
    public partial class ShopItemControl : UserControl
    {
        private Action OnBuy;

        public ShopItemControl(Image image, string name, decimal price,
            string description, Color buyButtonColor, Action onBuy)
        {
            InitializeComponent();

            pbIcon.Image = image;
            lblName.Text = name;
            lblPrice.Text = $"{price} монет";
            lblDesc.Text = description;
            btnBuy.BackColor = buyButtonColor;

            OnBuy = onBuy;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            OnBuy();
        }
    }
}
