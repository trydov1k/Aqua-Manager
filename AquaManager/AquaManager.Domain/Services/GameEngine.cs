using AquaManager.Domain.Constants;
using AquaManager.Domain.Enums;
using AquaManager.Domain.Factories;
using AquaManager.Domain.Interfaces.Services;
using AquaManager.Domain.Models;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AquaManager.Domain.Services
{
    public class GameEngine : IDisposable, IGameEngineService
    {
        public Player Player { get; private set; }
        public bool IsRunning { get; private set; }

        private Timer _gameTimer;
        private IncomeService _incomeService;
        private SaveLoadService _saveLoadService;
        public FishFactory _fishFactory;        

        public event EventHandler<Player> StateChanged;

        // Конструктор
        public GameEngine()
        {
            _gameTimer = new Timer(GameConstants.GameTickIntervalMs);
            _gameTimer.Elapsed += OnGameTick;
            _saveLoadService = new SaveLoadService();
            _fishFactory = new FishFactory();
            IsRunning = false;
        }

        // Методы Start(), Stop() и NewGame()

        public void Start()
        {
            if (Player == null)
                Player = _saveLoadService.LoadGame();

            if (Player == null)
                CreateNewGame();

            _incomeService = new IncomeService(Player);
            _incomeService.IncomeGenerated += OnIncomeGenerated;

            IsRunning = true;
            _gameTimer.Start();
            _incomeService.Start();

            RaiseStateChanged();
        }

        public void Stop()
        {
            IsRunning = false;
            _gameTimer.Stop();
            _incomeService?.Stop();
        }

        public void NewGame()
        {
            CreateNewGame();
            Start();
        }

        private void CreateNewGame()
        {
            var startFish = _fishFactory.CreateFish(GameConstants.DefaultFishtype);
            var startAquarium = new Aquarium(GameConstants.DefaultAquariumName, GameConstants.DefaultAquariumCapacity);
            startAquarium.AddFish(startFish);
            var aquariums = new List<Aquarium>() { startAquarium };
            Player = new Player(GameConstants.StartingMoney, aquariums, 0);

            RaiseStateChanged();
        }

        public void LoadPlayer(Player loadedPlayer)
        {
            if (IsRunning)
                Stop();
            _incomeService?.Dispose();

            Player = loadedPlayer;
            Start();
        }


        // Логика обновления (работа с таймерами)

        private void OnGameTick(object sender, ElapsedEventArgs e)
        {
            // Тут должен быть обработчик таймера...
            if (!IsRunning)
                return;

            UpdateGameState();
        }

        private void UpdateGameState()
        {
            if (Player == null) return;

            foreach (var aquarium in Player.Aquariums)
            { 
                if (aquarium == null) return;
                var fishList = aquarium.FishList;

                aquarium.UpdateWaterCleanliness(GameConstants.WaterDirtRate);  // Уменьшаем чистоту воды на WaterDirtRate

                if (aquarium.WaterCleanliness <= 0)
                    foreach (var fish in fishList)
                        fish.Kill();

                foreach (var fish in fishList.Where(fish => fish.IsAlive))
                {
                    fish.UpdateHunger();  // Уменьшили голод
                    if (aquarium.WaterCleanliness <= GameConstants.DirtyWaterThreshold)
                        fish.UpdateHunger();  // Уменьшили голод еще раз, если аквариум слишком грязный (голод Х2)

                    if (fish.Hunger <= 0)
                        fish.Kill();
                }

                RaiseStateChanged();
            }
        }


        // Действия игрока

        public bool FeedAllFish()
        {
            var aquarium = Player.GetCurrentAquarium();
            if (aquarium == null) return false;
            var fishList = aquarium.FishList;

            var liveFishCount = aquarium.GetLiveFishCount();

            var cost = liveFishCount * GameConstants.FeedCostPerFish;

            var canFeedAllFish = liveFishCount > 0 && Player.CanAfford(cost);

            if (canFeedAllFish)
            {
                Player.SpendMoney(cost);
                foreach (var fish in fishList.Where(fish => fish.IsAlive))
                    fish.Feed();
                RaiseStateChanged();
            }

            return canFeedAllFish;
        }

        public bool FeedSingleFish(string fishId)
        {
            var aquarium = Player.GetCurrentAquarium();
            if (aquarium == null) return false;
            var fishList = aquarium.FishList;

            var feedCost = GameConstants.FeedCostPerFish;

            foreach (var fish in fishList)
            {
                if (fish.Id == fishId && fish.IsAlive && Player.CanAfford(feedCost))
                {
                    Player.SpendMoney(feedCost);
                    fish.Feed();
                    RaiseStateChanged();
                    return true;
                }
            }

            return false;
        }

        public bool ChangeWater()
        {
            var aquarium = Player.GetCurrentAquarium();
            if (aquarium == null) return false;
            var changeCost = GameConstants.WaterChangeCost;

            var canChangeWater = Player.CanAfford(changeCost);

            if (canChangeWater)
            {
                Player.SpendMoney(changeCost);
                aquarium.CleanWater();
                RaiseStateChanged();
            }

            return canChangeWater;
        }

        public bool RemoveFish(string fishId)
        {
            var aquarium = Player.GetCurrentAquarium();
            if (aquarium == null) return false;

            var fishList = aquarium.FishList;

            var fish = fishList.Where(f => f.Id == fishId).FirstOrDefault();

            if (fish == null) 
                return false;

            var result = fishList.Remove(fish);

            RaiseStateChanged();

            return result;
        }

        public bool BuyFish(FishType type, string fishName)
        {
            var aquarium = Player.GetCurrentAquarium();
            if (aquarium == null) return false;

            var fishPrice = _fishFactory.GetFishPrice(type);

            var canBuyFish = Player.CanAfford(fishPrice) && aquarium.CanAddFish() && _fishFactory.GetAllFishTypes().Contains(type);

            if (canBuyFish)
            {
                var newFish = _fishFactory.CreateFish(type);

                newFish.Name = fishName;

                aquarium.AddFish(newFish);
                Player.SpendMoney(fishPrice);
                RaiseStateChanged();
            }

            return canBuyFish;
        }

        public bool BuyAquarium(string name)
        { 
            var aquariumCost = GameConstants.NewAquariumPrice;
            var aquariumCapacity = GameConstants.DefaultAquariumCapacity;
            var aquariumName = name == "" ? $"Аквариум {Player.Aquariums.Count + 1}" : name;

            var canBuyAquarium = Player.CanAfford(aquariumCost);

            if (canBuyAquarium)
            {
                var newAquarium = new Aquarium(aquariumName, aquariumCapacity);
                Player.Aquariums.Add(newAquarium);
                Player.SpendMoney(aquariumCost);
                RaiseStateChanged();
            }

            return canBuyAquarium;
        }

        public bool CanBuyFish(FishType type)
        {
            return Player.CanAfford(_fishFactory.GetFishPrice(type));
        }

        public bool CanBuyAquarium()
        {
            return Player.CanAfford(GameConstants.NewAquariumPrice);
        }

        public bool SwitchAquarium(int index)
        {
            var isCorrectIndex = 0 <= index && index < Player.Aquariums.Count;
            if (isCorrectIndex)
            {
                Player.CurrentAquariumIndex = index;
                RaiseStateChanged();
            }

            return isCorrectIndex;
        }

        public Aquarium? GetCurrentAquarium() => Player.GetCurrentAquarium();

        //

        private void OnIncomeGenerated(object sender, IncomeEventArgs e)
        {
            RaiseStateChanged();
        }

        private void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, Player);
        }

        // Методы для сохранения игры вручную
        public void SaveGame() => _saveLoadService.SaveGame(Player);
        public void LoadGame() => LoadPlayer(_saveLoadService.LoadGame());

        // Удаление таймеров
        public void Dispose()
        {
            Stop();
            _gameTimer?.Dispose();
            _incomeService?.Dispose();
        }
    }
}
