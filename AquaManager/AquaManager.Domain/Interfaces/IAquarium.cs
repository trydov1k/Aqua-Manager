using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces
{
    public interface IAquarium
    {
        // Свойства

        /// <summary>
        /// Название аквариума
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Уровень чистоты воды
        /// </summary>
        public double WaterCleanliness { get; }
        /// <summary>
        /// Список рыбок в аквариуме
        /// </summary>
        public List<Fish> FishList { get; }
        /// <summary>
        /// Максимальное количество рыбок в аквариуме
        /// </summary>
        public int Capacity { get; }


        // Методы

        /// <summary>
        /// Устанавливает WaterCleanliness = 100
        /// </summary>
        public void CleanWater();

        /// <summary>
        /// Добавить рыбку в аквариум, если есть место
        /// </summary>
        /// <param name="fish">Рыбка, которую нужно добавить в аквариум</param>
        /// <returns>true если рыбка добавилась, false если нет места</returns>
        public bool AddFish(Fish fish);

        /// <summary>
        /// Удалить рыбку из аквариума
        /// </summary>
        /// <param name="fish">Рыбка, которую нужно убрать из аквариума</param>
        /// <returns>true если рыбка удалена, false такой рыбки в аквариуме нет</returns>
        public bool RemoveFish(Fish fish);

        /// <summary>
        /// Удаляет всех мёртвых рыбок
        /// </summary>
        public void RemoveDeadFish();

        /// <summary>
        /// Возвращает количество живых рыбок
        /// </summary>
        public int GetLiveFishCount();
    }
}