using Godot;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

/// <summary>
/// stat which can be modifed through persiant modifed and single/instant
/// modifers (eg. dmg or a potion)
/// todo
/// </summary>
public partial class CompStat : StatBaseOld
{
    public CompStat(string name, string description, float baseValue, float minValue, float maxValue)
        : base(name, description, baseValue){
        modifers = new Dictionary<string, StatModiferOld>();
        updateStat = false;
        currentVal = baseValue;
        this.minValue = minValue;
        this.maxValue = maxValue;
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
        foreach (StatModiferOld modifer in modifers.Values)
        {
            if (currentVal <= minValue || currentVal >= maxValue)
            {
                break;
            }

            Math.Clamp(currentVal += modifer.Value, minValue, maxValue);
        }
    }
    public void AddModifer(StatModiferOld mod){
        updateStat = true;
        if (modifers.ContainsKey(mod.Name))
        {
            modifers[mod.Name].Count += mod.Count; 
        }  
        else{
            modifers.Add(mod.Name, mod);
        }
        
    }
    public void RemoveModifer(StatModiferOld mod){
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