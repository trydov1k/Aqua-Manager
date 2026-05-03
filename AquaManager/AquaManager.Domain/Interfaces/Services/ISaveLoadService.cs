
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
    bool SaveGame(SaveSlotInfo saveInfo);

    /// <summary>
    /// Асинхронное сохранение (не блокирует UI)
    /// </summary>
    Task<bool> SaveGameAsync(SaveSlotInfo saveInfo);

    /// <summary>
    /// Загрузить состояние игрока из файла (синхронно)
    /// </summary>
    SaveSlotInfo? LoadGame(string slotName);

    /// <summary>
    /// Асинхронная загрузка
    /// </summary>
    Task<SaveSlotInfo?> LoadGameAsync(string slotName);

    /// <summary>
    /// Проверить, существует ли файл сохранения
    /// </summary>
    bool SaveFileExists(string fileName);

    /// <summary>
    /// Удалить файл сохранения (начать новую игру)
    /// </summary>
    bool DeleteSaveFile(string fileName);

    /// <summary>
    /// Получить перечисление всех файлов сохранений
    /// </summary>
    /// <returns>Массив, содержащий все сохранения</returns>
    IEnumerable<string> GiveAllSaveFileNames();
}
