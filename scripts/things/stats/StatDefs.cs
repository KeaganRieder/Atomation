using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atomation.Things
{
    /// <summary>
	// /// used in creation and formatting of a config file design to be read, 
	// /// and cached at game start and then used in create an instance of 
	// /// a stat
	// /// </summary>
    // public class StatDef : Thing
    // {
    //     [JsonProperty("minValue", Order = 3)]
    //     public float MinValue;

    //     [JsonProperty("maxValue", Order = 4)]
    //     public float MaxValue;
    //     [JsonProperty("baseValue", Order = 5)]
    //     public float BaseValue;
    // }

    // /// <summary>
	// /// used in creation and formatting of a config file design to be read, 
	// /// and cached at game start and then used in create an instance of 
	// /// a stat modifier
	// /// </summary>
    // public class StatModifierDef : Thing
    // {
    //     [JsonProperty("baseValue", Order = 3)]
    //     public float BaseValue;

    //     [JsonConverter(typeof(StringEnumConverter)), JsonProperty("type", Order = 3)]
    //     public ModifierType Type;
    // }
}