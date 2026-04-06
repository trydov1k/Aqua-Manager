using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
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
        public bool AddFish(Fish fish)
        {
            var canAdd = FishList.Count + 1 < Capacity;
            if (!canAdd)
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
            foreach (var fish in FishList)
                if (fish.IsDie)
                    FishList.Remove(fish);
        }

    }
}