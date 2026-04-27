using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
        private readonly string _saveFilePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public event EventHandler<Player> GameLoaded;

        public event EventHandler GameSaved;

        public event EventHandler<string> ErrorOccurred;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="saveFileName">Имя файла сохранения (по умолчанию "savegame.json")</param>
        public SaveLoadService(string saveFileName = "savegame.json")
        {
            // Путь к файлу сохранения в папке приложения
            _saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveFileName);

            // Настройки сериализации
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,           // Красивое форматирование
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // camelCase для JSON
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public bool SaveGame(Player player)
        {
            if (player == null)
            {
                OnErrorOccurred("Нельзя сохранить: игрок не существует");
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(player, _jsonOptions);
                File.WriteAllText(_saveFilePath, jsonString);
                OnGameSaved();
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveGameAsync(Player player)
        {
            if (player == null)
            {
                OnErrorOccurred("Нельзя сохранить: игрок не существует");
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(player, _jsonOptions);
                await File.WriteAllTextAsync(_saveFilePath, jsonString);
                OnGameSaved();
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }

        public Player LoadGame()
        {
            if (!File.Exists(_saveFilePath))
            {
                OnErrorOccurred("Файл сохранения не найден. Будет начата новая игра.");
                return null;
            }

            try
            {
                string jsonString = File.ReadAllText(_saveFilePath);
                Player player = JsonSerializer.Deserialize<Player>(jsonString, _jsonOptions);

                if (player == null)
                {
                    OnErrorOccurred("Файл сохранения повреждён");
                    return null;
                }

                // Восстановление связей и валидация загруженных данных
                ValidateAndRepairPlayer(player);

                OnGameLoaded(player);
                return player;
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

        public async Task<Player> LoadGameAsync()
        {
            if (!File.Exists(_saveFilePath))
            {
                OnErrorOccurred("Файл сохранения не найден. Будет начата новая игра.");
                return null;
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(_saveFilePath);
                Player player = JsonSerializer.Deserialize<Player>(jsonString, _jsonOptions);

                if (player == null)
                {
                    OnErrorOccurred("Файл сохранения повреждён");
                    return null;
                }

                ValidateAndRepairPlayer(player);
                OnGameLoaded(player);
                return player;
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

        public bool SaveFileExists() => File.Exists(_saveFilePath);

        public bool DeleteSaveFile()
        {
            try
            {
                if (File.Exists(_saveFilePath))
                    File.Delete(_saveFilePath);
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Не удалось удалить сохранение: {ex.Message}");
                return false;
            }
        }

        // Приватные методы вызова событий
        private void OnGameLoaded(Player player) => GameLoaded?.Invoke(this, player);
        private void OnGameSaved() => GameSaved?.Invoke(this, EventArgs.Empty);
        private void OnErrorOccurred(string message) => ErrorOccurred?.Invoke(this, message);
    }
}