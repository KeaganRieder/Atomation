using Godot;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
/*
	expands upon StatBase to allow for more compelx stats
    to be defien, with min/max values and the abilty to
    have modifers applied
    these modifers will only apply to more complex things
*/
public partial class CompStat : StatBase
{
    private float currentVal;
    private bool updateStat;
    private Dictionary<string, StatModifer> modifers; // only for prolonged modifers

    public CompStat(string name, string description, float baseValue, float minValue, float maxValue)
        : base(name, description, baseValue, minValue, maxValue){
        modifers = new Dictionary<string, StatModifer>();
        updateStat = false;
        currentVal = baseValue;
    }
     public CompStat(StatDef def) 
        : base(def){
        modifers = new Dictionary<string, StatModifer>();
        updateStat = false;
        currentVal = def.baseValue;
    }

    public override float Value{
        get {
            if (updateStat)
            {
                ApplyModifers();
                return currentVal;
            }
            return currentVal;
        }
        //used to apply "none modifer class" dmg 
        set {baseValue = Math.Clamp(value, minValue, maxValue);} 
    }

    public void ApplyModifers(){
        updateStat = false;
        currentVal = baseValue;
        foreach (StatModifer modifer in modifers.Values)
        {
            if (currentVal <= minValue || currentVal >= maxValue)
            {
                break;
            }

            Math.Clamp(currentVal += modifer.Value, minValue, maxValue);
        }
    }
    public void AddModifer(StatModifer mod){
        updateStat = true;
        if (modifers.ContainsKey(mod.Name))
        {
            modifers[mod.Name].Count += mod.Count; 
        }  
        else{
            modifers.Add(mod.Name, mod);
        }
        
    }
    public void RemoveModifer(StatModifer mod){
        try
        {
            if (modifers.ContainsKey(mod.Name))
            {
                updateStat = true;
                var existingMod = modifers[mod.Name];
                existingMod.Count -= mod.Count; 
                if (existingMod.Count < 0)
                {
                    modifers.Remove(mod.Name);
                }
            }  
            else{
                throw new ArgumentException($"Modifier with name {mod.Name} not found.");
            }
        }
        catch (Exception error)
        {
            GD.PrintErr("Error writing file: " + error.Message);
        }
    }
}