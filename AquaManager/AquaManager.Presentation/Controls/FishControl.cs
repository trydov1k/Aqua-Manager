using AquaManager.Domain.Models;

namespace AquaManager.Presentation.Controls
{
    public partial class FishControl : UserControl
    {
        private Fish _fish;
        public FishControl(Fish fish)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            _fish = fish;
            UpdateDisplay();

            // Подписываем клик по всему контролу (и его дочерним элементам)
            this.Click += OnClick;
            pbIcon.Click += OnClick;
            lblName.Click += OnClick;
            pbHunger.Click += OnClick;
            lblHungerPercent.Click += OnClick;
        }

        public string FishId => _fish.Id;
        public event EventHandler FishClicked;

        public void UpdateDisplay(Fish? fish = null)
        {
            _fish = fish ?? _fish;
            if (_fish == null) return;

            lblName.Text = _fish.Name;

            string imageName = _fish.Type.ToString().ToLower();
            var img = (Image)(Properties.Resources.ResourceManager.GetObject(imageName) ?? Properties.Resources.guppy);
            pbIcon.Image = img;

            if (_fish.IsAlive)
            {
                pbHunger.Value = (int)_fish.Hunger;
                lblHungerPercent.Text = $"{(int)_fish.Hunger}%";

                if (_fish.Hunger < 30)
                    pbHunger.ForeColor = Color.Red;
                else if (_fish.Hunger < 70)
                    pbHunger.ForeColor = Color.Orange;
                else
                    pbHunger.ForeColor = Color.Green;

                pbHunger.Visible = true;
                lblHungerPercent.Visible = true;
                lblName.ForeColor = SystemColors.ControlText;
                pbIcon.BackColor = Color.Transparent;
            }
            else
            {
                // Мёртвая рыбка
                pbHunger.Visible = false;
                lblHungerPercent.Visible = false;
                lblName.Text += " (мёртва)";
                lblName.ForeColor = Color.Gray;
                pbIcon.BackColor = Color.LightGray;
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            FishClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
