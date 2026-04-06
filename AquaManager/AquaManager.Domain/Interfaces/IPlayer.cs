using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces
{
    public interface IPlayer
    {
        // Свойства

        /// <summary>
        /// Количество монет
        /// </summary>
        public int Money { get; }

        /// <summary>
        /// Список всех аквариумов
        /// </summary>
        public List<Aquarium> Aquariums { get; set; }

        /// <summary>
        /// Индекс текущего выбранного аквариума (0, 1, 2, 3 ...)
        /// </summary>
        /// <example>0, 1, 2, 3, 4</example>
        public int CurrentAquariumIndex { get; set; }


        // Методы

        /// <summary>
        /// Получить текущий аквариум
        /// </summary>
        /// <returns>Возвращает текущий аквариум</returns>
        public Aquarium GetCurrentAquarium();

        /// <summary>
        /// Добавить деньги
        /// </summary>
        /// <param name="amount">Сколько денег добавить</param>
        public void AddMoney(int amount);

        /// <summary>
        /// Потратить деньги
        /// </summary>
        /// <param name="amount">Сколько денег потратить</param>
        /// <returns>Если операция удалась - true, если не удалась - false</returns>
        public bool SpendMoney(int amount);

        /// <summary>
        /// Проверить, хватает ли денег на покупку
        /// </summary>
        /// <param name="amount">Сколько денег надо</param>
        /// <returns>Если хватает - true, если не хватает - false</returns>
        public bool CanAfford(int amount);
    }
}