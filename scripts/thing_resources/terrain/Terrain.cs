using System.Collections.Generic;
using Godot;

/// <summary>
/// defines what a terrain object is in the game world 
/// </summary>
public partial class Terrain : CompThing
{
    public override Graphic Graphic{get => graphic; set{graphic = value;}} 

    public Terrain(){
        name = "Default";
        description = "";
    }
    public Terrain(string name, string description, Dictionary<string, StatBase> stats, Graphic graphic){
        this.name = name;
        this.description = description;
        this.stats = stats;
        this.graphic = graphic;
    }
    public Terrain(TerrainDef config){
        name = config.Name;
        description = config.Description;
        stats = config.CreateStats();
        Graphic = new Graphic(config.graphicData);
    }

    //todo add geters and seters for stats all Terrain have
}