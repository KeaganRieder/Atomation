using System.Collections.Generic;
using Godot;

/// <summary>
/// maybe?
/// </summary>
public partial class StatSheet : Resource
{
    //needs work maybe make a statSheet class which handles 
    //interactions/interaction witha nd between stats

    private Dictionary<string, StatBase> stats; 

    public StatSheet(){
        stats = new Dictionary<string, StatBase>();
    }

    public StatBase GetStat(string statName){
        if (stats.TryGetValue(statName, out var stat))
        {
            return stat;
        }
        else{
            throw new KeyNotFoundException($"Stat {statName} doesn't exsit");
        }
    }
    

    // public void Apply Stat
    //todo add more functionallity to this class like it handling applying modifers and
    //other interaction between differnt stat and stuff like that?
}