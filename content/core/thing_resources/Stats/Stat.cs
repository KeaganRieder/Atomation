using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// todo
/// </summary>
public partial class Stat : StatBase
{

    [JsonProperty]
    protected float current;
    [JsonProperty]
    protected float minValue;
    [JsonProperty]
    protected float maxValue;
    protected Dictionary<string, StatModifer> valueModifers;
    protected Dictionary<string, StatModifer> maxModifers;

    private bool updateValues = false;

    public Stat(){
        valueModifers = new Dictionary<string, StatModifer>();
        maxModifers = new Dictionary<string, StatModifer>();
    }

    public override float Value{
        get{
            if (updateValues)
            {
                ApplyBaseModifers();
                ApplyMaxModifers();
                updateValues = false;
            }
            return  current;
        }
        set
        {
            baseValue = value;
        }
    }
    public float Min{get => minValue; private set{minValue = value;}}
    public float Man{get => maxValue; private set{maxValue = value;}}

    //todo overload += and -= operators to apply instant dmg or healing

    public void AddMaxModifer(StatModifer modifer){
        if (!maxModifers.ContainsKey(modifer.Name))
        {
            maxModifers.Add(modifer.Name,modifer);
            updateValues = true;
        }
        else
        {
            //output warning
        }
    }
    public void RemoveMaxModifer(StatModifer modifer){
        if (!maxModifers.ContainsKey(modifer.Name))
        {
            throw new KeyNotFoundException($"modifer {modifer.Name} wasn't found in {this.Name}");
        }
        else
        {
            maxModifers.Remove(modifer.Name);
            updateValues = true;
        }
    }

    public void AddBaseModifer(StatModifer modifer){
        if (!valueModifers.ContainsKey(modifer.Name))
        {
           valueModifers.Remove(modifer.Name); 
           updateValues = true;
        }
        else
        {
            //output warning
        }
    }
    public void RemoveBaseModifer(StatModifer modifer){
        if (!valueModifers.ContainsKey(modifer.Name))
        {
            throw new KeyNotFoundException($"modifer {modifer.Name} wasn't found in {this.Name}");
        }
        else
        {
            valueModifers.Remove(modifer.Name);
            updateValues = true;
        }
    }

    public void ApplyInstatntModifer(int value){
        //maybe want to just make it re apply modifers after this?
        baseValue = Mathf.Clamp(baseValue + value, minValue, maxValue);
        baseValue = Mathf.Clamp(current + value, minValue, maxValue);
    }

    private void ApplyBaseModifers(){
        //todo
        
    }
    private void ApplyMaxModifers(){
        //todo
    }
}

