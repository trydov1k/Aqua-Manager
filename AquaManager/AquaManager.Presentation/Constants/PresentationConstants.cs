using AquaManager.Domain.Enums;
using AquaManager.Presentation.Enums;

namespace AquaManager.Presentation.Constants;

public class PresentationConstants
{
    /// <summary>
    /// Словарь для хранения направления рыбки на ее картинке
    /// </summary>
    public static readonly Dictionary<FishType, PictureDefaultDirection> DefaultFishPictureDirection = new()
    {
        { FishType.Guppy, PictureDefaultDirection.Right },
        { FishType.SwordsMan, PictureDefaultDirection.Left },
        { FishType.Angelfish, PictureDefaultDirection.Right },
        { FishType.Goldfish, PictureDefaultDirection.Right }
    };

    // Константы, связанные с отрисовкой рыбок в аквариуме

    /// <summary>
    /// Интервал таймера, отвеающего за отрисовку рыбок в аквариуме
    /// </summary>
    public const int AnimationTimerIntervalMs = 30;

    /// <summary>
    /// Размер уменьшенной картинки рыбки (ширина)
    /// </summary>
    public const int StandartFishImageWidth = 60;

    /// <summary>
    /// Размер уменьшенной картинки рыбки (длина)
    /// </summary>
    public const int StandartFishImageHeight = 60;

    /// <summary>
    /// Минимальная скорость движения рыбки в аквариуме
    /// </summary>
    public const float SwimmingFishVelocityMin = -2.5f;

    /// <summary>
    /// Максимальная скорость движения рыбки в аквариуме
    /// </summary>
    public const float SwimmingFishVelocityMax = 2.5f;

    // Константы форм

    /// <summary>
    /// Максимальное количество символов для названия имени (рыбки/аквариума/сохранения)
    /// </summary>
    public const int MaxInputNameLenght = 15;
}
