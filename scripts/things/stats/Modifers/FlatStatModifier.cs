namespace Atomation.Things;


public class FlatStatModifier : StatModifierBase
{
    public FlatStatModifier(string name, string description, string targetStat, float value, object source = null)
    : base(name, description, targetStat, value, ModifierType.Flat, source) { }

    public FlatStatModifier(StatModifierBase statModifier, object source = null)
    {
        defName = statModifier.defName;
        description = statModifier.description;
		TargetStat = statModifier.TargetStat;
        Value = statModifier.Value;
        Type = ModifierType.Flat;
        Source = source;
    }

    /// <summary>
	/// increase modifier amount
	/// </summary>
	public override void IncreaseModifier(int val)
    {
        Value += val;
    }
    /// <summary>
    /// decrease modifier amount
    /// </summary>
    public override void DecreaseModifier(int val)
    {
        Value -= val;
    }

    public override float ApplyModifier(float value)
    {
        return value + Value;

    }



}