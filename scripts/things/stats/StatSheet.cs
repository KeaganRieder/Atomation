namespace Atomation.Things;

using System.Collections.Generic;
using Godot;

/// <summary>
/// An stat sheet contains an object stat modifiers and Stats
/// </summary>
public class StatSheet
{
    public Dictionary<string, StatBase> Stats { get; private set; }

    public Dictionary<string, StatModifierBase> StatModifers { get; private set; }

    public StatSheet()
    {
        Stats = new Dictionary<string, StatBase>();
        StatModifers = new Dictionary<string, StatModifierBase>();
    }

    public StatSheet(Dictionary<string, StatBase> stats = null, Dictionary<string, StatModifierBase> statModifers = null)
    {
        this.Stats = (stats == null) ? new Dictionary<string, StatBase>() : stats;
        this.StatModifers = (StatModifers == null) ? new Dictionary<string, StatModifierBase>() : statModifers;
    }

    public StatBase GetStat(string key)
    {
        if (Stats.TryGetValue(key, out StatBase stat))
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
        if (StatModifers.TryGetValue(key, out StatModifierBase stat))
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