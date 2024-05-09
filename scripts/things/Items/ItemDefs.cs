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
            
        };

        DefFile<ItemDef> resourceDefs = new DefFile<ItemDef>(resourceItems, FilePaths.ITEM_FOLDER, "resource_items");
    }
}
