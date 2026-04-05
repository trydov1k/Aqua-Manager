using AquaManager.Domain.Enums;

namespace AquaManager.Domain.Interfaces
{
    public interface IFish
    {
        // Свойства:

        /// <summary>
        /// Имя рыбки
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Тип рыбки
        /// </summary>
        public FishType Type { get; }

        /// <summary>
        /// Текущий голод рыбки
        /// </summary>
        public int Hunger { get; }

        /// <summary>
        /// Коэффициент голода рыбки (насколько быстро голодает)
        /// </summary>
        public double HungerRate { get; }

        /// <summary>
        /// Стоимость рыбки
        /// </summary>
        public double Price { get; }

        /// <summary>
        /// Рыбка жива
        /// </summary>
        public bool IsAlive { get; }

        // Лямбда-свойства:

        /// <summary>
        /// Рыбка мертва
        /// </summary>
        public bool IsDie { get; }

        // Методы:

        /// <summary>
        /// Восстановить голод рыбки на 100%
        /// </summary>
        public void Feed();

        /// <summary>
        /// уменьшает голод на HungerRate
        /// </summary>
        public void UpdateHunger();

        /// <summary>
        /// Сделать рыбку мертвой
        /// </summary>
        public void Die();
    }
}