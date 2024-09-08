namespace Atomation.Things;

using System.Collections.Generic;
using GameMap;
using Resources;
using StatSystem;
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
    private SupportType supportReq;
    private Dictionary<string, int> buildCost;

    [JsonConstructor]
    public StructureDef() { }
    public StructureDef(string name, string description, StatSheet statSheet, GraphicData graphicData, int gridLayer = GameLayers.Structure)
    : base(name, description, statSheet, graphicData, gridLayer) { }

    [JsonProperty(Order = 2),JsonConverter(typeof(StringEnumConverter))]
    public SupportType SupportReq { get => supportReq; set => supportReq = value; }
    
    [JsonProperty(Order = 2)]
    public Dictionary<string, int> BuildCost { get => buildCost; set => buildCost = value; }

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
                 graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
        { SupportReq = SupportType.Undefined};
    }
}

