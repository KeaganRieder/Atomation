using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;

namespace Atomation.Thing
{
	/// <summary>
	/// compThing defines the foundations of all complex object that 
	/// appear in the game world. 
	/// </summary>
	public abstract class CompThing : Thing , ICompThing
	{
		[JsonProperty("graphic data",Order = 3)]
		protected Resources.Graphic graphic;
		[JsonProperty("stats",Order = 4)]
		protected Dictionary<string, StatOld> stats;
		[JsonProperty("modifiers",Order = 5)]
		protected Dictionary<string, StatModifier> modifiers;

		/// <summary>
		/// returns value of the objects Node2D
		/// </summary>
		public Node2D ThingNode { get; set; }

		[JsonIgnore]
		public virtual Resources.Graphic Graphic { get => graphic; set { graphic = value; } }
		public virtual StatOld Stat(string statId)
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

		/// <summary>
		/// gets the Things global cords, base on it's chunks cords
		/// and not having cell size offset returns as a vector
		/// </summary>
		public virtual Vector2 GlobalPosition(){
			return ThingNode.GlobalPosition/MapSettings.CELL_SIZE;
		}
		/// <summary>
		/// gets the Things global cords, base on it's chunks cords
		/// and not having cell size offset returns x and y cords
		/// </summary>
		public virtual void GlobalPosition(out int x,out int y){
			Vector2 position = ThingNode.GlobalPosition/MapSettings.CELL_SIZE;
			x = Mathf.RoundToInt(position.X);
			y = Mathf.RoundToInt(position.Y);
		}
	}

	public interface ICompThing
	{
		public Node2D ThingNode { get; set; }
	}
}
