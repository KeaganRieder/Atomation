namespace Atomation.Resources;

using GameMap;
using StatSystem;
using Things;
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
                DefName = "Stone",
                Description = "It's Stony",
                StackLimit = 64,
                GridLayer = GameLayers.Items,
                GraphicData = new GraphicData{
                    TexturePath = "item/resource/stone",
                    Variants = 1,
                    Color = Colors.Black,
                    GraphicSize = new Vector2I(8,8),
                },
                StatSheet = new StatSheet(new Dictionary<string, StatBase>{

                },
                new Dictionary<string, StatModifierBase>{

                }),
            }
            }
        };

        DefFile<ItemDef> resourceDefs = new DefFile<ItemDef>(resourceItems, FilePaths.ITEM_FOLDER, "resource_items");
    }
}
