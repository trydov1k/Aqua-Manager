using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Services;
using AquaManager.Presentation.Controls;
using AquaManager.Presentation.Enums;
using AquaManager.Presentation.Extensions;

namespace AquaManager.Presentation.Forms
{
    public partial class ShopForm : Form
    {
        private GameEngine _engine;
        private FishFactory _fishFactory => _engine._fishFactory;
        private AquariumFactory _aquariumFactory => _engine._aquariumFactory;
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
            var fishTypes = _fishFactory.GetAllFishTypes();
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

            // 3. Добавляем аквариумы
            var aquariumTypes = _aquariumFactory.GetAllAquariumTypes();
            foreach (AquariumType type in aquariumTypes)
            {
                var aquariumControl = new ShopItemControl(
                    _aquariumFactory.GetAquariumImage(type),
                    _aquariumFactory.GetAquariumStandartName(type),
                    _aquariumFactory.GetAquariumPrice(type),
                    _aquariumFactory.GetAquariumDescription(type),
                    Color.LightBlue,
                    () => BuyAquarium(type)
                    );
                flpItems.Controls.Add(aquariumControl);
            }
        }

        private void BuyFish(FishType type)
        {
            var name = _fishFactory.GetFishName(type);

            var aquarium = _engine.GetCurrentAquarium();

            if (_engine.CanBuyFish(type) && (aquarium?.CanAddFish() ?? true))
            {
                var nameInputForm = new NameInputForm(NameInputFormType.Fish, name);
                nameInputForm.ShowDialog();

                if (nameInputForm.EnteredName == string.Empty)
                    return;

                name = nameInputForm.EnteredName;
                nameInputForm.Dispose();
            }

            bool success = _engine.BuyFish(type, name);

            if (!success)
            {
                if (aquarium != null && aquarium.FishList.Count >= aquarium.Capacity)
                    MessageBox.Show("Нет места в аквариуме!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Недостаточно денег!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateMoneyDisplay();
        }

        private void BuyAquarium(AquariumType type)
        {
            var standartName = _aquariumFactory.GetAquariumStandartName(type);
            var name = $"{standartName} {_engine.Player.Aquariums.Where(a => a.Type == type).Count() + 1}";
            if (_engine.CanBuyAquarium(type))
            {
                var nameInputForm = new NameInputForm(NameInputFormType.Aquarium, name);
                nameInputForm.ShowDialog();
                if (nameInputForm.EnteredName == string.Empty)
                    return;
                name = nameInputForm.EnteredName;
                nameInputForm.Dispose();
            }

            bool success = _engine.BuyAquarium(type, name);

            if (!success)
            {
                MessageBox.Show("Недостаточно денег для покупки аквариума!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateMoneyDisplay();
        }

        private void UpdateMoneyDisplay()
        {
            lblMoney.Text = $"У вас: {_engine.Player.Money} монет";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
