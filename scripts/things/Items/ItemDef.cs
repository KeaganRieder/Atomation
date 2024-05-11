namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.Map;
using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

public class ItemDef : ThingDef
{
    public int stackLimit;
    public ItemDef() { }
    public ItemDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }

    public static ItemDef Undefined()
    {
        return new ItemDef("Undefined Item", " ",
                      new StatSheet(new Dictionary<string, StatBase> { }, new Dictionary<string, StatModifierBase> { }),
                      new GraphicData()
                      {
                          texturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                          variants = 1,
                          color = Colors.Purple,
                          graphicSize = new Vector2I(MapData.CELL_SIZE / 2, MapData.CELL_SIZE / 2)
                      });
    }

    public override string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "Undefined Item";
        }

        return defName;
    }
}