using AquaManager.Domain.Interfaces.Models;
using System.Text.Json.Serialization;

namespace AquaManager.Domain.Models;
/// <summary>
/// Класс, описывающий логику игрока
/// </summary>
public class Player : IPlayer
{
    // Свойства
    public decimal Money { get; set; }

    public List<Aquarium> Aquariums { get; set; }
    public int CurrentAquariumIndex { get; set; }

    // Конструкторы
    [JsonConstructor]
    public Player(decimal money, List<Aquarium> aquariums, int currentAquariumIndex)
    {
        Money = money;
        Aquariums = aquariums;
        CurrentAquariumIndex = currentAquariumIndex;
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
        if (0 <= CurrentAquariumIndex && CurrentAquariumIndex <= Aquariums.Count - 1)
            return Aquariums[CurrentAquariumIndex];
        return null;
    }

    public bool SpendMoney(decimal amount)
    {
        var canAfford = CanAfford(amount);
        if (canAfford)
            Money -= amount;
        return canAfford;
    }
}