using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atomation.Thing
{
    public enum StatType{
        Undefined = 0,
        Stat = 1,
        StatModifier = 2,
    }

    /// <summary>
    /// statDef are configuration files used to configure
    /// StatModifiers and Stats
    /// </summary>
    public class StatDef : ThingDef
    {
        /// <summary>
        /// used to determine if this is a config for a stat
        /// or modifier
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter)), JsonProperty("stat Type",Order = 3)]
        public StatType statType;

        /// <summary>
        /// value of stat or modifier
        /// </summary>
        [JsonProperty("base Value",Order = 3)]
        public float BaseValue;
        
        /// <summary>
        /// the minium value a stat can be
        /// </summary>
        [JsonProperty("min value",Order = 3)]
        public float MinValue;

        /// <summary>
        /// the maximum value a stat can be
        /// </summary>
        [JsonProperty("max value",Order = 3)]
        public float MaxValue;


    }
}