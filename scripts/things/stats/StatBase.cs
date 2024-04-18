namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum StatType
{
    Undefined = 0,
    Constant = 1,
    DamageAble = 2,
}

/// <summary>
/// base of stats
/// </summary>
public class StatBase : Thing
{
    [JsonConverter(typeof(StringEnumConverter)), JsonProperty("Stat Type", Order = 1)]
    public StatType Type { get; protected set; }

    [JsonProperty("Value", Order = 2)]
    protected float baseValue;
    protected float currentValue;
    protected float damage;

    private List<FlatStatModifier> flatStatModifiers;

    protected bool updateValue;

    protected StatBase()
    {
        flatStatModifiers = new();
    }
    public StatBase(string name, string description, float baseValue, StatType statType = StatType.Undefined) : base()
    {
        Name = name;
        Description = description;
        Type = statType;
        this.baseValue = baseValue;
        currentValue = baseValue;
        damage = 0;

        updateValue = false;
    }

    public StatBase(StatBase statBase) : base()
    {
        Name = statBase.Name;
        Description = statBase.Description;
        Type = statBase.Type;
        baseValue = statBase.BaseValue;
        currentValue = statBase.BaseValue;
        damage = 0;

        updateValue = false;
    }

    [JsonIgnore]
    public virtual float MaxValue { get => baseValue; }
    [JsonIgnore]
    public virtual float BaseValue { get => baseValue; }

    [JsonIgnore]
    public virtual float Value
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
        if (statModifier.Type == ModifierTypeNew.Undefined)
        {
            GD.PushError($"{statModifier.Name} is of undefined type");
        }
        else if (statModifier.Type == ModifierTypeNew.Flat)
        {
            flatStatModifiers.Add((FlatStatModifier)statModifier);
            updateValue = true;
        }
        else if (statModifier.Type == ModifierTypeNew.Percentage)
        {
            GD.PushError($"{statModifier.Name} is of Percentage type which is currently not implemented");
        }
    }

    /// <summary>
    /// Remove a modifier
    /// </summary>
    public virtual void RemoveModifier(StatModifierBase statModifier)
    {
        if (statModifier.Type == ModifierTypeNew.Undefined)
        {
            GD.PushError($"{statModifier.Name} is of undefined type");
        }
        else if (statModifier.Type == ModifierTypeNew.Flat)
        {
            RemoveModifier((FlatStatModifier)statModifier, flatStatModifiers);
        }
        else if (statModifier.Type == ModifierTypeNew.Percentage)
        {
            GD.PushError($"{statModifier.Name} is of Percentage type which is currently not implemented");

        }
    }

    /// <summary>
    /// Remove all modifiers from source
    /// </summary>
    public virtual void RemoveModifier(object source)
    {
        //todo
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
        if (damage > 0)
        {
            damage = 0;
            return;
        }
        damage += damageAmt;

        updateValue = true;
    }
    protected virtual void UpdateStat()
    {
        // float flat = ApplyFlatModifiers();
        // float finalValue = flat;

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
        string objString = $"{Value}";
        return objString;
    }

}