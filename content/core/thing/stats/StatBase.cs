using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


/// <summary>
/// a stat which doesn't hold modifer and can't act as one,
/// meant to be stats which are sort of persitant 
/// </summary>
public partial class StatBase : Thing
{
    //base values
    [JsonProperty]
    protected float baseValue;
    [JsonProperty]
    protected float minValue = 0;
    [JsonProperty]
    protected float maxValue = 0;

    //complex stat values
    protected float currentVal = 0;
    protected bool updateStat;
    protected Dictionary<string, StatModifer> modifers; // only for prolonged modifers\

    //stat modifer values
    [JsonProperty("order")]
    protected int Order{get; set;}  = 0;

    //constrcutors
    public StatBase() : this("default", "default", 0){}
    public StatBase(string name, string description, float baseVal)
        : base(name, description){
        baseValue = baseVal;
    }
    
    // public StatBase(StatDef def) : this(def.DefName,def.Description, def.baseValue){}
    public virtual float Value{
        get => baseValue;
        set {baseValue = Math.Clamp(value, minValue, maxValue);}
    }
}
