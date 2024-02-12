using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation;
namespace Atomation.Thing
{
    /// <summary>
    /// compThing defines the foundtaions of all complex obejct that 
    /// appear in the game world. 
    /// </summary>
    public abstract partial class CompThing : Thing
    {
        // protected Node2D objNode;
        [JsonProperty("graphic data")]
        protected Resources.Graphic graphic;
        [JsonProperty("stats")]
        protected Dictionary<string, StatBase> stats;

        // public virtual Node2D ObjNode{get => objNode; set{objNode = value;}} maybe?
        [JsonIgnore]
        public virtual Resources.Graphic Graphic { get => graphic; set { graphic = value; } }
        public virtual StatBase Stat(string statId)
        {
            if (stats.TryGetValue(statId, out var stat))
            {
                return stat;
            }
            else
            {
                throw new KeyNotFoundException($"Error: Stat {statId} isn't present in {this.name}");
            }
        }

    }
}