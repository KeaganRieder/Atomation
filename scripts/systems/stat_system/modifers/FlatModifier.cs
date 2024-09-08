namespace Atomation.StatSystem;

public class FlatModifier : StatModifierBase
{
    public FlatModifier(string name, string targetStat, float value, int order = 0, bool negative = true)
        : base(name, targetStat, value, ModifierType.Flat, order, negative)
    {

    }
    
    public FlatModifier(StatModifierBase statModifierBase)
    {
        name = statModifierBase.Name;
        targetStat = statModifierBase.TargetStat;

        type = statModifierBase.Type;
        value = statModifierBase.Value;
        order = statModifierBase.Order;
        source = statModifierBase.Source;
    }

    public override float ApplyModifier(float currentValue)
    {
        float finalValue = currentValue;

        finalValue += value;

        return finalValue;
    }
}