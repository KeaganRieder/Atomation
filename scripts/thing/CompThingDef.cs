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
        [JsonProperty("GraphicConfig")]
        public GraphicConfig GraphicData { get; set; }
        [JsonProperty("statBases")]
        public StatDef[] StatDefs { get; set; }

        //todo make function which formats things from being config 
        //data to actual
        public Dictionary<string, StatBase> CreateStats()
        {
            Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>();

            //need to work on stat modifers at some point
            foreach (StatDef def in StatDefs)
            {
                if (stats.ContainsKey(def.Name))
                {
                    GD.PrintErr($"ERROR: attempted to add {def.Name} which is already presnt");
                }
                else
                {
                    stats.Add(def.Name, new Stat(def));
                }
            }

            return stats;
        }

    }
}