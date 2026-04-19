using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using AquaManager.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AquaManager.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private GameEngine _engine;
        private bool _isFeedingMode;
        public MainForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            _engine = new GameEngine();
            _engine.StateChanged += OnEngineStateChanged;
            _engine.Start();
        }

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
        }

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
                fc.UpdateDisplay();
            }
        }

        private void OnFishClicked(object sender, EventArgs e)
        {
            if (!_isFeedingMode) return;
            var fishControl = (FishControl)sender;
            _engine.FeedSingleFish(fishControl.FishId);
            _isFeedingMode = false;
            btnFeedingMode.BackColor = SystemColors.Control;
            Cursor = Cursors.Default;
        }
        #endregion

        #region Обработка нажатий на кнопки
        private void btnFeedAll_Click(object sender, EventArgs e) => _engine.FeedAllFish();

        private void btnFeedingMode_Click(object sender, EventArgs e)
        {
            _isFeedingMode = !_isFeedingMode;
            btnFeedingMode.BackColor = _isFeedingMode ? Color.LightGreen : SystemColors.Control;
            Cursor = _isFeedingMode ? Cursors.Hand : Cursors.Default;
        }

        private void btnChangeWater_Click(object sender, EventArgs e) => _engine.ChangeWater();

        private void btnRemoveDead_Click(object sender, EventArgs e) => _engine.RemoveDeadFish();
        

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
        #endregion
    }
}
