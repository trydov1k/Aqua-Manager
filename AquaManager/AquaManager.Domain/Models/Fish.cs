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
    public string Name { get; private set; }
    public FishType Type { get; }
    public double Hunger { get; private set; }
    public double HungerRate { get; }
    public decimal Price { get; }
    public decimal IncomeValue { get; }
    public bool IsAlive { get; private set; }
    public string Id { get; private set; }

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
    }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public bool TryChangeId(string newId)
    {
        var needToChange = string.IsNullOrEmpty(Id);
        if (needToChange)
            Id = newId;
        return needToChange;
    }
}