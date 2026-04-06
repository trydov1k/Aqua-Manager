using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
    public class Fish : IFish
    {
        // Свойства рыбки
        public string Name { get; }
        public FishType Type { get; }
        public double Hunger { get; private set; }
        public double HungerRate { get; }
        public double Price { get; private set; }
        public bool IsAlive { get; private set; }

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
            Hunger = 100.00;
        }

        public void UpdateHunger()
        {
            Hunger -= HungerRate;
        }

        public void Die()
        {
            IsAlive = false;
            Price = 0.00;
        }
    }
}