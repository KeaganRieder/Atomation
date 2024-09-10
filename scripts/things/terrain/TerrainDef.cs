namespace Atomation.Things;

using System.Collections.Generic;
using GameMap;
using Resources;
using StatSystem;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

/// <summary>
/// def file used to define aspects of terrain 
/// </summary>
public class TerrainDef : ThingDef
{
    private bool collidable;

    private SupportType supportProvided;
    private SupportType supportReq;

    [JsonConstructor]
    public TerrainDef() { }
    public TerrainDef(string name, string description, StatSheet statSheet, GraphicData graphicData, int gridLayer = GameLayers.Terrain)
    : base(name, description, statSheet, graphicData, gridLayer) { }

    public static TerrainDef Undefined
    {
        get
        {
            return new TerrainDef("Undefined Terrain", " ",
                      new StatSheet(new Dictionary<string, StatBase> { }, new Dictionary<string, StatModifierBase> { }),
                      new GraphicData()
                      {
                          TexturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                          Variants = 1,
                          Color = Colors.Purple,
                          GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                      });
        }
    }

    [JsonProperty(Order = 2)]
    public bool Collidable { get => collidable; set => collidable = value; }

    [JsonProperty(Order = 2), JsonConverter(typeof(StringEnumConverter))]
    public SupportType SupportProvided { get => supportProvided; set => supportProvided = value; }
    [JsonProperty(Order = 2), JsonConverter(typeof(StringEnumConverter))]
    public SupportType SupportReq { get => supportReq; set => supportReq = value; }

    public override string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "Undefined Terrain";
        }

        return defName;
    }
}