using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Resources;

namespace Atomation.Thing
{
    /// <summary>
    /// base config file used for all complex things in game
    /// </summary>
    public abstract class CompThingDef : ThingDef
    {
        [JsonProperty("GraphicConfig", Order = 3)]
        public GraphicConfig GraphicData { get; set; }
        [JsonProperty("statBases", Order = 4)]
        public StatDef[] StatDefs { get; set; }
    
        /// <summary>
        /// creates stat from provided configs and then attempts to add 
        /// them to the correct collection
        /// </summary>
        public Dictionary<string,StatOld> FormatStats(){
            Dictionary<string, StatOld> stats = new Dictionary<string, StatOld>();
            foreach (StatDef config in StatDefs)
            {
                if (config.statType == StatType.Stat)
                {
                    if (!stats.ContainsKey(config.Name))
                    {
                        stats.Add(config.Name,new StatOld(config));
                    }
                    else
                    {
                        GD.PushError($"{config.Name} is already a modifier on object");
                    }
                }                
            }
            return stats;
        }

        /// <summary>
        /// creates stat modifier from provided configs and then attempts to add 
        /// them to the correct collection
        /// </summary>
        public Dictionary<string,StatModifier> FormatStatModifers(){
            Dictionary<string,StatModifier> modifiers = new Dictionary<string, StatModifier>();
            foreach (StatDef config in StatDefs)
            {
                if (config.statType == StatType.StatModifier)
                {
                    if (!modifiers.ContainsKey(config.Name))
                    {
                        modifiers.Add(config.Name,new StatModifier(config));
                    }
                    else
                    {
                        GD.PushError($"{config.Name} is already a modifier on object");
                    }
                }                
            }
            
            return modifiers;
        }

    }
}