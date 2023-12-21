using Godot;
using System;
/*
	is the base definition fo rall stats, and is used
    for stats which can't have modifers applyed to them
    it's also is expanded upon by compStats to allow for 
    modifers
*/
public partial class StatBase : Thing
{
    protected float baseValue;
    protected float minValue;
    protected float maxValue;

    public StatBase(string name, string description, float baseVal, float min, float max)
        : base(name, description){
        baseValue = baseVal;
        minValue = min;
        maxValue = max;
    }
     public StatBase(StatDef def) : base(def){
        baseValue = def.baseValue;
        minValue = def.minValue;
        maxValue = def.maxValue;
    }

    public virtual float Value{
        get => baseValue;
        set {baseValue = Math.Clamp(value, minValue, maxValue);}
    }
}