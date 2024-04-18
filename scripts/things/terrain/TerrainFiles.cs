namespace Atomation.Things;

using Atomation.Resources;
using Godot;


/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a Terrain
/// </summary>
public class TerrainDef : CompThingDetails
{
    public TerrainDef() { }
    public TerrainDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData)
    {
    }

}


