using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Interfaces.Models;
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
                Properties.Resources.Аквариум_2,
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

            var aquarium = _engine.GetCurrentAquarium();

            if (_engine.CanBuyFish(type) && aquarium?.FishList.Count +1 <= aquarium?.Capacity)
            {
                var nameInputForm = new NameInputForm(NameInputType.Fish, name);
                nameInputForm.ShowDialog();

                if (nameInputForm.EnteredName == string.Empty)
                    return;

                name = nameInputForm.EnteredName;
                nameInputForm.Dispose();
            }            

            bool success = _engine.BuyFish(type, name);

            if (success)
            {
                UpdateMoneyDisplay();
            }
            else
            {
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
                var nameInputForm = new NameInputForm(NameInputType.Aquarium, name);
                nameInputForm.ShowDialog();
                if (nameInputForm.EnteredName == string.Empty)
                    return;
                name = nameInputForm.EnteredName;
                nameInputForm.Dispose();
            }            

            bool success = _engine.BuyAquarium(name);
            
            if (success)
            {
                UpdateMoneyDisplay();
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
