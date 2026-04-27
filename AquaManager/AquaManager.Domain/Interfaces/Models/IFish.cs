using AquaManager.Domain.Enums;

namespace AquaManager.Domain.Interfaces.Models;

public interface IFish
{
    // Свойства:

    /// <summary>
    /// Имя рыбки
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Тип рыбки
    /// </summary>
    FishType Type { get; }

    /// <summary>
    /// Текущий голод рыбки
    /// </summary>
    double Hunger { get; }

    /// <summary>
    /// Коэффициент голода рыбки (сколько единиц голода уходит в секунду)
    /// </summary>
    double HungerRate { get; }

    /// <summary>
    /// Стоимость рыбки
    /// </summary>
    decimal Price { get; }

    /// <summary>
    /// Рыбка жива
    /// </summary>
    bool IsAlive { get; }

    // Лямбда-свойства:

    /// <summary>
    /// Рыбка мертва
    /// </summary>
    bool IsDie { get; }

    // Методы:

    /// <summary>
    /// Восстановить голод рыбки на 100%
    /// </summary>
    void Feed();

    /// <summary>
    /// уменьшает голод на HungerRate
    /// </summary>
    void UpdateHunger();

    /// <summary>
    /// Сделать рыбку мертвой
    /// </summary>
    void Kill();
}