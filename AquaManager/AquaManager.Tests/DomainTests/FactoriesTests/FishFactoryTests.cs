using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using NUnit.Framework.Legacy;

namespace AquaManager.Tests.DomainTests.FactoriesTests
{
    [TestFixture]
    public class FishFactoryTests
    {
        private FishFactory _factory;

        [SetUp]
        public void SetUp() => _factory = new FishFactory();

        [TestCase (FishType.Guppy)]
        [TestCase (FishType.SwordsMan)]
        [TestCase (FishType.Angelfish)]
        [TestCase (FishType.Goldfish)]
        public void CreateFish_ShouldReturnFishWithCorrectProperties(FishType type)
        {
            var fish = _factory.CreateFish(type);

            Assert.AreEqual(type, fish.Type);
            Assert.AreEqual(FishName(type), fish.Name);
            Assert.AreEqual(FishPrice(type), fish.Price);
            Assert.AreEqual(FishHungerRate(type), fish.HungerRate);
            Assert.AreEqual(FishIncomeValue(type), fish.IncomeValue);
            Assert.AreEqual(100, fish.Hunger);
            Assert.IsTrue(fish.IsAlive);
        }

        [TestCase(FishType.Guppy)]
        [TestCase(FishType.SwordsMan)]
        [TestCase(FishType.Angelfish)]
        [TestCase(FishType.Goldfish)]
        public void GetFishPrice_ReturnsCorrectValue(FishType type)
        {
            Assert.AreEqual(FishPrice(type), _factory.GetFishPrice(type));
        }

        [TestCase(FishType.Guppy)]
        [TestCase(FishType.SwordsMan)]
        [TestCase(FishType.Angelfish)]
        [TestCase(FishType.Goldfish)]
        public void GetFishHungerRate_ReturnsCorrectRate(FishType type)
        {
            Assert.AreEqual(FishHungerRate(type), _factory.GetFishHungerRate(type));
        }

        [TestCase(FishType.Guppy)]
        [TestCase(FishType.SwordsMan)]
        [TestCase(FishType.Angelfish)]
        [TestCase(FishType.Goldfish)]
        public void GetFishIncomeValue_ReturnsCorrectValue(FishType type)
        {
            Assert.AreEqual(FishIncomeValue(type), _factory.GetFishIncomeValue(type));
        }

        [Test]
        public void GetAllFishTypes_ReturnsAllEnums()
        {
            var types = _factory.GetAllFishTypes();
            CollectionAssert.AreEquivalent(fishDict.Keys, types);
        }

        // Вспомогательные поля
        private static Dictionary<FishType, (decimal, double, string, decimal)> fishDict = GameConstants.FishByTypeDict;
        private static string FishName(FishType type) => fishDict[type].Item3;
        private static decimal FishPrice(FishType type) => fishDict[type].Item1;
        private static double FishHungerRate(FishType type) => fishDict[type].Item2;
        private static decimal FishIncomeValue(FishType type) => fishDict[type].Item4;
    }
}