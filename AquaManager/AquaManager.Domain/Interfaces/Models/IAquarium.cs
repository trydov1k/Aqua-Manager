using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Models;

public interface IAquarium
{
    // Свойства

    /// <summary>
    /// Название аквариума
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Уровень чистоты воды
    /// </summary>
    double WaterCleanliness { get; }
    /// <summary>
    /// Список рыбок в аквариуме
    /// </summary>
    List<Fish> FishList { get; }
    /// <summary>
    /// Максимальное количество рыбок в аквариуме
    /// </summary>
    int Capacity { get; }


    // Методы

    /// <summary>
    /// Проверяет можно ли добавить рыбку в аквариум
    /// </summary>
    /// <returns>true если рыбку можно добавить, false если места не хватит</returns>
    bool CanAddFish();

    /// <summary>
    /// Устанавливает WaterCleanliness = 100
    /// </summary>
    void CleanWater();

    /// <summary>
    /// Добавить рыбку в аквариум, если есть место
    /// </summary>
    /// <param name="fish">Рыбка, которую нужно добавить в аквариум</param>
    /// <returns>true если рыбка добавилась, false если нет места</returns>
    bool AddFish(Fish fish);

    /// <summary>
    /// Удалить рыбку из аквариума
    /// </summary>
    /// <param name="fish">Рыбка, которую нужно убрать из аквариума</param>
    /// <returns>true если рыбка удалена, false такой рыбки в аквариуме нет</returns>
    bool RemoveFish(Fish fish);

    /// <summary>
    /// Возвращает количество живых рыбок
    /// </summary>
    int GetLiveFishCount();

    /// <summary>
    /// Уменьшает чистоту воды (WaterCleanliness) на waterDirtRate
    /// </summary>
    /// <param name="waterDirtRate">На сколько процентов уменьшить чистоту воды</param>
    void UpdateWaterCleanliness(double waterDirtRate);
}