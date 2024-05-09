namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.Map;
using Atomation.Resources;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// data class used in formatting terrain def files that once written to a json
/// can be read in order to create new instances of terrain based on values defined
/// in the def file
/// </summary>
public class TerrainDef : ThingDef
{
    public bool collidable { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public SupportType supportProvided { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public SupportType supportReq { get; set; }

    [JsonConstructor]
    public TerrainDef() { }
    public TerrainDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }

    public static TerrainDef Undefined()
    {
        return new TerrainDef("Undefined Terrain", " ",
                      new StatSheet(new Dictionary<string, StatBase> { }, new Dictionary<string, StatModifierBase> { }),
                      new GraphicData()
                      {
                          texturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                          variants = 1,
                          color = Colors.Purple,
                          graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
                      });
    }

    public override string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "Undefined Terrain";
        }

        return defName;
    }

}
