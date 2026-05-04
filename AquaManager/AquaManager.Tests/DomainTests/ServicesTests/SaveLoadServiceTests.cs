using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;
using AquaManager.Domain.Services;

namespace AquaManager.Tests.DomainTests.ServicesTests;

[TestFixture]
public class SaveLoadServiceTests
{
    private string _directory;
    private string _tempFileName;
    private string _tempFilePath;
    private SaveLoadService _service;
    private Player _originalPlayer;
    private SaveSlotInfo _saveSlot;
    

    [SetUp]
    public void SetUp()
    {
        _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AquaManager", SaveLoadConstants.DefaultFolderToSavesName);
        _tempFileName = "SaveForTests";
        _tempFilePath = Path.Combine(_directory, _tempFileName + SaveLoadConstants.DefaultSaveFileExtension);
        _service = new SaveLoadService();
        _originalPlayer = new Player(500, new List<Aquarium>(), 0);
        var aqua = new Aquarium("TestAqua", 3);
        var fish = CreateStandartFish();
        aqua.AddFish(fish);
        _originalPlayer.Aquariums.Add(aqua);
        
        _saveSlot = new SaveSlotInfo(_tempFileName, _originalPlayer);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_tempFilePath))
            File.Delete(_tempFilePath);
    }

    [Test]
    public void SaveGame_ShouldCreateFile()
    {
        _service.SaveGame(_saveSlot);
        Assert.IsTrue(File.Exists(_tempFilePath));

        Assert.IsTrue(new FileInfo(_tempFilePath).Length > 0);
    }

    [Test]
    public void LoadGame_ShouldRestorePlayerCorrectly()
    {
        _service.SaveGame(_saveSlot);
        var loaded = _service.LoadGame(_tempFileName).Player;
        Assert.AreEqual(_originalPlayer.Money, loaded.Money);
        Assert.AreEqual(_originalPlayer.Aquariums.Count, loaded.Aquariums.Count);
        Assert.AreEqual(_originalPlayer.Aquariums[0].Name, loaded.Aquariums[0].Name);
        Assert.AreEqual(_originalPlayer.Aquariums[0].FishList[0].Name, loaded.Aquariums[0].FishList[0].Name);
        Assert.AreEqual(_originalPlayer.Aquariums[0].FishList[0].Hunger, loaded.Aquariums[0].FishList[0].Hunger);
    }

    [Test]
    public void LoadGame_WhenFileNotExists_ReturnsNullAndRaisesError()
    {
        if (File.Exists(_tempFilePath)) 
            File.Delete(_tempFilePath);
        bool errorRaised = false;
        _service.ErrorOccurred += (s, msg) => errorRaised = true;
        var loaded = _service.LoadGame(_tempFileName);
        Assert.IsNull(loaded);
        Assert.IsTrue(errorRaised);
    }

    [Test]
    public void SaveFileExists_ReturnsCorrectState()
    {
        if (File.Exists(_tempFilePath)) 
            File.Delete(_tempFilePath);
        Assert.IsFalse(_service.SaveFileExists(_tempFileName));
        _service.SaveGame(_saveSlot);
        Assert.IsTrue(_service.SaveFileExists(_tempFileName));
    }

    [Test]
    public void DeleteSaveFile_RemovesFile()
    {
        _service.SaveGame(_saveSlot);
        Assert.IsTrue(File.Exists(_tempFilePath));
        _service.DeleteSaveFile(_tempFileName);
        Assert.IsFalse(File.Exists(_tempFilePath));
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