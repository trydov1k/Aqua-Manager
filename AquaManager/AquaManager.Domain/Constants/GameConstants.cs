
using AquaManager.Domain.Enums;

namespace AquaManager.Domain.Constants;

public static class GameConstants
{
    // Экономика 

    /// <summary>
    /// Начальное количество монет у игрока
    /// </summary>
    public const decimal StartingMoney = 200;

    /// <summary>
    /// Монеты, начисляемые за каждую живую рыбку (за один тик таймера)
    /// </summary>
    public const decimal IncomePerLiveFish = 5;

    /// <summary>
    /// Интервал пассивного дохода в секундах
    /// </summary>
    public const int IncomeIntervalSeconds = 30;


    // Стоимость действий

    /// <summary>
    /// Стоимость кормления одной рыбки
    /// </summary>
    public const decimal FeedCostPerFish = 5;

    /// <summary>
    /// Стоимость полной смены воды в аквариуме
    /// </summary>
    public const decimal WaterChangeCost = 30;


    // Параметры аквариума

    /// <summary>
    /// Название первого аквариума
    /// </summary>
    public const string DefaultAquariumName = "Мой первый аквариум";

    /// <summary>
    /// Максимальное количество рыбок в одном аквариуме
    /// </summary>
    public const int DefaultAquariumCapacity = 6;

    /// <summary>
    /// Цена покупки дополнительного аквариума
    /// </summary>
    public const decimal NewAquariumPrice = 500;

    /// <summary>
    /// Уменьшение чистоты воды в процентах за секунду
    /// </summary>
    public const double WaterDirtRate = 0.3;

    /// <summary>
    /// Уровень чистоты (%), ниже которого рыбки голодают в 2 раза быстрее
    /// </summary>
    public const double DirtyWaterThreshold = 20;


    // Характеристики видов рыб

    /// <summary>
    /// Словарь с характеристиками рыбок в зависимости от их вида
    /// Вид рыбки: (стоимость, коэффициент голода)
    /// </summary>
    public static readonly Dictionary<FishType, (decimal, double, string)> FishByTypeDict = new()
    {
        { FishType.Guppy, (40, 0.2, "Гуппи") },
        { FishType.SwordsMan, (70, 0.33, "Меченосец") },
        { FishType.Angelfish, (100, 0.5, "Скалярия") },
        { FishType.Goldfish, (150, 0.67, "Золотая рыбка") }
    };

    // Таймеры и обновления

    /// <summary>
    /// Интервал основного таймера в миллисекундах (1 секунда)
    /// </summary>
    public const int GameTickIntervalMs = 1000;

    //

    /// <summary>
    /// Тип рыбки, которая будет создана при новой игре
    /// </summary>
    public const FishType DefaultFishtype = FishType.Guppy;
}
