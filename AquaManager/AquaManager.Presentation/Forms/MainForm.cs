using AquaManager.Domain.Constants;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using AquaManager.Presentation.Controls;
using AquaManager.Presentation.Extensions;
using AquaManager.Presentation.Models;

namespace AquaManager.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private GameEngine _engine;
        private bool _isFeedingMode;
        private bool _isRemovingMode;

        private List<SwimmingFish> _swimmingFishs = new List<SwimmingFish>();
        private System.Windows.Forms.Timer _animationTimer;
        private FishFactory _fishFactory => _engine._fishFactory;

        public MainForm()
        {
            InitializeComponent();

            _animationTimer = new System.Windows.Forms.Timer();

            _animationTimer.Interval = GameConstants.AnimationTimerIntervalMs;
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();

            picAquarium.Paint += PicAquarium_Paint;
            //picAquarium.MouseClick += PicAquarium_MouseClick;  в будущем сделать, чтобы кормить рыбок можно было по нажатию на рыбку в аквариуме

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            _engine = new GameEngine();
            _engine.StateChanged += OnEngineStateChanged;
            _engine.Start();
        }

        #region Обовление UI
        private void OnEngineStateChanged(object sender, Player player)
        {
            // Обновление UI (с учётом потока)
            if (InvokeRequired)
                Invoke(new Action(() => RefreshUI(player)));
            else
                RefreshUI(player);
        }

        private void RefreshUI(Player player)
        {
            lblMoney.Text = $"{player.Money} монет";


            if (cmbAquariums.Items.Count != player.Aquariums.Count)
            {
                cmbAquariums.Items.Clear();
                foreach (var a in player.Aquariums)
                    cmbAquariums.Items.Add(a.Name);
                cmbAquariums.SelectedIndex = player.CurrentAquariumIndex;
            }
            else if (cmbAquariums.SelectedIndex != player.CurrentAquariumIndex)
            {
                cmbAquariums.SelectedIndex = player.CurrentAquariumIndex;
            }

            var aquarium = player.GetCurrentAquarium();
            if (aquarium == null) return;

            int cleanliness = (int)aquarium.WaterCleanliness;
            pbWaterCleanliness.Value = cleanliness;
            lblWaterPercent.Text = $"{cleanliness}%";

            if (cleanliness < 20)
                pbWaterCleanliness.ForeColor = Color.Red;
            else if (cleanliness < 50)
                pbWaterCleanliness.ForeColor = Color.Orange;
            else
                pbWaterCleanliness.ForeColor = Color.Green;


            SyncSwimmingFishs(aquarium);  // Добавляем плавающих рыбок


            if (flpFishList.Controls.Count != aquarium.FishList.Count)
                RebuildFishList(aquarium);
            else
                UpdateExistingFishControls(aquarium);
        }
        #endregion

        #region Работа со списком рыбок
        private void RebuildFishList(Aquarium aquarium)
        {
            flpFishList.SuspendLayout();
            flpFishList.Controls.Clear();
            foreach (var fish in aquarium.FishList)
            {
                var fc = new FishControl(fish);
                fc.FishClicked += OnFishClicked;
                flpFishList.Controls.Add(fc);
            }
            flpFishList.ResumeLayout();
        }

        private void UpdateExistingFishControls(Aquarium aquarium)
        {
            for (int i = 0; i < aquarium.FishList.Count; i++)
            {
                var fish = aquarium.FishList[i];
                var fc = (FishControl)flpFishList.Controls[i];
                fc.UpdateDisplay(fish);
            }
        }

        private void OnFishClicked(object sender, EventArgs e)
        {
            if (!_isFeedingMode && !_isRemovingMode) return;

            var fishControl = (FishControl)sender;

            if (_isFeedingMode)
            {
                _engine.FeedSingleFish(fishControl.FishId);
                _isFeedingMode = false;
                btnFeedingMode.BackColor = SystemColors.Control;
            }
            else if (_isRemovingMode)
            {
                _engine.RemoveFish(fishControl.FishId);
                _isRemovingMode = false;
                btnRemoveFish.BackColor = SystemColors.Control;
            }
            Cursor = Cursors.Default;
        }
        #endregion

        #region Работа с плавающими рыбками
        private void SyncSwimmingFishs(Aquarium aquarium)
        {
            if (aquarium == null) return;

            _swimmingFishs.RemoveAll(sf => !aquarium.FishList.Contains(sf.Model) || !sf.Model.IsAlive);

            foreach (var fish in aquarium.FishList)
            {
                if (!_swimmingFishs.Any(sf => sf.Model == fish) && fish.IsAlive)
                {
                    Image originalImg = _fishFactory.GetFishImage(fish.Type);
                    Random rnd = new Random();
                    float x = 20, y = 20;

                    var fishWidth = GameConstants.StandartFishImageWidth;
                    var fishHeight = GameConstants.StandartFishImageHeight;

                    if (picAquarium.Width > fishWidth)
                        x = rnd.Next(20, picAquarium.Width - fishWidth);
                    if (picAquarium.Height > fishHeight)
                        y = rnd.Next(20, picAquarium.Height - fishHeight);
                    _swimmingFishs.Add(new SwimmingFish(fish, originalImg, x, y, _fishFactory.IsDefaultRight(fish.Type), fishWidth, fishHeight));
                }
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (picAquarium.Width == 0) return;
            foreach (var sf in _swimmingFishs)
            {
                sf.Update(picAquarium.Width, picAquarium.Height);
            }
            picAquarium.Invalidate();
        }

        private void PicAquarium_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var sf in _swimmingFishs)
            {
                if (sf.Model.IsAlive)
                    g.DrawImage(sf.Image, sf.Position);
                else
                {
                    using (var attr = new System.Drawing.Imaging.ImageAttributes())
                    {
                        var cm = new System.Drawing.Imaging.ColorMatrix { Matrix33 = 0.4f };
                        attr.SetColorMatrix(cm);
                        g.DrawImage(sf.Image, new Rectangle((int)sf.Position.X, (int)sf.Position.Y, sf.Image.Width, sf.Image.Height),
                            0, 0, sf.Image.Width, sf.Image.Height, GraphicsUnit.Pixel, attr);
                    }
                }
            }
        }
        #endregion

        #region Обработка нажатий на кнопки
        private void btnFeedAll_Click(object sender, EventArgs e) => _engine.FeedAllFish();

        private void btnFeedingMode_Click(object sender, EventArgs e)
        {
            _isRemovingMode = false;
            btnRemoveFish.BackColor = SystemColors.Control;

            _isFeedingMode = !_isFeedingMode;
            btnFeedingMode.BackColor = _isFeedingMode ? Color.LightGreen : SystemColors.Control;
            Cursor = _isFeedingMode ? Cursors.Hand : Cursors.Default;
        }

        private void btnChangeWater_Click(object sender, EventArgs e) => _engine.ChangeWater();

        private void btnRemoveFish_Click(object sender, EventArgs e)
        {
            _isFeedingMode = false;
            btnFeedingMode.BackColor = SystemColors.Control;

            _isRemovingMode = !_isRemovingMode;
            btnRemoveFish.BackColor = _isRemovingMode ? Color.Red : SystemColors.Control;
            Cursor = _isRemovingMode ? Cursors.Hand : Cursors.Default;
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            var shopForm = new ShopForm(_engine);
            shopForm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e) => _engine.SaveGame();

        private void btnLoad_Click(object sender, EventArgs e) => _engine.LoadGame();

        private void cmbAquariums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAquariums.SelectedIndex != _engine.Player.CurrentAquariumIndex)
                _engine.SwitchAquarium(cmbAquariums.SelectedIndex);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            _engine.NewGame();
        }

        #endregion
    }
}
