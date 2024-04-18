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
        this.stats = new Dictionary<string, StatBase>();
        this.statModifers = new Dictionary<string, StatModifierBase>();

        foreach (var stat in statSheet.stats)
        {
            this.stats.Add(stat.Key, new StatBase(stat.Value));
        }
        foreach (var modifier in statSheet.statModifers)
        {
            this.statModifers.Add(modifier.Key, new StatModifierBase(modifier.Value));
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