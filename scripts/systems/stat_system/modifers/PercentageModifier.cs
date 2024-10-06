namespace Atomation.StatSystem;

public class PercentageModifier : StatModifierBase
{
    public PercentageModifier(string id, string targetStat, float value, int order = 0, bool negative = true)
        : base(id, targetStat, value, ModifierType.Percentage, order, negative)
    {
    }

    public PercentageModifier(StatModifierBase statModifierBase)
    {
        id = statModifierBase.ID;
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