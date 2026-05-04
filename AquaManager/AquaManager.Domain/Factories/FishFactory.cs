using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces.Factories;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Factories;

/// <summary>
/// Вспомогательный класс для создания рыбок и получения их параметров (имя, цена, ...)
/// </summary>
public class FishFactory : IFishFactory
{
    private readonly Dictionary<FishType, (decimal, double, string, decimal)> FishDict = GameConstants.FishByTypeDict;

    public Fish CreateFish(FishType type)
    {
        var fishConstants = FishDict[type];
        return new Fish(
            fishConstants.Item3,
            type,
            fishConstants.Item2,
            fishConstants.Item1,
            fishConstants.Item4
        );
    }

    public decimal GetFishPrice(FishType type)
    {
        var fishConstants = FishDict[type];
        return fishConstants.Item1;
    }

    public double GetFishHungerRate(FishType type)
    {
        var fishConstants = FishDict[type];
        return fishConstants.Item2;
    }

    public string GetFishName(FishType type)
    {
        var fishConstants = FishDict[type];
        return fishConstants.Item3;
    }

    public decimal GetFishIncomeValue(FishType type)
    {
        var fishConstants = FishDict[type];
        return fishConstants.Item4;
    }

    public FishType[] GetAllFishTypes()
    {
        return FishDict.Keys.ToArray();
    }
}
