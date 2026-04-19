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

        private void flpFishList_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
