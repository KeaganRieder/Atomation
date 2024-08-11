using Newtonsoft.Json;

namespace Atomation.StatSystem;

/// <summary>
/// determines how the modifier gets applied to the stat
/// </summary>
public enum ModifierType
{
    Undefined = -1,
    Flat = 1,
    Percentage = 2,

}

/// <summary>
/// base stat modifier class which are used to define aspect in the game 
/// that act ass effects on stats which can modify them
/// </summary>
public class StatModifierBase
{
    protected string name;
    protected string targetStat;
    protected object source;

    protected bool negative;

    protected ModifierType type;

    protected float value;
    protected int order;

    [JsonConstructor]
    public StatModifierBase() { }

    public StatModifierBase(string name, string targetStat, float value, ModifierType type = ModifierType.Undefined, 
    int order = 0, object source = null, bool negative = true)
    {
        this.name = name;
        this.targetStat = targetStat;
        this.negative = negative;
        this.type = type;
        this.value = value;
        this.order = order;
        this.source = source;
    }
    public StatModifierBase(StatModifierBase statModifierBase)
    {
        name = statModifierBase.name;
        targetStat = statModifierBase.targetStat;

        type = statModifierBase.type;
        value = statModifierBase.value;
        order = statModifierBase.order;
        source = statModifierBase.source;
    }

    public string Name { get => name; set => name = value; }
    public string TargetStat { get => targetStat; set => targetStat = value; }

    public ModifierType Type { get => type; set => type = value; }
    public float Value { get => value; protected set => this.value = value; }
    public int Order { get => order; protected set => order = value; }

    public object Source { get => source; set => source = value; }
    public bool Negative { get => negative; protected set => negative = value; }

    /// <summary>
    /// applies this modifier to the given stat
    /// </summary>
    public virtual float ApplyModifier(float currentValue)
    {
        float finalValue = currentValue;
        return finalValue;
    }

}