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
    public Terrain(string name, string description, Dictionary<string,StatBaseOld> stats, Graphic graphic)
        : base(name, description, stats, graphic){
            AddChild(graphic.GetTexture());
        }
  
}




