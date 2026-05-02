using System.Text.Json.Serialization;

namespace AquaManager.Domain.Models;

public class SaveSlotInfo
{
    public string SlotName { get; private set; }

    public DateTime SavedAt { get; private set; }

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
