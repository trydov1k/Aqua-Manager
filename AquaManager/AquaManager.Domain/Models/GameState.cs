using AquaManager.Domain.Interfaces;

namespace AquaManager.Domain.Models
{
    public class GameState : IGameState
    {
        public bool IsFeedingMode { get; set; }
        public Timer GameTimer { get; set; }
        public Timer IncomeTimer { get; set; }
    }
}