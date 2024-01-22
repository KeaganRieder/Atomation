using System.Collections.Generic;
using Godot;

/// <summary>
/// defines what a terrain object is in the game world 
/// </summary>
public partial class TerrainNew : CompThingNew
{
    public override Graphic Graphic{get => graphic; set{graphic = value;}} 

    public TerrainNew(){
        name = "Default";
        description = "";
    }
    public TerrainNew(string name, string description, Dictionary<string, StatBase> stats, Graphic graphic){
        this.name = name;
        this.description = description;
        this.stats = stats;
        this.graphic = graphic;
    }

    //todo add geters and seters for stats all Terrain have
}