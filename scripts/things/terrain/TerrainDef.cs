namespace Atomation.Things;

using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// data class used in formatting terrain def files that once written to a json
/// can be read in order to create new instances of terrain based on values defined
/// in the def file
/// </summary>
public class TerrainDef : ThingDef
{
    [JsonProperty("Layerable")]
    public bool Layerable { get; set; }
    [JsonProperty("Collidable")]
    public bool Collidable { get; set; }

    public TerrainDef() { }
    public TerrainDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }

    public TerrainDef(string name, string parent, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData)
    {
        Parent = parent;
    }

    public override string GetKey()
    {
        if (Name == "" || Name == null)
        {
            Name = Parent;
        }

        return Name;
    }

}