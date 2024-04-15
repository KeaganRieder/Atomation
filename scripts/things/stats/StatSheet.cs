namespace Atomation.Things;

using System.Collections.Generic;
/// <summary>
/// acts as an access point to stats and stat modifiers that objects have
/// </summary>
public class StatSheet
{
    private Dictionary<string, Stat> stats;
    private Dictionary<string, StatModifier> statModifiers;

    public StatSheet(Dictionary<string, Stat> stats, Dictionary<string, StatModifier> statModifiers)
    {
        this.stats = stats;
        this.statModifiers = statModifiers;
    }

    public void SetStat()
    {

    }
    public Stat GetStat(string statID)
    {
        if (stats.TryGetValue(statID, out Stat stat))
        {
            return stat;
        }
        else
        {
            return null;
        }
    }

    public void SetModifier()
    {

    }
    public StatModifier GetModifier(string statModifierID)
    {
        if (statModifiers.TryGetValue(statModifierID, out StatModifier statModifier))
        {
            return statModifier;
        }
        else
        {
            return null;
        }
    }

    public void ApplyModifiers(StatSheet statSheet)
    {

    }



}