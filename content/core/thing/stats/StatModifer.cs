using Godot;
using System;

/// <summary>
/// a stat which is a used to a perstnat modifers onto a compStat
/// </summary>
public partial class StatModifer : StatBase
{
    public StatModifer(string name, string description, float baseValue, int order)
         : base(name, description, baseValue){
        Count = 1;
        Order = order;
    }

    public int Count{
        get => Count;
        set{
            Count = Math.Max(0, value);            
        }
    }

    public override float Value{
        get => baseValue * Count;       
    }
}
