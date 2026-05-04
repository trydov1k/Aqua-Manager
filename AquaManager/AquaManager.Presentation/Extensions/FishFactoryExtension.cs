using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Presentation.Constants;
using AquaManager.Presentation.Enums;

namespace AquaManager.Presentation.Extensions;

public static class FishFactoryExtension
{
    private static readonly Dictionary<FishType, PictureDefaultDirection> TypeToDirection = PresentationConstants.DefaultFishPictureDirection;

    public static Image GetFishImage(this FishFactory fishFactory, FishType type)
    {
        string name = type.ToString().ToLower();
        return (Image)(Properties.Resources.ResourceManager.GetObject(name) ?? Properties.Resources.guppy);
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

    /// <summary>
    /// Метод, определющий в какую сторону смотрит рыбка в исходном положении (влево или вправо)
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>true если рыбка смотрит вправо, иначе false</returns>
    public static bool IsDefaultRight(this FishFactory fishFactory, FishType type)
    {
        return TypeToDirection[type] == PictureDefaultDirection.Right;
    }
}
