using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
    public class Aquarium : IAquarium
    {
        // Свойства
        public string Name { get; }

        public double WaterCleanliness { get; }

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
            throw new NotImplementedException();
        }

        public void CleanWater()
        {
            throw new NotImplementedException();
        }

        public int GetLiveFishCount()
        {
            throw new NotImplementedException();
        }

        public void RemoveDeadFish()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFish(Fish fish)
        {
            throw new NotImplementedException();
        }
    }
}