using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Interfaces.Factories;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Factories;

public class AquariumFactory : IAquariumFactory
{
    private readonly Dictionary<AquariumType, (string, decimal, int, double, double)> AquariumDict = GameConstants.AquariumByTypeDict;

    public Aquarium CreateAquarium(AquariumType type, string? name = null)
    {
        return new Aquarium(
            name ?? GetAquariumStandartName(type),
            type,
            GetAquariumCapacity(type));
    }

    public string GetAquariumStandartName(AquariumType type)
        => AquariumDict[type].Item1;

    public decimal GetAquariumPrice(AquariumType type)
        => AquariumDict[type].Item2;

    public int GetAquariumCapacity(AquariumType type)
        => AquariumDict[type].Item3;

    public double GetAquariumWaterDirtRatePerFish(AquariumType type)
        => AquariumDict[type].Item4;

    public double GetAquariumDirtyWaterThreshold(AquariumType type)
        => AquariumDict[type].Item5;

    public AquariumType[] GetAllAquariumTypes()
    {
        return AquariumDict.Keys.ToArray();
    }
}
