using System.Text.Json.Serialization;

namespace IdleSim.scenes.main.components;

public class PlayerDataJson
{
    [JsonPropertyName("currency")] public CurrencyData Currency { get; set; }
}

public class CurrencyData
{
    [JsonPropertyName("money")] public int Money { get; set; }
    [JsonPropertyName("skillPoints")] public int SkillPoints { get; set; }
}