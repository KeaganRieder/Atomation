using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atomation.Thing
{
    /// <summary>
    /// config file used to create stats
    /// </summary>
    public class StatDef : ThingDef
    {
        [JsonProperty("minValue", Order = 3)]
        public float MinValue;

        [JsonProperty("maxValue", Order = 4)]
        public float MaxValue;
        [JsonProperty("baseValue", Order = 5)]
        public float BaseValue;
    }

    /// <summary>
    /// config file used to create stat modifiers
    /// </summary>
    public class StatModifierDef : ThingDef
    {
        [JsonProperty("baseValue", Order = 3)]
        public float BaseValue;

        [JsonConverter(typeof(StringEnumConverter)), JsonProperty("type", Order = 3)]
        public ModifierType Type;
    }
}