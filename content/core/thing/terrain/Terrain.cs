using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// terrain are things like floor in atomation
/// </summary>
public partial class Terrain : CompThing
{
 
    public Terrain(): this("Default","Default"){}
    public Terrain(string name, string description) : base(name, description) {}
    public Terrain(string name, string description, Dictionary<string,StatBase> stats, Graphic graphic)
         : base(name, description, stats, graphic){}

    // public virtual Node2D NodeObj{
    //     get => NodeObj;
    //     set{
    //         NodeObj = value; 
    //     }
    // }
    public override void SetNode(Node2D nodeObj){
        node = nodeObj; 
        node.AddChild(graphic.GetTexture());
    }
  
}




