using Godot;
using System.Collections.Generic;
using Atomation.Resources;
using Newtonsoft.Json;

namespace Atomation.Things
{
	/// <summary>
	/// used in creating and formatting a config file design to be read, 
	/// and cached at game start and then used in create an instance of 
	/// a complex things
	/// </summary>
	public abstract class CompThingDef : Thing
	{
		[JsonProperty("Graphic Data", Order = 1)]
		public GraphicData GraphicData { get; set; }
		[JsonProperty("Stats", Order = 2)]
		public Stat[] StatDefs { get; set; }
		[JsonProperty("stat Modifiers",Order = 3)]
		public StatModifier[] StatModifersDefs { get; set; }

		/// <summary>
		/// creates stat from provided configs and then attempts to add 
		/// them to the correct collection
		/// </summary>
		public Dictionary<string, Stat> FormatStats()
		{
			Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
			foreach (Stat config in StatDefs)
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
			foreach (StatModifier config in StatModifersDefs)
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


}