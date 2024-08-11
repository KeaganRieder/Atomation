namespace Atomation.StatSystem;

public class PercentageModifier : StatModifierBase
{
    public PercentageModifier(string name, string targetStat, float value, int order = 0, bool negative = true)
        : base(name, targetStat, value, ModifierType.Percentage, order, negative)
    {
    }

    public PercentageModifier(PercentageModifier statModifierBase)
    {
        name = statModifierBase.name;
        targetStat = statModifierBase.targetStat;

        type = statModifierBase.type;
        value = statModifierBase.value;
        order = statModifierBase.order;
        source = statModifierBase.source;
    }

    public override float ApplyModifier(float currentValue)
    {
        float finalValue = currentValue;

        finalValue *= 1 + value;

        return finalValue;
    }
}