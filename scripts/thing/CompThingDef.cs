using System.Collections.Generic;
using System.Text.Json.Serialization;
using Godot;
namespace Atomation.Thing
{
    /// <summary>
    /// base config file used for all complex things in game
    /// </summary>
    public abstract class CompThingDef : ThingDef
    {
        public GraphicData graphicData { get; set; }
        public StatDef[] statDefs { get; set; }

        //todo make function which formats things from being config 
        //data to actual
        public Dictionary<string, StatBase> CreateStats()
        {
            Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>();

            //need to work on stat modifers at some point
            foreach (StatDef def in statDefs)
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