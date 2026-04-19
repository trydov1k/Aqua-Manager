using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
    public class Player : IPlayer
    {
        // Свойства
        public decimal Money { get; set; }

        public List<Aquarium> Aquariums { get; set; }
        public int CurrentAquariumIndex { get; set; }

        // Конструкторы
        public Player(decimal money, List<Aquarium> aquariums, int aquariumIndex)
        {
            Money = money;
            Aquariums = aquariums;
            CurrentAquariumIndex = aquariumIndex;
        }

        // Методы
        public void AddMoney(decimal amount)
        {
            Money += amount;
        }

        public bool CanAfford(decimal amount)
        {
            return Money >= amount;
        }

        public Aquarium GetCurrentAquarium()
        {
            return Aquariums[CurrentAquariumIndex];
        }

        public bool SpendMoney(decimal amount)
        {
            var canAfford = CanAfford(amount);
            if (canAfford)
                Money -= amount;
            return canAfford;
        }
    }
}