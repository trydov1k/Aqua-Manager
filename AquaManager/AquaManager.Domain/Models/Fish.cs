using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces.Models;
using System.Text.Json.Serialization;

namespace AquaManager.Domain.Models;
/// <summary>
/// Класс, описывающий логику рыбки
/// </summary>
public class Fish : IFish
{
    // Свойства рыбки
    public string Name { get; set; }
    public FishType Type { get; }
    public double Hunger { get; private set; }
    public double HungerRate { get; }
    public decimal Price { get; private set; }
    public decimal IncomeValue { get; private set; }
    public bool IsAlive { get; private set; }
    public string Id { get; set; }

    // Лямбда-свойства
    public bool IsDie => !IsAlive;

    // Конструкторы
    public Fish(string name, FishType type, double hungerRate, decimal price, decimal incomeValue) :
        this(name, type, 100, hungerRate, price, incomeValue, true, Guid.NewGuid().ToString(), false)
    { }

    [JsonConstructor]
    public Fish(string name, FishType type, double hunger, double hungerRate, decimal price, 
        decimal incomeValue, bool isAlive, string id, bool isDie)
    {
        Name = name;
        Type = type;
        Hunger = hunger;
        HungerRate = hungerRate;
        Price = price;
        IncomeValue = incomeValue;
        IsAlive = isAlive;
        Id = id;
    }

    // Методы класса

    public void Feed()
    {
        if (IsAlive)
            Hunger = 100.00;
    }

    public void UpdateHunger()
    {
        Hunger -= HungerRate;
        if (Hunger <= 0)
            Kill();
    }

    public void Kill()
    {
        IsAlive = false;
        Hunger = 0;
        Price = 0;
    }
}