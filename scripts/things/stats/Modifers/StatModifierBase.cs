namespace Atomation.Things;

using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// the type of stat modifier
/// </summary>
public enum ModifierTypeNew
{
	Undefined = 0,
	Flat = 1,
	Percentage = 2,
}

/// <summary>
/// base stat modifier
/// </summary>
public class StatModifierBase : Thing
{
	[JsonProperty("Target Stat", Order = 1)]
	public string TargetStat { get; protected set; }

	[JsonProperty("Value", Order = 2)]
	public float Value { get; protected set; }

	[JsonConverter(typeof(StringEnumConverter)), JsonProperty("Modifier Type", Order = 1)]
	public ModifierTypeNew Type { get; protected set; }

	[JsonIgnore]
	public object Source { get; protected set; }

	protected StatModifierBase() { }

	public StatModifierBase(string name, string description, string targetStat, float value, ModifierTypeNew type = ModifierTypeNew.Undefined,
	object source = null)
	{
		Name = name;
		Description = description;
		TargetStat = targetStat;
		Value = value;
		Type = type;
		Source = source;
	}

	public StatModifierBase(StatModifierBase statModifier,object source = null)
	{
		Name = statModifier.Name;
		Description = statModifier.Description;
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