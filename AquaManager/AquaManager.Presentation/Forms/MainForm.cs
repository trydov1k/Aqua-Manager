using AquaManager.Domain.Constants;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using AquaManager.Forms;
using AquaManager.Presentation.Controls;
using AquaManager.Presentation.Enums;
using AquaManager.Presentation.Extensions;
using AquaManager.Presentation.Models;
using Timer = System.Windows.Forms.Timer;

namespace AquaManager.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private GameEngine _engine;
        private FishFactory _fishFactory => _engine._fishFactory;
        private bool _isFeedingMode;
        private bool _isRemovingMode;

        private List<SwimmingFish> _swimmingFishs = new List<SwimmingFish>();
        private Timer _animationTimer;        

        public MainForm()
        {
            InitializeComponent();

            _animationTimer = new Timer();

            _animationTimer.Interval = GameConstants.AnimationTimerIntervalMs;
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();

            picAquarium.Paint += PicAquarium_Paint;
            //picAquarium.MouseClick += PicAquarium_MouseClick;  в будущем сделать, чтобы кормить рыбок можно было по нажатию на рыбку в аквариуме

            _engine = new GameEngine();
            _engine.StateChanged += OnEngineStateChanged;
            _engine.Start();

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            this.FormClosing += MainForm_FormClosing;
            this.Disposed += DisposeGame;
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

            if (flpFishList.Controls.Count != aquarium.FishList.Count)
                RebuildFishList(aquarium);
            else
                UpdateExistingFishControls(aquarium);

            SyncSwimmingFishs(aquarium);  // Добавляем плавающих рыбок
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

            if (_engine.IsFirstGame)
            {
                OpenTutorial();
                _engine.IsFirstGame = false;
                _animationTimer.Start();
                _engine.Start();
            }
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

        #region Работа с меню и обучением
        private void OpenMenu()
        {
            _engine.Stop();
            _animationTimer.Stop();

            var menuForm = new GameMenuForm();
            menuForm.ShowDialog();

            switch (menuForm.SelectedAction)
            {
                case MenuAction.None:  // Форма закрыта
                case MenuAction.Continue:  // Нажата кнопка "Продолжить игру"
                    _engine.Start();
                    _animationTimer.Start();
                    break;
                case MenuAction.Save:  // Нажата кнопка "Сохранить игру"
                    var nameInputForm = new NameInputForm(NameInputType.Save, 
                        SaveLoadConstants.DefaultGameSaveName,
                        "💾");
                    nameInputForm.ShowDialog();

                    if (nameInputForm.EnteredName == string.Empty)
                    {
                        _engine.Start();
                        _animationTimer.Start();
                        break;
                    }
                    _engine.SaveGame(nameInputForm.EnteredName);
                    _engine.SaveGame(SaveLoadConstants.DefaultSystemGameSaveFileName);

                    MessageBox.Show("Игра сохранена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _engine.Start();
                    _animationTimer.Start();
                    break;
                case MenuAction.Load:  // Нажата кнопка "Загрузить игру"
                    var selectSaveForm = new SelectSaveForm(
                        _engine.GiveAllSaveFileNames(), 
                        SaveLoadConstants.DefaultGameSaveName,
                        _engine.DeleteSaveFile
                        );                    
                    selectSaveForm.ShowDialog();

                    if (selectSaveForm.SelectedSave == string.Empty)
                    {
                        _engine.Start();
                        _animationTimer.Start();
                        break;
                    }

                    _engine.LoadGame(selectSaveForm.SelectedSave);

                    selectSaveForm.Dispose();

                    MessageBox.Show("Игра загружена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _engine.Start();
                    _animationTimer.Start();
                    break;
                case MenuAction.Tutorial:
                    OpenTutorial();
                    break;
                case MenuAction.NewGame:  // Нажата кнопка "Новая игра"
                    _engine.NewGame();
                    _animationTimer.Start();
                    break;
                case MenuAction.Exit:  // Нажата кнопка "Выйти из игры"
                    this.Close();
                    break;
            }

            menuForm.Dispose();
        }

        private void OpenTutorial()
        {
            if (_engine.IsRunning)
            {
                _engine.Stop();
                _animationTimer.Stop();
            }
            var tutorialForm = new TutorialForm();
            tutorialForm.ShowDialog();
            tutorialForm.Dispose();
            
            if (!_engine.IsRunning)
            {
                _engine.Start();
                _animationTimer.Start();
            }            
        }
        #endregion

        #region Обработка нажатий на кнопки
        private void btnFeedAll_Click(object sender, EventArgs e) 
            => _engine.FeedAllFish();

        private void btnFeedingMode_Click(object sender, EventArgs e)
            => DoFeedingMode();

        private void btnChangeWater_Click(object sender, EventArgs e)
            => _engine.ChangeWater();

        private void btnRemoveFish_Click(object sender, EventArgs e)
            => DoRemoveMode();

        private void btnShop_Click(object sender, EventArgs e)
        {
            var shopForm = new ShopForm(_engine);
            shopForm.ShowDialog();

            shopForm.Dispose();
        }

        private void btnGameMenu_Click(object sender, EventArgs e)
        {
            OpenMenu();
        }

        private void cmbAquariums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAquariums.SelectedIndex != _engine.Player.CurrentAquariumIndex)
                _engine.SwitchAquarium(cmbAquariums.SelectedIndex);
        }

        #endregion

        #region Вспомогательные методы для обработки нажатий на кнопки

        private void DoFeedingMode()
        {
            _isRemovingMode = false;
            btnRemoveFish.BackColor = SystemColors.Control;

            _isFeedingMode = !_isFeedingMode;
            btnFeedingMode.BackColor = _isFeedingMode ? Color.LightGreen : SystemColors.Control;
            Cursor = _isFeedingMode ? Cursors.Hand : Cursors.Default;
        }

        private void DoRemoveMode()
        {
            _isFeedingMode = false;
            btnFeedingMode.BackColor = SystemColors.Control;

            _isRemovingMode = !_isRemovingMode;
            btnRemoveFish.BackColor = _isRemovingMode ? Color.Red : SystemColors.Control;
            Cursor = _isRemovingMode ? Cursors.Hand : Cursors.Default;
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Escape:
                    OpenMenu();
                    return true;  // Меню
                case Keys.F1:
                    OpenTutorial();
                    return true;  // Обучение
                case Keys.Right:  // Переключение аквариума вправо
                case Keys.D:
                    if (_engine.Player.CurrentAquariumIndex + 1 < _engine.Player.Aquariums.Count)
                        _engine.Player.CurrentAquariumIndex++;
                    return true;
                case Keys.Left:  // Переключение аквариума влево
                case Keys.A:
                    if (_engine.Player.CurrentAquariumIndex - 1 >= 0)
                        _engine.Player.CurrentAquariumIndex--;
                    return true;

                case Keys.E:
                    DoFeedingMode();
                    return true;  // Режим кормления
                case Keys.R:
                    DoRemoveMode();
                    return true;  // Удалить рыбку
                case Keys.F:
                    _engine.ChangeWater();
                    return true;  // Поменять воду в аквариуме
                case Keys.Q:
                    _engine.FeedAllFish();
                    return true;  // Покормить всех

                case Keys.D1:
                    break;  // Покормить/удалить рыбку 1
                case Keys.D2:
                    break;  // Покормить/удалить рыбку 2
                case Keys.D3:
                    break;  // Покормить/удалить рыбку 3
                case Keys.D4:
                    break;  // Покормить/удалить рыбку 4
                case Keys.D5:
                    break;  // Покормить/удалить рыбку 5
                case Keys.D6:
                    break;  // Покормить/удалить рыбку 6

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void DisposeGame(object sender, EventArgs e)
        {
            _engine.Dispose();
            _animationTimer.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            var result = MessageBox.Show(
                "Выйти из игры?\nНесохранённый прогресс будет потерян.",
                "Выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Если пользователь выбрал No (Нет), отменяем закрытие
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
