namespace Atomation.StatSystem;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// A collection of stats and stat modifiers of a thing
/// in the game
/// </summary>
public class StatSheet
{
    /// <summary>
    /// the object which the stat Sheet is contain stats for
    /// </summary>
    private object targetObject;

    /// <summary>
    /// a collection of stats. The key is what the stat represents well the value is the stat
    /// </summary>
    private Dictionary<string, StatBase> stats;

    /// <summary>
    /// a collection of statModifers. The key is what the modifiers target stat well the value is the modifier
    /// </summary>
    private Dictionary<string, StatModifierBase> statModifers;

    [JsonConstructor]
    public StatSheet() { }

    public StatSheet(Dictionary<string, StatBase> stats, Dictionary<string, StatModifierBase> statModifers, object targetObject = null)
    {
        this.targetObject = targetObject;
        this.stats = stats;
        this.statModifers = statModifers;
    }

    public StatSheet(StatSheet statSheet, object targetObject)
    {
        statModifers = new Dictionary<string, StatModifierBase>();
        stats = new Dictionary<string, StatBase>();

        foreach (var stat in statSheet.stats)
        {
            stats.Add(stat.Key, new StatBase(stat.Value));
        }

        foreach (var modifier in statSheet.statModifers)
        {
            modifier.Value.Source = targetObject == null ? modifier.Value.Source : targetObject;

            if (modifier.Value.Type == ModifierType.Undefined)
            {
                GD.PushError($"{modifier.Value.ID} attempted to use undefined modifier for configs");
                return;
            }
            else if (modifier.Value.Type == ModifierType.Flat)
            {
                statModifers.Add(modifier.Key, new FlatModifier(modifier.Value));
            }
            else if (modifier.Value.Type == ModifierType.Percentage)
            {
                statModifers.Add(modifier.Key, new PercentageModifier(modifier.Value));
            }
        }
    }
    [JsonIgnore]
    public object TargetObject { get => targetObject; set => targetObject = value; }
    [JsonProperty]
    public Dictionary<string, StatBase> Stats { get => stats; private set => stats = value; }
    [JsonProperty]
    public Dictionary<string, StatModifierBase> StatModifers { get => statModifers; private set => statModifers = value; }

    public void ApplyModifiers(StatSheet modifierSheet)
    {
        foreach (var modifier in modifierSheet.StatModifers)
        {
            //make sure modifier has source if not already set
            if (modifier.Value.Source == null)
            {
                modifier.Value.Source = modifierSheet.TargetObject;
            }
            StatBase targetStat = GetStat(modifier.Value.TargetStat);

            // apply modifier to target stat if not null
            if (targetStat != null)
            {
                targetStat.AddModifier(modifier.Value);
            }
        }
    }

    /// <summary>
    /// removes all modifiers from the source
    /// </summary>
    /// <param name="source"></param>
    public void RemoveModifiers(StatSheet modifierSheet)
    {
        foreach (var stat in stats)
        {
            stat.Value.RemoveModifier(modifierSheet.TargetObject);
        }
    }

    /// <summary>
    /// removes all modifiers from the source
    /// </summary>
    /// <param name="source"></param>
    public void RemoveModifiers(object source)
    {
        foreach (var stat in stats)
        {
            stat.Value.RemoveModifier(source);
        }
    }

    public StatBase GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
        {
            return stats[statName];
        }
        else
        {
            return null;
        }
    }
}