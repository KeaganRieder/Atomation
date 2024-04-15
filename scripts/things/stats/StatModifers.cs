namespace Atomation.Things;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary> determines the type of operation the modifier does </summary>

public enum ModifierType
{
	Flat,
	AdditivePercentage,
	MultiplicativePercentage,
}

/// <summary> determines how long a modifier can last  </summary>
public enum ModifierLife{
    Persistent,
    Temporary,
}

/// <summary>
/// class used for all stat modifiers
/// </summary>
/// 
public class StatModifier : Thing
{
	[JsonProperty("Base Value", Order = 1)]
	public float Value { get; private set; }
	
	[JsonConverter(typeof(StringEnumConverter)), JsonProperty("Modifier Type", Order = 1)]
	public ModifierType Type { get; private set; }

	[JsonIgnore]
	public object Source { get; private set; }

	// private 

	public StatModifier() { }

	public StatModifier(string name, string description, float value, ModifierType type, object source = null)
	{
		Name = name;
		Description = description;
		Value = value;
		Type = type;
		Source = source;
	}
	public StatModifier(StatModifier statModifier)
	{
		Name = statModifier.Name;
		Description = statModifier.Description;
		Value = statModifier.Value;
		Type = statModifier.Type;
		Source = statModifier.Source;
	}

	public void SetSource(object source)
	{
		Source = source;
	}
}