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
            throw new NotImplementedException();
        }

        public bool CanAfford(int amount)
        {
            throw new NotImplementedException();
        }

        public Aquarium GetCurrentAquarium()
        {
            throw new NotImplementedException();
        }

        public bool SpendMoney(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
