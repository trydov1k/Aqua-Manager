using AquaManager.Domain.Interfaces.Models;

namespace AquaManager.Domain.Models;
/// <summary>
/// Класс, описывающий логику аквариума
/// </summary>
public class Aquarium : IAquarium
{
    // Свойства
    public string Name { get; }

    public double WaterCleanliness { get; private set; }

    public List<Fish> FishList { get; }

    public int Capacity { get; }

    // Конструкторы
    public Aquarium(string name, int capacity)
    {
        Name = name;
        WaterCleanliness = 100;
        FishList = new List<Fish>();
        Capacity = capacity;
    }

    // Методы
    public bool CanAddFish() => FishList.Count + 1 <= Capacity;

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

    public void RemoveDeadFish()
    {
        FishList.RemoveAll(fish => fish.IsDie);
    }

    public void UpdateWaterCleanliness(double waterDirtRate)
    {
        WaterCleanliness -= waterDirtRate;
        if (WaterCleanliness < 0)
            WaterCleanliness = 0;
    }
}