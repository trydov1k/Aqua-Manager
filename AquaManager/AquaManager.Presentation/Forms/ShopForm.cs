using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using AquaManager.Forms;
using AquaManager.Presentation.Controls;
using AquaManager.Presentation.Extensions;

namespace AquaManager.Presentation.Forms
{
    public partial class ShopForm : Form
    {
        private GameEngine _engine;
        private FishFactory _fishFactory => _engine._fishFactory;
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
                
                var fishControl = new ShopItemControl(
                    _fishFactory.GetFishImage(type),
                    _fishFactory.GetFishName(type),
                    _fishFactory.GetFishPrice(type),
                    _fishFactory.GetFishDescription(type),
                    Color.LightGreen,
                    () => BuyFish(type));
                flpItems.Controls.Add(fishControl);
            }

            // 2. Добавляем разделитель (просто панель с отступом)
            flpItems.Controls.Add(new Panel { Height = 5, BackColor = Color.LightGray, Width = 380 });

            // 3. Добавляем аквариум
            var aquariumControl = new ShopItemControl(
                Properties.Resources.Аквариум_1,
                "Новый аквариум",
                GameConstants.NewAquariumPrice,
                $"вместимость: {GameConstants.DefaultAquariumCapacity} рыбок",
                Color.LightBlue,
                () => BuyAquarium()
                );

            flpItems.Controls.Add(aquariumControl);
        }

        private void BuyFish(FishType type)
        {
            var name = _fishFactory.GetFishName(type);

            if (_engine.CanBuyFish(type))
            {
                using var dlg = new NameInputForm(FishOrAquarium.Fish, name);
                if (dlg.ShowDialog() == DialogResult.OK)
                    name = dlg.EnteredName;
            }            

            bool success = _engine.BuyFish(type, name);

            if (success)
            {
                UpdateMoneyDisplay();
                //MessageBox.Show("Рыбка куплена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var name = $"Аквариум {_engine.Player.Aquariums.Count + 1}";
            if (_engine.CanBuyAquarium())
            {
                using var dlg = new NameInputForm(FishOrAquarium.Aquarium, name);
                if (dlg.ShowDialog() == DialogResult.OK)
                    name = dlg.EnteredName;
            }            

            bool success = _engine.BuyAquarium(name);
            
            if (success)
            {
                UpdateMoneyDisplay();
                //MessageBox.Show("Новый аквариум куплен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
