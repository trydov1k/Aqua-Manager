using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces.Models;

namespace AquaManager.Domain.Models;
/// <summary>
/// Класс, описывающий логику рыбки
/// </summary>
public class Fish : IFish
{
    // Свойства рыбки
    public string Name { get; }
    public FishType Type { get; }
    public double Hunger { get; private set; }
    public double HungerRate { get; }
    public decimal Price { get; private set; }
    public bool IsAlive { get; private set; }
    public string Id { get; set; }

    // Лямбда-свойства
    public bool IsDie => !IsAlive;

    // Конструкторы
    public Fish(string name, FishType type, double hungerRate, decimal price)
    {
        Name = name;
        Type = type;
        Hunger = 100;
        HungerRate = hungerRate;
        Price = price;
        IsAlive = true;
        Id = Guid.NewGuid().ToString();
    }

    // Методы класса

    public void Feed()
    {
        Hunger = 100.00;
    }

    public void UpdateHunger()
    {
        Hunger -= HungerRate;
    }

    public void Kill()
    {
        IsAlive = false;
        Hunger = 0;
        Price = 0;
    }
}