
using AquaManager.Domain.Models;

namespace AquaManager.Domain.Interfaces.Services;

public interface ISaveLoadService
{
    /// <summary>
    /// Событие возникает после успешной загрузки сохранения
    /// </summary>
    event EventHandler<Player> GameLoaded;

    /// <summary>
    /// Событие возникает после успешного сохранения
    /// </summary>
    event EventHandler GameSaved;

    /// <summary>
    /// Событие возникает при ошибке сохранения/загрузки
    /// </summary>
    event EventHandler<string> ErrorOccurred;

    /// <summary>
    /// Сохранить текущее состояние игрока в файл (синхронно)
    /// </summary>
    bool SaveGame(Player player);

    /// <summary>
    /// Асинхронное сохранение (не блокирует UI)
    /// </summary>
    Task<bool> SaveGameAsync(Player player);

    /// <summary>
    /// Загрузить состояние игрока из файла (синхронно)
    /// </summary>
    Player LoadGame();

    /// <summary>
    /// Асинхронная загрузка
    /// </summary>
    Task<Player> LoadGameAsync();

    /// <summary>
    /// Проверить, существует ли файл сохранения
    /// </summary>
    bool SaveFileExists();

    /// <summary>
    /// Удалить файл сохранения (начать новую игру)
    /// </summary>
    bool DeleteSaveFile();
}
