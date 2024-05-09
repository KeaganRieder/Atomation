namespace Atomation.Things;

using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// a stat which is able to take damage which is a flat modifier
/// to it's value
/// </summary>
public class ModifiableStat : StatBase
{
    public ModifiableStat(string name, string description, float baseValue)
    : base(name, description, baseValue, StatType.Modifiable) { }
    public ModifiableStat(StatBase modifiableStat)
    {
        defName = modifiableStat.defName;
        description = modifiableStat.description;
        baseValue = modifiableStat.BaseValue;
        Type = StatType.Modifiable;
    }

    public override void Damage(float damageAmt)
    {
        if (damage >= baseValue)
        {
            damage = baseValue;
            return;
        }
        damageAmt = Mathf.Abs(damageAmt);

        damage += damageAmt;

        updateValue = true;
    }
    public override void Heal(float damageAmt)
    {
        if (damage <= 0)
        {
            damage = 0;
            return;
        }
        damageAmt = Mathf.Abs(damageAmt);
        damage -= damageAmt;

        updateValue = true;
    }
}