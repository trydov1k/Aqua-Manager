using AquaManager.Domain.Models;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Constants;

namespace AquaManager.Tests.DomainTests.ModelsTests
{
    [TestFixture]
    public class FishTests
    {
        private static (decimal, double, string, decimal) GuppyConstants = GameConstants.FishByTypeDict[FishType.Guppy];
        private static string FishName = GuppyConstants.Item3;
        private static decimal FishPrice = GuppyConstants.Item1;
        private static double FishHungerRate = GuppyConstants.Item2;
        private static decimal FishIncomeValue = GuppyConstants.Item4;
        private Fish CreateStandartFish() => new Fish(FishName, FishType.Guppy, FishHungerRate, FishPrice, FishIncomeValue);
        private Fish CreateHungredFish()
        {
            var fish = CreateStandartFish();
            while (fish.Hunger - FishHungerRate > 0)
                fish.UpdateHunger();

            Assert.AreEqual(FishHungerRate, fish.Hunger, 0.001);
            return fish;
        }

        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var fish = CreateStandartFish();

            Assert.AreEqual(FishName, fish.Name);
            Assert.AreEqual(FishType.Guppy, fish.Type);
            Assert.AreEqual(100, fish.Hunger);
            Assert.AreEqual(FishHungerRate, fish.HungerRate);
            Assert.AreEqual(FishPrice, fish.Price);            
            Assert.AreEqual(FishIncomeValue, fish.IncomeValue);            
            Assert.IsTrue(fish.IsAlive);
            Assert.IsNotNull(fish.Id);
        }

        [Test]
        public void UpdateHunger_ShouldDecreaseHungerByRate()
        {
            var fish = CreateStandartFish();
            fish.UpdateHunger();
            Assert.AreEqual(100 - FishHungerRate, fish.Hunger, 0.001);

            for (int i = 0; i < 10; i++)
                fish.UpdateHunger();
            Assert.AreEqual(100 - FishHungerRate * 11, fish.Hunger, 0.001);
        }

        [Test]
        public void Feed_ShouldSetHungerTo100()
        {
            var fish = CreateHungredFish();

            fish.Feed();
            Assert.AreEqual(100, fish.Hunger);
        }

        [Test]
        public void Kill_ShouldSetIsAliveFalseAndHungerZero()
        {
            var fish = CreateStandartFish();

            fish.Kill();

            Assert.IsFalse(fish.IsAlive);
            Assert.AreEqual(0, fish.Hunger);
        }

        [Test]
        public void Feed_OnDeadFish_ShouldNotChangeHunger()
        {
            var fish = CreateHungredFish();

            fish.Kill();

            fish.Feed();

            Assert.AreEqual(0, fish.Hunger);
        }

        [Test]
        public void UpdateHunger_WhenHungerReachesZero_ShouldKillFish()
        {
            var fish = CreateHungredFish();

            Assert.IsTrue(fish.IsAlive);

            fish.UpdateHunger();

            Assert.IsFalse(fish.IsAlive);
            Assert.AreEqual(0, fish.Hunger);
        }


    }
}