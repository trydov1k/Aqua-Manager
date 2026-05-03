using System.Text.Json.Serialization;

namespace AquaManager.Domain.Models;

public class SaveSlotInfo
{
    /// <summary>
    /// Имя файла сохранения (без расширения)
    /// </summary>
    public string SlotName { get; private set; }

    /// <summary>
    /// Дата и время создания сохранения
    /// </summary>
    public DateTime SavedAt { get; private set; }

    /// <summary>
    /// Игрок
    /// </summary>
    public Player Player { get; private set; }

    public SaveSlotInfo(string slotName, Player player) : this(slotName, DateTime.Now, player)
    { }

    [JsonConstructor]
    public SaveSlotInfo(string slotName, DateTime savedAt, Player player)
    {
        SlotName = slotName;
        SavedAt = savedAt;
        Player = player;
    }
}
