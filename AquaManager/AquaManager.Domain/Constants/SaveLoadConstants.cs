namespace AquaManager.Domain.Constants;

public static class SaveLoadConstants
{
    /// <summary>
    /// Название папки, в котрой будут храниться сохранения игры
    /// </summary>
    public const string DefaultFolderToSavesName = "GameSaves";

    /// <summary>
    /// Название файла, в котором будет храниться сохранение недоступное пользователю, а используемое системой для первой загрузки
    /// </summary>
    public const string DefaultSystemGameSaveFileName = "SystemGameSave";

    /// <summary>
    /// Название файла, в который будет сохраняться игра по умолчнию (если не выбрать другое имя сохранения)
    /// </summary>
    public const string DefaultGameSaveName = "gamesave";

    /// <summary>
    /// Расширение файлов для сохранения игры (.json). Обязательно с точкой
    /// </summary>
    public const string DefaultSaveFileExtension = ".json";
}
