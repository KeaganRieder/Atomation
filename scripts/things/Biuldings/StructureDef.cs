namespace Atomation.Things;

using System.Collections.Generic;
using Map;
using Resources;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a Structure
/// </summary>
public class StructureDef : ThingDef
{
    [JsonConverter(typeof(StringEnumConverter))]
    public SupportType supportReq;
    public Dictionary<string,int> buildCost;

    [JsonConstructor]
    public StructureDef() { }
    public StructureDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }

    public static StructureDef Undefined()
    {
        return new StructureDef("Undefine Structure", "",
             new StatSheet(new Dictionary<string, StatBase> { },

             new Dictionary<string, StatModifierBase> { }),
             new GraphicData()
             {
                 texturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                 variants = 1,
                 color = Colors.Purple,
                 graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
             })
        { supportReq = SupportType.Undefined};
    }
}

