using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Atomation.Map;
using Atomation.Resources;

namespace Atomation.Thing
{
	/// <summary>
	/// definition file used in creating new instances of more complex things 
	/// </summary>
	public abstract class CompThingDef : ThingDef
	{
		[JsonProperty("graphicData", Order = 3)]
		public GraphicData GraphicData { get; set; }
		[JsonProperty("statBases", Order = 4)]
		public StatDef[] StatDefs { get; set; }
		[JsonProperty("statModifiers")]
		public StatModifierDef[] StatModifersDefs { get; set; }

		/// <summary>
		/// creates stat from provided configs and then attempts to add 
		/// them to the correct collection
		/// </summary>
		public Dictionary<string, Stat> FormatStats()
		{
			Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
			foreach (StatDef config in StatDefs)
			{
				if (!stats.ContainsKey(config.Name))
				{
					stats.Add(config.Name, new Stat(config));
				}
				else
				{
					GD.PushError($"{config.Name} is already a modifier on object");
				}

			}
			return stats;
		}

		/// <summary>
		/// creates stat modifier from provided configs and then attempts to add 
		/// them to the correct collection
		/// </summary>
		public Dictionary<string, StatModifier> FormatStatModifers()
		{
			Dictionary<string, StatModifier> modifiers = new Dictionary<string, StatModifier>();
			foreach (StatModifierDef config in StatModifersDefs)
			{
				if (!modifiers.ContainsKey(config.Name))
				{
					modifiers.Add(config.Name, new StatModifier(config));
				}
				else
				{
					GD.PushError($"{config.Name} is already a modifier on object");
				}

			}

			return modifiers;
		}
	}

	/// <summary>
	/// base class for more complex things in the game 
	/// </summary>
	public abstract partial class CompThing : Node2D,ICompThing
	{
		public string Description { get; set; }
		protected Dictionary<string, Stat> stats;
		protected Dictionary<string, StatModifier> modifiers;

		public Node2D Node{get=> this;}

		public virtual Stat GetStat(string key ){
			if (stats.TryGetValue(key,out Stat stat))
			{
				return stat;
			}
			else
			{
				return null;
			}
		}

		public virtual StatModifier GetModifier(string key ){
			if (modifiers.TryGetValue(key,out StatModifier modifier))
			{
				return modifier;
			}
			else
			{
				return null;
			}
		}

	}

	public interface ICompThing {
		public Node2D Node{get;}
	}

}
