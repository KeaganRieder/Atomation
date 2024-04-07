using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation;
using Atomation.Resources;

namespace Atomation.Things
{
	/// <summary>
	/// base of all complex things inside the game. these are things which 
	/// generally appear in the world or have more complex functionality
	/// </summary>
	public abstract partial class CompThing : Node2D, ICompThing
	{
		protected Coordinate coordinate;
		protected Dictionary<string, Stat> stats;
		protected Dictionary<string, StatModifier> modifiers;

		public Node2D Node { get => this; }
		public string Description { get; set; }
		public Coordinate Coordinate { get => coordinate; }

		/// <summary>
		/// gets the stat of the provide type
		/// </summary>
		public virtual Stat Stat(string key)
		{
			if (stats.TryGetValue(key, out Stat stat))
			{
				return stat;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// gets the StatModifier of the provide type
		/// </summary>
		public virtual StatModifier StatModifier(string key)
		{
			if (modifiers.TryGetValue(key, out StatModifier modifier))
			{
				return modifier;
			}
			else
			{
				return null;
			}
		}

	}
}
