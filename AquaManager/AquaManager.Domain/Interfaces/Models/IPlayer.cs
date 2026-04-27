using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Models;

public interface IPlayer
{
    // Свойства

    /// <summary>
    /// Количество монет
    /// </summary>
    decimal Money { get; }

    /// <summary>
    /// Список всех аквариумов
    /// </summary>
    List<Aquarium> Aquariums { get; set; }

    /// <summary>
    /// Индекс текущего выбранного аквариума (0, 1, 2, 3 ...)
    /// </summary>
    /// <example>0, 1, 2, 3, 4</example>
    int CurrentAquariumIndex { get; set; }


    // Методы

    /// <summary>
    /// Получить текущий аквариум
    /// </summary>
    /// <returns>Возвращает текущий аквариум</returns>
    Aquarium GetCurrentAquarium();

    /// <summary>
    /// Добавить деньги
    /// </summary>
    /// <param name="amount">Сколько денег добавить</param>
    void AddMoney(decimal amount);

    /// <summary>
    /// Потратить деньги
    /// </summary>
    /// <param name="amount">Сколько денег потратить</param>
    /// <returns>Если операция удалась - true, если не удалась - false</returns>
    bool SpendMoney(decimal amount);

    /// <summary>
    /// Проверить, хватает ли денег на покупку
    /// </summary>
    /// <param name="amount">Сколько денег надо</param>
    /// <returns>Если хватает - true, если не хватает - false</returns>
    bool CanAfford(decimal amount);
}