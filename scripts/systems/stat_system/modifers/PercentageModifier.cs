namespace Atomation.StatSystem;

public class PercentageModifier : StatModifierBase
{
    public PercentageModifier(string name, string targetStat, float value, int order = 0, bool negative = true)
        : base(name, targetStat, value, ModifierType.Percentage, order, negative)
    {
    }

    public PercentageModifier(StatModifierBase statModifierBase)
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

        finalValue *= 1 + value;

        return finalValue;
    }
}