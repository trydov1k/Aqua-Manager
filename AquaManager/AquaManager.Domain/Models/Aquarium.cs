using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces.Models;
using System.Text.Json.Serialization;

namespace AquaManager.Domain.Models;
/// <summary>
/// Класс, описывающий логику аквариума
/// </summary>
public class Aquarium : IAquarium
{
    // Свойства
    public string Name { get; }

    public AquariumType Type { get; }

    public double WaterCleanliness { get; private set; }

    public List<Fish> FishList { get; }

    public int Capacity { get; }

    // Конструкторы
    public Aquarium(string name, AquariumType type, int capacity)
        : this(name, type, 100, new List<Fish>(), capacity)
    { }

    [JsonConstructor]
    public Aquarium(string name, AquariumType type, double waterCleanliness, List<Fish> fishList, int capacity)
    {
        Name = name;
        Type = type;
        WaterCleanliness = waterCleanliness;
        FishList = fishList;
        Capacity = capacity;
    }

    // Методы
    public bool CanAddFish() 
        => FishList.Count + 1 <= Capacity;

    public bool AddFish(Fish fish)
    {
        if (!CanAddFish())
            return false;

        FishList.Add(fish);
        return true;
    }

    public bool RemoveFish(Fish fish)
    {
        return FishList.Remove(fish);
    }

    public void CleanWater()
    {
        WaterCleanliness = 100.00;
    }

    public int GetLiveFishCount()
    {
        return FishList.Where(fish => fish.IsAlive).Count();
    }

    public void UpdateWaterCleanliness(double waterDirtRatePerFish)
    {
        WaterCleanliness -= waterDirtRatePerFish * GetLiveFishCount();
        if (WaterCleanliness < 0)
            WaterCleanliness = 0;
    }
}