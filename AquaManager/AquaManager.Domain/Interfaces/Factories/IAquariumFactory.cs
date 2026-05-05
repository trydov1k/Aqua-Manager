using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Factories;

public interface IAquariumFactory
{
    /// <summary>
    /// Метод для создания аквариума по его типу и имени (если имя не указано, создается со стандартным именем)
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <param name="name"></param>
    /// <returns>Возвращает созданный аквариум</returns>
    Aquarium CreateAquarium(AquariumType type, string? name = null);

    /// <summary>
    /// Метод для получения стандартного имени аквариума
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <returns>Возвращает стандартное имя аквариума</returns>
    string GetAquariumStandartName(AquariumType type);

    /// <summary>
    /// Метод для получения стоимости аквариума
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <returns>Возвращает стоимость аквариума</returns>
    decimal GetAquariumPrice(AquariumType type);

    /// <summary>
    /// Метод для получения вместимости аквариума
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <returns>Возвращает вместимость аквариума</returns>
    int GetAquariumCapacity(AquariumType type);

    /// <summary>
    /// Метод для получения коэффициента загрязнения аквариума
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <returns>Возвращает коэффициент загрязнения аквариума</returns>
    double GetAquariumWaterDirtRatePerFish(AquariumType type);

    /// <summary>
    /// Метод для получения значения загрязненности аквариума, 
    /// после которого все рыбы голодают в два раза быстрее
    /// </summary>
    /// <param name="type">Тип аквариума</param>
    /// <returns>Возвращает значение порога загрязненности</returns>
    double GetAquariumDirtyWaterThreshold(AquariumType type);

    /// <summary>
    /// Метод для получения всех типов аквариумов
    /// </summary>
    /// <returns>Возвращает все типы аквариумов</returns>
    AquariumType[] GetAllAquariumTypes();
}
