using System.Text.Json;
using System.Text.Json.Serialization;
using AquaManager.Domain.Constants;
using AquaManager.Domain.Interfaces.Services;
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Services
{
    /// <summary>
    /// Сервис для сохранения и загрузки состояния игры в JSON-файл
    /// </summary>
    public class SaveLoadService : ISaveLoadService
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public event EventHandler<Player> GameLoaded;

        public event EventHandler GameSaved;

        public event EventHandler<string> ErrorOccurred;

        private readonly string _folderToSaveName;
        private readonly string _systemGameSaveName;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="saveFileName">Имя файла сохранения (по умолчанию "savegame.json")</param>
        public SaveLoadService()
        {
            // Настройки сериализации
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };

            _folderToSaveName = SaveLoadConstants.DefaultFolderToSavesName;
            _systemGameSaveName = SaveLoadConstants.DefaultSystemGameSaveFileName;
        }

        public bool SaveGame(SaveSlotInfo saveInfo)
        {
            var saveFilePath = GiveFilePath(saveInfo.SlotName);
            var player = saveInfo.Player;

            if (player == null)
            {
                OnErrorOccurred("Нельзя сохранить: игрок не существует");
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(saveInfo, _jsonOptions);
                File.WriteAllText(saveFilePath, jsonString);
                OnGameSaved();
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveGameAsync(SaveSlotInfo saveInfo)
        {
            var saveFilePath = GiveFilePath(saveInfo.SlotName);
            var player = saveInfo.Player;

            if (player == null)
            {
                OnErrorOccurred("Нельзя сохранить: игрок не существует");
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(saveInfo, _jsonOptions);
                await File.WriteAllTextAsync(saveFilePath, jsonString);
                OnGameSaved();
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }

        public SaveSlotInfo LoadGame(string slotName)
        {
            var saveFilePath = GiveFilePath(slotName);

            if (!File.Exists(saveFilePath))
            {
                OnErrorOccurred("Файл сохранения не найден. Будет начата новая игра.");
                return null;
            }

            try
            {
                string jsonString = File.ReadAllText(saveFilePath);
                SaveSlotInfo saveInfo = JsonSerializer.Deserialize<SaveSlotInfo>(jsonString, _jsonOptions);

                var player = saveInfo.Player;

                if (player == null)
                {
                    OnErrorOccurred("Файл сохранения повреждён");
                    return null;
                }

                // Восстановление связей и валидация загруженных данных
                ValidateAndRepairPlayer(player);

                OnGameLoaded(player);
                return saveInfo;
            }
            catch (JsonException ex)
            {
                OnErrorOccurred($"Ошибка разбора JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка загрузки: {ex.Message}");
                return null;
            }
        }

        public async Task<SaveSlotInfo> LoadGameAsync(string slotName)
        {
            var saveFilePath = GiveFilePath(slotName);

            if (!File.Exists(saveFilePath))
            {
                OnErrorOccurred("Файл сохранения не найден. Будет начата новая игра.");
                return null;
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(saveFilePath);
                SaveSlotInfo saveInfo = JsonSerializer.Deserialize<SaveSlotInfo>(jsonString, _jsonOptions);

                Player player = saveInfo.Player;

                if (player == null)
                {
                    OnErrorOccurred("Файл сохранения повреждён");
                    return null;
                }

                ValidateAndRepairPlayer(player);
                OnGameLoaded(player);
                return saveInfo;
            }
            catch (JsonException ex)
            {
                OnErrorOccurred($"Ошибка разбора JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка загрузки: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Проверяет загруженные данные на целостность и восстанавливает критические поля
        /// </summary>
        private void ValidateAndRepairPlayer(Player player)
        {
            if (player == null) return;

            // Если нет аквариумов — создать один по умолчанию
            if (player.Aquariums == null || player.Aquariums.Count == 0)
            {
                player.Aquariums = new List<Aquarium>
                {
                    new Aquarium(GameConstants.DefaultAquariumName, GameConstants.DefaultAquariumCapacity)
                };
                player.CurrentAquariumIndex = 0;
            }

            // Проверить, что CurrentAquariumIndex корректен
            if (player.CurrentAquariumIndex < 0 || player.CurrentAquariumIndex >= player.Aquariums.Count)
                player.CurrentAquariumIndex = 0;



            // Для каждой рыбки: убедиться, что Id не пустой (для совместимости со старыми сохранениями)
            foreach (var aquarium in player.Aquariums)
            {
                foreach (var fish in aquarium.FishList)
                {
                    if (string.IsNullOrEmpty(fish.Id))
                        fish.Id = Guid.NewGuid().ToString();
                }
            }
        }

        public bool SaveFileExists(string fileName) => File.Exists(GiveFilePath(fileName));

        public bool DeleteSaveFile(string fileName)
        {
            try
            {
                var saveFilePath = GiveFilePath(fileName);
                if (File.Exists(saveFilePath))
                    File.Delete(saveFilePath);
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Не удалось удалить сохранение: {ex.Message}");
                return false;
            }
        }

        private string GiveFilePath(string fileName)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var path = Path.Combine(appDataPath, "AquaManager", _folderToSaveName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileNameWithExt = fileName + ".json";
            return Path.Combine(path, fileNameWithExt);
        }

        public IEnumerable<string> GiveAllSaveFileNames()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(appDataPath, "AquaManager", _folderToSaveName);

            if (!Directory.Exists(path))
                return new string[0];

            var files = Directory.GetFiles(path, "*.json");

            var names = files.Select(f => f.Remove(0, f.LastIndexOf("\\") + 1)).Select(f => f.Remove(f.LastIndexOf(".")));

            return names.Where(f => f != _systemGameSaveName);
        }

        // Приватные методы вызова событий
        private void OnGameLoaded(Player player) => GameLoaded?.Invoke(this, player);
        private void OnGameSaved() => GameSaved?.Invoke(this, EventArgs.Empty);
        private void OnErrorOccurred(string message) => ErrorOccurred?.Invoke(this, message);
    }
}