using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;

namespace AquaManager.Tests.DomainTests.ServicesTests
{
    [TestFixture]
    public class IncomeServiceTests
    {
        [Test]
        public void CalculateTotalIncome_ShouldSumIncomeValueOfAllLiveFish()
        {
            var player = new Player(100, new List<Aquarium>(), 0);
            var aquarium = new Aquarium("Test", 5);
            var fish1 = CreateStandartFish();
            var fish2 = CreateStandartFish();
            var deadFish = CreateDeadFish();
            aquarium.AddFish(fish1);
            aquarium.AddFish(fish2);
            aquarium.AddFish(deadFish);
            player.Aquariums.Add(aquarium);

            var service = new IncomeService(player);
            
            bool incomeRaised = false;
            decimal raisedAmount = 0;
            service.IncomeGenerated += (s, e) => { incomeRaised = true; raisedAmount = e.Amount; };
            service.Start();
            
            var method = typeof(IncomeService).GetMethod("CalculateTotalIncome", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var total = (decimal)method.Invoke(service, null);
            Assert.AreEqual(FishIncomeValue + FishIncomeValue, total);
            service.Stop();
            service.Dispose();
        }

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