using AquaManager.Domain.Enums;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Factories;

public interface IFishFactory
{
    /// <summary>
    /// Создает новую рыбку, по переданному типу рыбки
    /// </summary>
    /// <param name="type">Тип создаваемой рыбки</param>
    /// <returns>Возвращает новую рыбку типа type</returns>
    Fish CreateFish(FishType type);
    /// <summary>
    /// Метод для получения стоимости рыбки по ее типу
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>Возвращает стоимость рыбки</returns>
    decimal GetFishPrice(FishType type);
    /// <summary>
    /// Метод для получения имени рыбки по ее типу
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>Возвращает имя рыбки</returns>
    string GetFishName(FishType type);
    /// <summary>
    /// Метод для получения коэффициента голодания рыбки по ее типу
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>Возвращает коэффициента голодания рыбки</returns>
    double GetFishHungerRate(FishType type);
    /// <summary>
    /// Метод для получения всех типов рыбок
    /// </summary>
    /// <returns>Возвращает все типы робок</returns>
    FishType[] GetAllFishTypes();

    /// <summary>
    /// Метод для получения значения дохода от рыбки
    /// </summary>
    /// <param name="type">Тип рыбки</param>
    /// <returns>Возвращает значение дохода от рыбки</returns>
    decimal GetFishIncomeValue(FishType type);
}
