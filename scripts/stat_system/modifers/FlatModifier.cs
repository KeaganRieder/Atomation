namespace Atomation.StatSystem;

public class FlatModifier : StatModifierBase
{
    public FlatModifier(string name, string targetStat, float value, int order = 0, bool negative = true)
        : base(name, targetStat, value, ModifierType.Flat, order, negative)
    {

    }
    public FlatModifier(FlatModifier statModifierBase)
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

        finalValue += value;

        return finalValue;
    }
}