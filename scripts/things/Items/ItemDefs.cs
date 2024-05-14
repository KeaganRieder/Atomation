namespace Atomation.Things;

using Map;
using Resources;
using Godot;
using System.Collections.Generic;

/// <summary>
/// collection of functions which can be used to write a def file contain
/// defined terrain defs
/// </summary>
public static class ItemDefs
{
    public static void FormatResourceItemDefs()
    {
        Dictionary<string, ItemDef> resourceItems = new Dictionary<string, ItemDef>
        {
            {"Stone", new ItemDef{
                defName = "Stone", 
                description = "It's Stony",
                stackLimit = 64,
                graphicData = new GraphicData{
                    texturePath = "item/resource/stone",
                    variants = 1,
                    color = Colors.LightGray, 
                    graphicSize = new Vector2I(8,8),
                }, 
                statSheet = new StatSheet(new Dictionary<string, StatBase>{
                    
                }, 
                new Dictionary<string, StatModifierBase>{

                }),
            }
            }
        };

        DefFile<ItemDef> resourceDefs = new DefFile<ItemDef>(resourceItems, FilePaths.ITEM_FOLDER, "resource_items");
    }
}
