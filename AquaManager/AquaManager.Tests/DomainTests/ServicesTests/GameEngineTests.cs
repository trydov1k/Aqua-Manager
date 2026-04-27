using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace AquaManager.Tests.DomainTests.ServicesTests;

[TestFixture]
public class GameEngineTests
{
    private GameEngine _engine;

    [SetUp]
    public void SetUp()
    {
        _engine = new GameEngine();
        _engine.NewGame();
    }

    [TearDown]
    public void TearDown()
    {
        _engine.Dispose();
    }

    [Test]
    public void NewGame_ShouldCreatePlayerWithOneAquariumAndOneFish()
    {
        Assert.AreEqual(GameConstants.StartingMoney, _engine.Player.Money);
        Assert.AreEqual(1, _engine.Player.Aquariums.Count);
        Assert.AreEqual(1, _engine.Player.GetCurrentAquarium().FishList.Count);
        Assert.IsTrue(_engine.Player.GetCurrentAquarium().FishList[0].IsAlive);
    }

    [Test]
    public void FeedAllFish_ShouldFeedAllLiveFishAndDecreaseMoney()
    {
        var aquarium = _engine.Player.GetCurrentAquarium();
        var fish = aquarium.FishList[0];
        fish.UpdateHunger(); // уменьшим голод
        var oldHunger = fish.Hunger;
        var oldMoney = _engine.Player.Money;
        int liveCount = aquarium.GetLiveFishCount();
        var expectedCost = liveCount * 5;

        var result = _engine.FeedAllFish();

        Assert.IsTrue(result);
        Assert.AreEqual(100, fish.Hunger);
        Assert.AreEqual(oldMoney - expectedCost, _engine.Player.Money);
    }

    [Test]
    public void FeedSingleFish_ShouldFeedSpecificFishAndDecreaseMoney()
    {
        // добавим вторую рыбку
        var factory = new FishFactory();
        var newFish = factory.CreateFish(FishType.Goldfish);
        _engine.Player.GetCurrentAquarium().AddFish(newFish);
        string fishId = newFish.Id;
        newFish.UpdateHunger(); // уменьшим голод
        var oldHunger = newFish.Hunger;
        var oldMoney = _engine.Player.Money;

        var result = _engine.FeedSingleFish(fishId);
        Assert.IsTrue(result);
        Assert.AreEqual(100, newFish.Hunger);
        Assert.AreEqual(oldMoney - 5, _engine.Player.Money);
    }

    [Test]
    public void ChangeWater_ShouldCleansWaterAndDecreaseMoney()
    {
        var aquarium = _engine.Player.GetCurrentAquarium();
        aquarium.UpdateWaterCleanliness(30);
        var oldMoney = _engine.Player.Money;
        var result = _engine.ChangeWater();
        Assert.IsTrue(result);
        Assert.AreEqual(100, aquarium.WaterCleanliness);
        Assert.AreEqual(oldMoney - GameConstants.WaterChangeCost, _engine.Player.Money);
    }

    [Test]
    public void BuyFish_WhenHaveSpaceAndMoney_AddsFish()
    {
        var oldMoney = _engine.Player.Money;
        var aquarium = _engine.Player.GetCurrentAquarium();
        int oldCount = aquarium.FishList.Count;
        var result = _engine.BuyFish(FishType.Goldfish);
        Assert.IsTrue(result);
        Assert.AreEqual(oldCount + 1, aquarium.FishList.Count);
        Assert.AreEqual(oldMoney - 150, _engine.Player.Money);
    }

    [Test]
    public void BuyFish_WhenNoSpace_ReturnsFalse()
    {
        var aquarium = _engine.Player.GetCurrentAquarium();

        var factory = new FishFactory();
        // заполним до предела (вместимость 6)
        for (int i = 0; i < 5; i++) // уже 1 есть, добавим 5
            aquarium.AddFish(factory.CreateFish(FishType.Goldfish));
        var result = _engine.BuyFish(FishType.Goldfish);
        Assert.IsFalse(result);
    }

    [Test]
    public void BuyAquarium_AddsNewAquariumToPlayer()
    {
        _engine.Player.AddMoney(GameConstants.NewAquariumPrice - GameConstants.StartingMoney);
        int oldCount = _engine.Player.Aquariums.Count;
        var oldMoney = _engine.Player.Money;
        var result = _engine.BuyAquarium("Новый дом");
        Assert.IsTrue(result);
        Assert.AreEqual(oldCount + 1, _engine.Player.Aquariums.Count);
        Assert.AreEqual("Новый дом", _engine.Player.Aquariums.Last().Name);
        Assert.AreEqual(oldMoney - GameConstants.NewAquariumPrice, _engine.Player.Money);
    }

    [Test]
    public void RemoveDeadFish_RemovesOnlyDead()
    {
        var factory = new FishFactory();

        var aquarium = _engine.Player.GetCurrentAquarium();
        var deadFish = factory.CreateFish(FishType.Goldfish);
        deadFish.Kill();
        aquarium.AddFish(deadFish);
        _engine.RemoveDeadFish();
        Assert.AreEqual(1, aquarium.FishList.Count); // только изначальная живая
    }

    [Test]
    public void SwitchAquarium_ChangesCurrentIndex()
    {
        _engine.Player.AddMoney(GameConstants.NewAquariumPrice - GameConstants.StartingMoney);
        _engine.BuyAquarium("Second");
        Assert.AreEqual(0, _engine.Player.CurrentAquariumIndex);
        _engine.SwitchAquarium(1);
        Assert.AreEqual(1, _engine.Player.CurrentAquariumIndex);
        Assert.AreEqual(_engine.GetCurrentAquarium()?.Name, "Second");
    }

    [Test]
    public void UpdateGameState_ReducesHungerAndCleanliness()
    {
        var aquarium = _engine.Player.GetCurrentAquarium();
        var fish = aquarium.FishList[0];
        double oldHunger = fish.Hunger;
        double oldClean = aquarium.WaterCleanliness;

        var method = typeof(GameEngine).GetMethod("UpdateGameState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method.Invoke(_engine, null);

        Assert.Less(fish.Hunger, oldHunger);
        Assert.Less(aquarium.WaterCleanliness, oldClean);
    }
}