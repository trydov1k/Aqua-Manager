
using AquaManager.Domain.Enums;

namespace AquaManager.Domain.Constants;

public static class GameConstants
{
    // Экономика 

    /// <summary>
    /// Начальное количество монет у игрока
    /// </summary>
    public const decimal StartingMoney = 150;

    /// <summary>
    /// Интервал пассивного дохода в секундах
    /// </summary>
    public const int IncomeIntervalSeconds = 20;


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
    /// Словарь с характеристиками аквариумов в зависимости от их вида
    /// Вид аквариума: (имя, стоимость, вместительность, коэффициент загрязнения в секунду, порог после которго рыбы быстрее голодают)
    /// </summary>
    public static readonly Dictionary<AquariumType, (string, decimal, int, double, double)> AquariumByTypeDict = new()
    {
        { AquariumType.Default, ("Стандартный аквариум", 500, 6, 0.07, 20) },
        { AquariumType.Ocean, ("Океан", 600, 6, 0.05, 20) }
    };

    // Параметры рыб

    /// <summary>
    /// Словарь с характеристиками рыбок в зависимости от их вида
    /// Вид рыбки: (стоимость, коэффициент голода, имя, доход от рыбки)
    /// </summary>
    public static readonly Dictionary<FishType, (decimal, double, string, decimal)> FishByTypeDict = new()
    {
        { FishType.Guppy, (40, 0.2, "Гуппи", 5) },
        { FishType.SwordsMan, (70, 0.33, "Меченосец", 8) },
        { FishType.Angelfish, (100, 0.5, "Скалярия", 12) },
        { FishType.Goldfish, (150, 0.67, "Золотая рыбка", 20) }
    };

    // Таймеры и обновления

    /// <summary>
    /// Интервал основного таймера в миллисекундах (1 секунда)
    /// </summary>
    public const int GameTickIntervalMs = 1000;

    // другое

    /// <summary>
    /// Тип рыбки, которая будет создана при новой игре
    /// </summary>
    public const FishType DefaultFishtype = FishType.Guppy;
}
