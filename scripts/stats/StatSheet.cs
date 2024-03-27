using System.Collections.Generic;
using Atomation.Thing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atomation.Stats
{
    /// <summary>
    /// A stat sheet contains the configs for the stats and modifiers of 
    /// a entity in the game
    /// </summary>
    public class StatSheet
    {   //maybe make this the config file? 
        [JsonProperty("stat defs", Order = 1)]
        public Dictionary<string, StatDef> Stats;
        [JsonProperty("stat modifier defs", Order = 2)]
        public Dictionary<string, StatModifierDef> StatModifier;

        public Dictionary<string, Stat> FormatStats()
        {
            Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

            return stats;
        }
    }

    /// <summary>
    /// StatDefs are used to create config files in order to create instances of 
    /// an objects stat
    /// </summary>
    public class StatDef : ThingDef
    {
        [JsonProperty("min value", Order = 3)]
        public float MinValue;

        [JsonProperty("max value", Order = 4)]
        public float MaxValue;
        [JsonProperty("base value", Order = 5)]
        public float BaseValue;
    }

    /// <summary>
    /// StatModifierDefs are used to create config files in order to create instances of 
    /// an objects StatModifier
    /// </summary>
    public class StatModifierDef : ThingDef
    {
        [JsonProperty("base value", Order = 3)]
        public float BaseValue;

        [JsonConverter(typeof(StringEnumConverter)), JsonProperty("type", Order = 3)]
        public ModifierType Type;
    }
}