using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;

namespace AquaManager.Presentation.Extensions;

public static class FishFactoryExtension
{
    public static Image GetFishImage(this FishFactory fishFactory, FishType type)
    {
        string name = type.ToString().ToLower();
        return (Image)Properties.Resources.ResourceManager.GetObject(name) ?? Properties.Resources.guppy;
    }

    public static string GetFishDescription(this FishFactory fishFactory, FishType type)
    {
        var rate = fishFactory.GetFishHungerRate(type);
        string rateDesc = rate switch
        {
            <= 0.25 => "медленно",
            <= 0.4 => "средне",
            <= 0.6 => "быстро",
            _ => "очень быстро"
        };
        return $"голодает {rateDesc} ({rate}%/сек)";
    }
}
