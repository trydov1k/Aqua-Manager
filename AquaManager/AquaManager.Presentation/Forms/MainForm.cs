using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
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
            // Обновить ComboBox аквариумов
            // Обновить чистоту воды
            // Перестроить список рыбок
            // ...
        }

        private void btnFeedAll_Click(object sender, EventArgs e)
        {
            _engine.FeedAllFish();
        }

        private void btnFeedingMode_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeWater_Click(object sender, EventArgs e)
        {
            _engine.ChangeWater();
        }

        private void btnRemoveDead_Click(object sender, EventArgs e)
        {
            _engine.RemoveDeadFish();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            var shopForm = new ShopForm(_engine);
            shopForm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _engine.SaveGame();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            
        }

        private void cmbAquariums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAquariums.SelectedIndex != _engine.Player.CurrentAquariumIndex)
                _engine.SwitchAquarium(cmbAquariums.SelectedIndex);
        }
    }
}
