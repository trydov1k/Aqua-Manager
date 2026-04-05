namespace AquaManager.Domain.Interfaces
{
    public interface IGameState
    {
        /// <summary>
        /// Активен ли режим точечного кормления
        /// </summary>
        public bool IsFeedingMode { get; set; }

        /// <summary>
        /// Таймер, обновляющий состояние игры каждую секунду
        /// </summary>
        public Timer GameTimer { get; set; }

        /// <summary>
        /// Таймер для пассивного дохода
        /// </summary>
        public Timer IncomeTimer { get; set; }
    }
}