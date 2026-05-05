using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Tests.DomainTests.ModelsTests;

[TestFixture]
public class PlayerTests
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        var aquariums = new List<Aquarium> { new Aquarium("A1", AquariumType.Default, 5) };
        var player = new Player(200, aquariums, 0);
        Assert.AreEqual(200, player.Money);
        Assert.AreEqual(1, player.Aquariums.Count);
        Assert.AreEqual(aquariums, player.Aquariums);
        Assert.AreEqual(0, player.CurrentAquariumIndex);
    }

    [Test]
    public void AddMoney_ShouldIncreaseBalance()
    {
        var player = new Player(100, new List<Aquarium>(), 0);
        player.AddMoney(50);
        Assert.AreEqual(150, player.Money);
    }

    [Test]
    public void CanAfford_WhenEnoughMoney_ReturnsTrue()
    {
        var player = new Player(100, new List<Aquarium>(), 0);
        Assert.IsTrue(player.CanAfford(50));
        Assert.IsFalse(player.CanAfford(150));
    }

    [Test]
    public void SpendMoney_WhenEnough_ReturnsTrueAndDecreasesMoney()
    {
        var player = new Player(100, new List<Aquarium>(), 0);
        var result = player.SpendMoney(30);
        Assert.IsTrue(result);
        Assert.AreEqual(70, player.Money);
    }

    [Test]
    public void SpendMoney_WhenNotEnough_ReturnsFalseAndMoneyUnchanged()
    {
        var player = new Player(100, new List<Aquarium>(), 0);
        var result = player.SpendMoney(150);
        Assert.IsFalse(result);
        Assert.AreEqual(100, player.Money);
    }

    [Test]
    public void GetCurrentAquarium_ShouldReturnCorrectAquarium()
    {
        var aquariums = new List<Aquarium>
        {
            new Aquarium("A1",AquariumType.Default, 5),
            new Aquarium("A2",AquariumType.Default, 6)
        };
        var player = new Player(100, aquariums, 1);
        var current = player.GetCurrentAquarium();
        Assert.AreEqual(aquariums[1], current);
    }

    [Test]
    public void GetCurrentAquarium_WhenNoAquariums_ReturnsNull()
    {
        var player = new Player(100, new List<Aquarium>(), 0);
        Assert.IsNull(player.GetCurrentAquarium());
    }
}