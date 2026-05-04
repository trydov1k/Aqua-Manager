using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Tests.DomainTests.ModelsTests
{
    [TestFixture]
    public class AquariumTests
    {
        [Test]
        public void Constructor_ShouldSetNameAndCapacity()
        {
            var aquarium = new Aquarium("TestAqua", 5);
            Assert.AreEqual("TestAqua", aquarium.Name);
            Assert.AreEqual(5, aquarium.Capacity);
            Assert.AreEqual(100, aquarium.WaterCleanliness);
            Assert.IsEmpty(aquarium.FishList);
        }

        [Test]
        public void CanAddFish_WhenSpaceAvailable_ReturnsTrue()
        {
            var aquarium = new Aquarium("Test", 2);
            Assert.IsTrue(aquarium.CanAddFish());
            aquarium.AddFish(CreateStandartFish());
            Assert.IsTrue(aquarium.CanAddFish());
            aquarium.AddFish(CreateStandartFish());
            Assert.IsFalse(aquarium.CanAddFish());
        }

        [Test]
        public void AddFish_WhenSpaceAvailable_AddsFish()
        {
            var aquarium = new Aquarium("Test", 1);
            var fish = CreateStandartFish();
            var result = aquarium.AddFish(fish);
            Assert.IsTrue(result);
            Assert.AreEqual(1, aquarium.FishList.Count);
            Assert.AreEqual(fish, aquarium.FishList[0]);
        }

        [Test]
        public void AddFish_WhenFull_ReturnsFalse()
        {
            var aquarium = new Aquarium("Test", 1);
            aquarium.AddFish(CreateStandartFish());
            var secondFish = CreateStandartFish();
            Assert.IsFalse(aquarium.AddFish(secondFish));
        }

        [Test]
        public void RemoveFish_ShouldRemoveOneFish()
        {
            var aquarium = new Aquarium("Test", 3);

            var live = CreateStandartFish();
            var dead = CreateDeadFish();

            aquarium.AddFish(live);
            aquarium.AddFish(dead);

            aquarium.RemoveFish(live);

            Assert.AreEqual(1, aquarium.FishList.Count);
            Assert.AreEqual(dead, aquarium.FishList[0]);
        }

        [Test]
        public void CleanWater_ShouldSetWaterCleanlinessTo100WhenHaveFish()
        {
            var aquarium = new Aquarium("Test", 3);
            aquarium.AddFish(CreateStandartFish());
            aquarium.UpdateWaterCleanliness(50);
            Assert.AreEqual(50, aquarium.WaterCleanliness);
            aquarium.CleanWater();
            Assert.AreEqual(100, aquarium.WaterCleanliness);
        }

        [Test]
        public void CleanWater_ShouldDontSetWaterCleanlinessTo100WhenDontHaveFish()
        {
            var aquarium = new Aquarium("Test", 3);
            aquarium.UpdateWaterCleanliness(50);
            Assert.AreEqual(100, aquarium.WaterCleanliness);
        }

        [Test]
        public void UpdateWaterCleanliness_ShouldDecreaseByRate()
        {
            var aquarium = new Aquarium("Test", 3);
            aquarium.AddFish(CreateStandartFish());
            aquarium.UpdateWaterCleanliness(10);
            Assert.AreEqual(90, aquarium.WaterCleanliness);
            aquarium.UpdateWaterCleanliness(200);
            Assert.AreEqual(0, aquarium.WaterCleanliness);
        }

        [Test]
        public void GetLiveFishCount_ShouldReturnCorrectCount()
        {
            var aquarium = new Aquarium("Test", 3);
            var live1 = CreateStandartFish();
            var live2 = CreateStandartFish();
            var dead = CreateDeadFish();
            aquarium.AddFish(live1);
            aquarium.AddFish(live2);
            aquarium.AddFish(dead);
            Assert.AreEqual(2, aquarium.GetLiveFishCount());
        }

        // Вспомогательные методы
        #region Вспомогательные методы
        private static (decimal, double, string, decimal) GuppyConstants = GameConstants.FishByTypeDict[FishType.Guppy];
        private static string FishName = GuppyConstants.Item3;
        private static decimal FishPrice = GuppyConstants.Item1;
        private static double FishHungerRate = GuppyConstants.Item2;
        private static decimal FishIncomeValue = GuppyConstants.Item4;
        public Fish CreateStandartFish() => new Fish(FishName, FishType.Guppy, FishHungerRate, FishPrice, FishIncomeValue);
        private Fish CreateHungredFish()
        {
            var fish = CreateStandartFish();
            while (fish.Hunger - FishHungerRate > 0)
                fish.UpdateHunger();

            Assert.AreEqual(FishHungerRate, fish.Hunger, 0.001);
            return fish;
        }

        private Fish CreateDeadFish()
        {
            var fish = CreateHungredFish();
            fish.UpdateHunger();

            Assert.IsTrue(fish.IsDie);

            return fish;
        }
        #endregion
    }
}