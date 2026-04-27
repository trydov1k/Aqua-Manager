using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;

namespace AquaManager.Tests.DomainTests.ServicesTests;

[TestFixture]
public class SaveLoadServiceTests
{
    private string _tempFile;
    private SaveLoadService _service;
    private Player _originalPlayer;

    [SetUp]
    public void SetUp()
    {
        _tempFile = Path.GetTempFileName();
        _service = new SaveLoadService(_tempFile);
        _originalPlayer = new Player(500, new List<Aquarium>(), 0);
        var aqua = new Aquarium("TestAqua", 3);
        var fish = CreateStandartFish();
        aqua.AddFish(fish);
        _originalPlayer.Aquariums.Add(aqua);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_tempFile)) File.Delete(_tempFile);
    }

    [Test]
    public void SaveGame_ShouldCreateFile()
    {
        _service.SaveGame(_originalPlayer);
        Assert.IsTrue(File.Exists(_tempFile));
        Assert.IsTrue(new FileInfo(_tempFile).Length > 0);
    }

    [Test]
    public void LoadGame_ShouldRestorePlayerCorrectly()
    {
        _service.SaveGame(_originalPlayer);
        var loaded = _service.LoadGame();
        Assert.AreEqual(_originalPlayer.Money, loaded.Money);
        Assert.AreEqual(_originalPlayer.Aquariums.Count, loaded.Aquariums.Count);
        Assert.AreEqual(_originalPlayer.Aquariums[0].Name, loaded.Aquariums[0].Name);
        Assert.AreEqual(_originalPlayer.Aquariums[0].FishList[0].Name, loaded.Aquariums[0].FishList[0].Name);
        Assert.AreEqual(_originalPlayer.Aquariums[0].FishList[0].Hunger, loaded.Aquariums[0].FishList[0].Hunger);
    }

    [Test]
    public void LoadGame_WhenFileNotExists_ReturnsNullAndRaisesError()
    {
        if (File.Exists(_tempFile)) File.Delete(_tempFile);
        bool errorRaised = false;
        _service.ErrorOccurred += (s, msg) => errorRaised = true;
        var loaded = _service.LoadGame();
        Assert.IsNull(loaded);
        Assert.IsTrue(errorRaised);
    }

    [Test]
    public void SaveFileExists_ReturnsCorrectState()
    {
        if (File.Exists(_tempFile)) File.Delete(_tempFile);
        Assert.IsFalse(_service.SaveFileExists());
        _service.SaveGame(_originalPlayer);
        Assert.IsTrue(_service.SaveFileExists());
    }

    [Test]
    public void DeleteSaveFile_RemovesFile()
    {
        _service.SaveGame(_originalPlayer);
        Assert.IsTrue(File.Exists(_tempFile));
        _service.DeleteSaveFile();
        Assert.IsFalse(File.Exists(_tempFile));
    }

    #region Вспомогательные методы
    private static (decimal, double, string, decimal) GuppyConstants = GameConstants.FishByTypeDict[FishType.Guppy];
    private static string FishName = GuppyConstants.Item3;
    private static decimal FishPrice = GuppyConstants.Item1;
    private static double FishHungerRate = GuppyConstants.Item2;
    private static decimal FishIncomeValue = GuppyConstants.Item4;
    public Fish CreateStandartFish() => new Fish(FishName, FishType.Guppy, FishHungerRate, FishPrice, FishIncomeValue);
    
    #endregion
}