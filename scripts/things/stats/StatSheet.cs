namespace Atomation.Things;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// An stat sheet contains an object stat modifiers and Stats
/// </summary>
public class StatSheet
{
    [JsonProperty("Stats", Order = 1)]
    private Dictionary<string, StatBase> stats;

    [JsonProperty("StatModifers", Order = 2)]
    private Dictionary<string, StatModifierBase> statModifers;

    public StatSheet()
    {

    }

    public StatSheet(Dictionary<string, StatBase> stats, Dictionary<string, StatModifierBase> statModifers)
    {
        this.stats = stats;
        this.statModifers = statModifers;
    }

    public StatSheet(StatSheet statSheet, object source = null)
    {
        stats = new Dictionary<string, StatBase>();
        statModifers = new Dictionary<string, StatModifierBase>();

        foreach (var stat in statSheet.stats)
        {
            AddStats(stat.Key, stat.Value);
        }
        foreach (var modifier in statSheet.statModifers)
        {
            AddModifiers(modifier.Key, modifier.Value);
        }
    }

    private void AddStats(string key, StatBase stat){
        
        if (stat.Type == StatType.Modifiable)
        {
            stats.Add(key, new ModifiableStat(stat));
        }
        else if (stat.Type == StatType.Constant)
        {
            // GD.PushError("Constant Stat are currently not implemented");
            stats.Add(key, new StatBase(stat));
        }
        else
        {
            // GD.PushError("Stat type is undefined");
            stats.Add(key, new StatBase(stat));
        }
    }

    private void AddModifiers(string key, StatModifierBase statModifier){
        
        if (statModifier.Type == ModifierType.Flat)
        {
            statModifers.Add(key, new FlatStatModifier(statModifier));
        }
        else if (statModifier.Type == ModifierType.Percentage)
        {
            // GD.PushError("Percentage modifiers are currently not implemented");
            statModifers.Add(key, new StatModifierBase(statModifier));
        }
        else
        {
            // GD.PushError("Stat modified type is undefined");
            statModifers.Add(key, new StatModifierBase(statModifier));
        }
    }

    public StatBase GetStat(string key)
    {
        if (stats.TryGetValue(key, out StatBase stat))
        {
            return stat;
        }
        else
        {
            return null;
        }
    }
    public StatModifierBase GetStatModifier(string key)
    {
        if (statModifers.TryGetValue(key, out StatModifierBase stat))
        {
            return stat;
        }
        else
        {
            return null;
        }
    }

    public void ApplyModifiers(Dictionary<string, StatModifierBase> modifiers)
    {
        StatBase stat;

        foreach (var modifier in modifiers)
        {
            string statKey = modifier.Value.TargetStat;
            if ((stat = GetStat(statKey)) != null)
            {
                stat.AddModifier(modifier.Value);
            }
        }
    }
    public void RemoveModifiers()
    {
        GD.Print("implementation of removing stat modifiers is needed");

    }
}