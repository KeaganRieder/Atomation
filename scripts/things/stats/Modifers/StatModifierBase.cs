namespace Atomation.Things;

using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// the type of stat modifier
/// </summary>
public enum ModifierType
{
	Undefined = 0,
	Flat = 1,
	Percentage = 2,
}

/// <summary>
/// base stat modifier
/// </summary>
public class StatModifierBase : Def
{
	[JsonConverter(typeof(StringEnumConverter)), JsonProperty("modifierType", Order = 1)]
	public ModifierType Type { get; protected set; }
	[JsonProperty("targetStat", Order = 2)]
	public string TargetStat { get; protected set; }

	[JsonProperty("value", Order = 3)]
	public float Value { get; protected set; }

	[JsonIgnore]
	public object Source { get; protected set; }

	protected StatModifierBase() { }

	public StatModifierBase(string name, string description, string targetStat, float value, ModifierType type = ModifierType.Undefined,
	object source = null)
	{
		defName = name;
		this.description = description;
		TargetStat = targetStat;
		Value = value;
		Type = type;
		Source = source;
	}

	public StatModifierBase(StatModifierBase statModifier, object source = null)
	{
		defName = statModifier.defName;
		description = statModifier.description;
		TargetStat = statModifier.TargetStat;
		Value = statModifier.Value;
		Type = statModifier.Type;
		Source = source;
	}

	/// <summary>
	/// increase modifier amount
	/// </summary>
	public virtual void IncreaseModifier(int val) { }
	/// <summary>
	/// decrease modifier amount
	/// </summary>
	public virtual void DecreaseModifier(int val) { }


	public virtual float ApplyModifier(float value) { return value; }
}