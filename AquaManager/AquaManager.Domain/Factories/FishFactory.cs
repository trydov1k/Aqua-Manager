using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Factories
{
    public class FishFactory
    {
        private readonly Dictionary<FishType, (decimal, double, string)> FishDict = GameConstants.FishDictByType;

        public Fish CreateFish(FishType type)
        {
            var fishConstants = FishDict[type];
            return new Fish(
                fishConstants.Item3,
                type,
                fishConstants.Item2,
                fishConstants.Item1
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

        public FishType[] GetAllFishTypes()
        {
            return FishDict.Keys.ToArray();
        }
    }
}
