using Godot;
using System;
/*
	expands upon StatBase to allow for more stat modifers to
    be define
    these are 
    todo make modifer types and look into improveing
*/
public partial class StatModifer : StatBase
{
    public int Order{get; set;} 

    public StatModifer(string name, string description, float baseValue, int order)
         : base(name, description, baseValue, 0 ,0){
        Count = 1;
        Order = order;
    }
     public StatModifer(StatDef def) : base(def){
        Count = 1;
        Order = 0;//todo
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
