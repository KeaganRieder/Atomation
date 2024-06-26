namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum StatType
{
    Undefined = 0,
    Constant = 1,
    Modifiable = 2,
}

/// <summary>
/// base of stats
/// </summary>
public class StatBase : Def
{
    [JsonConverter(typeof(StringEnumConverter)), JsonProperty(Order = 1)]
    public StatType Type { get; protected set; }

    [JsonProperty(Order = 2)]
    protected float baseValue;
    protected float currentValue;
    protected float damage;

    private List<FlatStatModifier> flatStatModifiers;

    protected bool updateValue;

    protected StatBase()
    {
        flatStatModifiers = new();
        updateValue = true;

    }
    public StatBase(string name, string description, float baseValue, StatType statType = StatType.Undefined) : base()
    {
        defName = name;
        this.description = description;
        Type = statType;
        this.baseValue = baseValue;
        currentValue = baseValue;
        damage = 0;

        updateValue = true;
    }

    public StatBase(StatBase statBase) : base()
    {
        defName = statBase.defName;
        description = statBase.description;
        Type = statBase.Type;
        baseValue = statBase.BaseValue;
        currentValue = baseValue;
        damage = 0;

        updateValue = true;
    }

    [JsonIgnore]
    public virtual float MaxValue { get => baseValue; }
    [JsonIgnore]
    public virtual float BaseValue { get => baseValue; }

    [JsonIgnore]
    public virtual float CurrentValue
    {
        get
        {
            if (updateValue)
            {
                UpdateStat();
            }
            return currentValue;
        }
    }

    /// <summary>
    /// Add a modifier
    /// </summary>
    public virtual void AddModifier(StatModifierBase statModifier)
    {
        if (statModifier.Type == ModifierType.Undefined)
        {
            GD.PushError($"{statModifier.defName} is of undefined type");
        }
        else if (statModifier.Type == ModifierType.Flat)
        {
            flatStatModifiers.Add((FlatStatModifier)statModifier);
            updateValue = true;
        }
        else if (statModifier.Type == ModifierType.Percentage)
        {
            GD.PushError($"{statModifier.defName} is of Percentage type which is currently not implemented");
        }
    }

    /// <summary>
    /// Remove a modifier
    /// </summary>
    public virtual void RemoveModifier(StatModifierBase statModifier)
    {
        if (statModifier.Type == ModifierType.Undefined)
        {
            GD.PushError($"{statModifier.defName} is of undefined type");
        }
        else if (statModifier.Type == ModifierType.Flat)
        {
            RemoveModifier((FlatStatModifier)statModifier, flatStatModifiers);
        }
        else if (statModifier.Type == ModifierType.Percentage)
        {
            GD.PushError($"{statModifier.defName} is of Percentage type which is currently not implemented");

        }
    }

    /// <summary>
    /// Remove all modifiers from source
    /// </summary>
    public virtual void RemoveModifier(object source)
    {
        GD.PushError("Remove Modifier From Source Not Implemented");
    }

    /// <summary>
    /// Remove a modifier
    /// </summary>
    protected virtual void RemoveModifier<modType>(modType modifier, List<modType> modifierTable)
    {
        if (modifierTable.Remove(modifier))
        {
            updateValue = true;
        }
    }

    /// <summary>
    /// damage stat
    /// </summary>
    public virtual void Damage(float damageAmt)
    {
        damage += damageAmt;

        updateValue = true;
    }
    /// <summary>
    /// heal stat
    /// </summary>
    public virtual void Heal(float damageAmt)
    {
        updateValue = true;
    }
    protected virtual void UpdateStat()
    {
        currentValue = baseValue - damage;
        updateValue = false;
    }

    protected virtual float ApplyFlatModifiers()
    {
        float flatVal = baseValue;
        foreach (var modifiers in flatStatModifiers)
        {
            flatVal = modifiers.ApplyModifier(flatVal);
        }
        return flatVal;
    }

    public override string ToString()
    {
        string objString = $"{CurrentValue}";
        return objString;
    }

}