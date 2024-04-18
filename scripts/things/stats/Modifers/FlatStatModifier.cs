namespace Atomation.Things;


public class FlatStatModifier : StatModifierBase
{
    public FlatStatModifier(string name, string description, string targetStat, float value, object source = null)
    : base(name, description, targetStat, value, ModifierTypeNew.Flat, source) { }

    public FlatStatModifier(StatModifierBase statModifier, object source = null)
    {
        Name = statModifier.Name;
        Description = statModifier.Description;
        Value = statModifier.Value;
        Type = ModifierTypeNew.Flat;
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