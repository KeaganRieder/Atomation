namespace Atomation.StatSystem;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// The base of all stats, which are used to define different aspects of
/// things in the game. This class define values used by all stats
/// </summary>
public class StatBase
{
    protected string name;
    protected string description;

    /// <summary>
    /// used to determine if teh stat can be modified or not
    /// </summary>
    protected bool modifiable;

    /// <summary>
    /// used to determine if the stats current value needs to be updated
    /// </summary>
    protected bool updateValue;

    /// <summary>
    /// can teh stat be damaged
    /// </summary>
    // private bool damageAble;

    /// <summary>
    /// the base value of a stat, used to calculate the 
    /// current value of teh stat
    /// </summary>
    protected float value;

    /// <summary>
    /// the current value of the stat
    /// </summary>
    protected float currentValue;

    /// <summary>
    /// the lowest value the stat can reach
    /// </summary>
    protected float minValue;

    /// <summary>
    /// the highest value the stat can reach
    /// </summary>
    protected float maxValue;

    /// <summary>
    /// damage is a flat modifier that decreases the 
    /// the stats value
    /// </summary>
    private float damage = 0;

    protected List<StatModifierBase> flatModifiers;
    protected List<StatModifierBase> percentageModifiers;

    [JsonConstructor]
    public StatBase()
    {

    }

    public StatBase(string name, string description, float value, float minValue, float maxValue, bool modifiable = true)
    {
        this.name = name;
        this.description = description;
        flatModifiers = new List<StatModifierBase>();
        percentageModifiers = new List<StatModifierBase>();
        this.modifiable = modifiable;
        updateValue = true;

        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    public StatBase(StatBase stat)
    {
        name = stat.name;
        description = stat.description;
        modifiable = stat.modifiable;

        //todo copying
        flatModifiers = new List<StatModifierBase>();
        percentageModifiers = new List<StatModifierBase>();

        updateValue = true;

        value = stat.value;
        minValue = stat.minValue;
        maxValue = stat.maxValue;
    }

    [JsonProperty]
    public bool Modifiable { get => modifiable; set => modifiable = value; }
    [JsonProperty]
    public float Value { get => value; protected set => this.value = value; }
    [JsonProperty]
    public float MaxValue { get => maxValue; set => maxValue = value; }
    [JsonProperty]
    public float MinValue { get => minValue; set => minValue = value; }
    [JsonProperty]
    public List<StatModifierBase> FlatModifiers { get => flatModifiers; protected set => flatModifiers = value; }
    [JsonProperty]
    public List<StatModifierBase> PercentageModifiers { get => percentageModifiers; protected set => percentageModifiers = value; }

    [JsonProperty]
    public float Damage
    {
        get => damage;
        set
        {
            damage = value < 0 ? 0 : value;
            updateValue = true;
        }
    }
    [JsonProperty]
    public float CurrentValue
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

    [JsonProperty]
    public string Description { get => description; set => description = value; }

    /// <summary>
    /// applies the given modifier to the stat
    /// </summary>
    public void AddModifier(StatModifierBase statModifier)
    {
        if (!modifiable)
        {
            return;
        }
        else if (statModifier.Type == ModifierType.Undefined)
        {
            GD.PushError($"{statModifier.Name} is of undefined type");
            return;
        }
        else if (statModifier.Type == ModifierType.Flat)
        {
            flatModifiers.Add(statModifier);
            flatModifiers.Sort(StatUtil.CompareModifierOrder);
        }
        else if (statModifier.Type == ModifierType.Percentage)
        {
            percentageModifiers.Add(statModifier);
            percentageModifiers.Sort(StatUtil.CompareModifierOrder);
        }

        updateValue = true;
    }

    /// <summary>
    /// removes provided modifier from stat
    /// </summary>
    /// <returns>
    /// true if modifiers are removed
    /// false if not
    /// </returns>
    public bool RemoveModifier(StatModifierBase statModifier)
    {
        if (!modifiable)
        {
            return false;
        }
        else if (statModifier.Type == ModifierType.Undefined)
        {
            GD.PushError($"{statModifier.Name} is of undefined type");
            return false;
        }
        else if (statModifier.Type == ModifierType.Flat)
        {
            flatModifiers.Remove(statModifier);
        }
        else if (statModifier.Type == ModifierType.Percentage)
        {
            percentageModifiers.Remove(statModifier);
        }

        updateValue = true;
        return true;
    }

    /// <summary>
    /// removes provided modifiers from stat from provided source
    /// </summary>
    /// <returns>
    /// true if modifiers are removed
    /// false if not
    /// </returns>
    public bool RemoveModifier(object source)
    {
        bool removed = false;
        foreach (var flatModifiers in flatModifiers)
        {
            if (flatModifiers.Source == source)
            {
                RemoveModifier(flatModifiers);
                removed = true;
            }
        }
        foreach (var percentageModifiers in percentageModifiers)
        {
            if (percentageModifiers.Source == source)
            {
                RemoveModifier(percentageModifiers);
                removed = true;
            }
        }

        return removed;

    }

    /// <summary>
    /// updates the value of the stat, based on currently applied modifiers
    /// </summary>
    protected void UpdateStat()
    {
        float finalValue = value;

        finalValue -= damage;

        foreach (var flatModifier in flatModifiers)
        {
            finalValue = flatModifier.ApplyModifier(finalValue);
        }

        foreach (var percentageModifiers in percentageModifiers)
        {
            finalValue = percentageModifiers.ApplyModifier(finalValue);
        }

        currentValue = finalValue;
        updateValue = false;
    }

}