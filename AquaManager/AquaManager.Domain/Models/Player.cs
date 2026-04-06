using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
    public class Player : IPlayer
    {
        // Свойства
        public int Money { get; set; }

        public List<Aquarium> Aquariums { get; set; }
        public int CurrentAquariumIndex { get; set; }

        // Конструкторы
        public Player(int money, List<Aquarium> aquariums, int aquariumIndex)
        {
            Money = money;
            Aquariums = aquariums;
            CurrentAquariumIndex = aquariumIndex;
        }

        public Player() : this(0, [], 0)
        { }

        // Методы
        public void AddMoney(int amount)
        {
            Money += amount;
        }

        public bool CanAfford(int amount)
        {
            return Money <= amount;
        }

        public Aquarium GetCurrentAquarium()
        {
            return Aquariums[CurrentAquariumIndex];
        }

        public bool SpendMoney(int amount)
        {
            var canAfford = CanAfford(amount);
            if (canAfford)
                Money -= amount;
            return canAfford;
        }
    }
}