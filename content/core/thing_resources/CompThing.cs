using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// compThing defines the foundtaions of all complex obejct that 
/// appear in the game world. 
/// </summary>
public abstract partial class CompThingNew : ThingNew
{
    // protected Node2D objNode;
    [JsonProperty("graphic data")]
    protected Graphic graphic;
    [JsonProperty("stats")]
    protected Dictionary<string, StatBase> stats; //this needs work

    // public virtual Node2D ObjNode{get => objNode; set{objNode = value;}}
    public virtual Graphic Graphic{get => graphic; set{graphic = value;}}
    // public virtual StatBase StatSheet{get => stats; set{graphic = value;}}

}