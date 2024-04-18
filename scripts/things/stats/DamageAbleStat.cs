namespace Atomation.Things;

using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// a stat which is able to take damage which is a flat modifier
/// to it's value
/// </summary>
public class DamageAbleStat : StatBase
{
    public DamageAbleStat(string name, string description, float baseValue) : base(name, description, baseValue, StatType.DamageAble) { }
    public DamageAbleStat(StatBase damageAbleStat)
    {
        Name = damageAbleStat.Name;
        Description = damageAbleStat.Description;
        baseValue = damageAbleStat.BaseValue;
        Type = StatType.DamageAble;
    }


    public override void Damage(float damageAmt)
    {
        damage += damageAmt;

        updateValue = true;
    }
    public override void Heal(float damageAmt)
    {
        if (damage > 0)
        {
            damage = 0;
            return;
        }
        damage += damageAmt;

        updateValue = true;
    }
}