using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces;
using System.Xml.Linq;

namespace AquaManager.Domain.Models
{
    public class Fish : IFish
    {
        // Свойства рыбки
        public string Name { get; }
        public FishType Type { get; }
        public int Hunger { get; }
        public double HungerRate { get; }
        public double Price { get; }
        public bool IsAlive { get; }

        // Лямбда-свойства
        public bool IsDie => !IsAlive;

        // Конструкторы
        public Fish(string name, FishType type, double hungerRate, double price)
        {
            Name = name;
            Type = type;
            Hunger = 100;
            HungerRate = hungerRate;
            Price = price;
            IsAlive = true;
        }

        public Fish(FishType type, double hungerRate, double price) : this(type.ToString(), type, hungerRate, price) { }

        // Методы класса

        public void Feed()
        {
            throw new NotImplementedException();
        }

        public void UpdateHunger()
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    }
}
