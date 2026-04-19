using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AquaManager.Presentation.Forms
{
    public partial class ShopForm : Form
    {
        private GameEngine _engine;
        public ShopForm(GameEngine gameEngine)
        {
            _engine = gameEngine;
            InitializeComponent();
            LoadShopItems();
            UpdateMoneyDisplay();
        }

        private void LoadShopItems()
        {
            // 1. Добавляем рыбок
            var fishTypes = Enum.GetValues(typeof(FishType));
            foreach (FishType type in fishTypes)
            {
                var fishPanel = CreateFishPanel(type);
                flpItems.Controls.Add(fishPanel);
            }

            // 2. Добавляем разделитель (просто панель с отступом)
            flpItems.Controls.Add(new Panel { Height = 10 });

            // 3. Добавляем аквариум
            var aquariumPanel = CreateAquariumPanel();
            flpItems.Controls.Add(aquariumPanel);
        }

        private Panel CreateFishPanel(FishType type)
        {
            var panel = new Panel
            {
                Width = 380,
                Height = 70,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(3)
            };

            // Иконка (можно взять из ресурсов)
            var pbIcon = new PictureBox
            {
                Image = GetFishImage(type),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(5, 5),
                Size = new Size(50, 50)
            };

            // Название
            var lblName = new Label
            {
                Text = GetFishName(type),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(65, 10),
                AutoSize = true
            };

            // Цена
            int price = GetFishPrice(type);
            var lblPrice = new Label
            {
                Text = $"{price} монет",
                Location = new Point(65, 35),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };

            // Описание (скорость голодания)
            double rate = GetHungerRate(type);
            string rateDesc = rate switch
            {
                <= 0.25 => "медленно",
                <= 0.4 => "средне",
                <= 0.6 => "быстро",
                _ => "очень быстро"
            };
            var lblDesc = new Label
            {
                Text = $"голодает {rateDesc} ({rate}%/сек)",
                Location = new Point(65, 52),
                AutoSize = true,
                Font = new Font("Segoe UI", 7),
                ForeColor = Color.Gray
            };

            // Кнопка "Купить"
            var btnBuy = new Button
            {
                Text = "Купить",
                Location = new Point(290, 20),
                Size = new Size(80, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGreen,
                Tag = type
            };
            btnBuy.Click += (s, e) => BuyFish(type);

            panel.Controls.Add(pbIcon);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(lblDesc);
            panel.Controls.Add(btnBuy);

            return panel;
        }

        private Panel CreateAquariumPanel()
        {
            var panel = new Panel
            {
                Width = 380,
                Height = 70,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(3)
            };

            var pbIcon = new PictureBox
            {
                Image = Properties.Resources.Аквариум_1, // добавьте иконку в ресурсы
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(5, 5),
                Size = new Size(50, 50)
            };

            var lblName = new Label
            {
                Text = "Новый аквариум",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(65, 10),
                AutoSize = true
            };

            decimal price = GameConstants.NewAquariumPrice;
            var lblPrice = new Label
            {
                Text = $"{price} монет",
                Location = new Point(65, 35),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };

            var lblDesc = new Label
            {
                Text = $"вместимость: {GameConstants.DefaultAquariumCapacity} рыбок",
                Location = new Point(65, 52),
                AutoSize = true,
                Font = new Font("Segoe UI", 7),
                ForeColor = Color.Gray
            };

            var btnBuy = new Button
            {
                Text = "Купить",
                Location = new Point(290, 20),
                Size = new Size(80, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue
            };
            btnBuy.Click += (s, e) => BuyAquarium();

            panel.Controls.Add(pbIcon);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(lblDesc);
            panel.Controls.Add(btnBuy);

            return panel;
        }

        private void BuyFish(FishType type)
        {
            bool success = _engine.BuyFish(type);
            if (success)
            {
                UpdateMoneyDisplay();
                MessageBox.Show("Рыбка куплена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var aquarium = _engine.GetCurrentAquarium();
                if (aquarium != null && aquarium.FishList.Count >= aquarium.Capacity)
                    MessageBox.Show("Нет места в аквариуме!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Недостаточно денег!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BuyAquarium()
        {
            bool success = _engine.BuyAquarium();
            if (success)
            {
                UpdateMoneyDisplay();
                MessageBox.Show("Новый аквариум куплен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Недостаточно денег для покупки аквариума!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateMoneyDisplay()
        {
            lblMoney.Text = $"У вас: {_engine.Player.Money} монет";
        }

        // Вспомогательные методы (лучше вынести в отдельный класс-хелпер, но для простоты здесь)
        private Image GetFishImage(FishType type)
        {
            string name = type.ToString().ToLower();
            return (Image)Properties.Resources.ResourceManager.GetObject(name) ?? Properties.Resources.guppy;
        }

        private string GetFishName(FishType type) => type switch
        {
            FishType.Guppy => "Гуппи",
            FishType.SwordsMan => "Меченосец",
            FishType.Angelfish => "Скалярия",
            FishType.Goldfish => "Золотая рыбка",
            _ => type.ToString()
        };

        private int GetFishPrice(FishType type) => type switch
        {
            FishType.Guppy => 40,
            FishType.SwordsMan => 70,
            FishType.Angelfish => 100,
            FishType.Goldfish => 150,
            _ => 0
        };

        private double GetHungerRate(FishType type) => type switch
        {
            FishType.Guppy => 0.2,
            FishType.SwordsMan => 0.33,
            FishType.Angelfish => 0.5,
            FishType.Goldfish => 0.67,
            _ => 0
        };
    }
}
