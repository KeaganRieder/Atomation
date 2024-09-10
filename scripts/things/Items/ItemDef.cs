namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.GameMap;
using Atomation.Resources;
using Atomation.StatSystem;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// class used to define initial value of a thing in teh game. this also acts as a way 
/// or formatting def file to be read,cached and used to create new thing during play time
/// </summary>
public class ItemDef : ThingDef
{
    protected int stackLimit;

    public ItemDef() { }
    public ItemDef(string name, string description, StatSheet statSheet, GraphicData graphicData, int gridLayer = GameLayers.Items)
    : base(name, description, statSheet, graphicData, gridLayer) { }

    public static ItemDef Undefined()
    {
        return new ItemDef("Undefined Item", " ",
                    new StatSheet(new Dictionary<string, StatBase> { }, new Dictionary<string, StatModifierBase> { }),
                    new GraphicData()
                    {
                        TexturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                        Variants = 1,
                        Color = Colors.Purple,
                        GraphicSize = new Vector2I(Map.CELL_SIZE / 2, Map.CELL_SIZE / 2)
                    });
    }

    [JsonProperty]
    public int StackLimit { get => stackLimit; set => stackLimit = value; }

    public override string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "Undefined Item";
        }

        return defName;
    }
}