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
    protected string id;
    protected string targetStat;
    protected object source;

    protected bool negative;

    protected ModifierType type;

    protected float value;
    protected int order;

    [JsonConstructor]
    public StatModifierBase() { }

    public StatModifierBase(string id, string targetStat, float value, ModifierType type = ModifierType.Undefined, 
    int order = 0, object source = null, bool negative = true)
    {
        this.id = id;
        this.targetStat = targetStat;
        this.negative = negative;
        this.type = type;
        this.value = value;
        this.order = order;
        this.source = source;
    }
    public StatModifierBase(StatModifierBase statModifierBase)
    {
        id = statModifierBase.id;
        targetStat = statModifierBase.targetStat;

        type = statModifierBase.type;
        value = statModifierBase.value;
        order = statModifierBase.order;
        source = statModifierBase.source;
    }

    [JsonProperty]
    public string ID { get => id; set => id = value; }
    [JsonProperty]
    public string TargetStat { get => targetStat; set => targetStat = value; }

    [JsonProperty]
    public ModifierType Type { get => type; set => type = value; }
    [JsonProperty]
    public float Value { get => value; protected set => this.value = value; }
    [JsonProperty]
    public int Order { get => order; protected set => order = value; }

    [JsonProperty]
    public object Source { get => source; set => source = value; }
    [JsonProperty]
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