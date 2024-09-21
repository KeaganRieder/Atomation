namespace Atomation.Resources;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// loads thing def files, caching them to be reference through games runtime
/// </summary>
public class ThingDefDatabase
{
    private static ThingDefDatabase instance;
    public static ThingDefDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ThingDefDatabase();
            }
            return instance;
        }
    }

    private bool loaded;
    private DefFile terrainDefs;
    private DefFile structureDefs;
    private DefFile plantDefs;
    private DefFile itemDefs;

    private ThingDefDatabase()
    {
        loaded = false;
    }

    public void LoadDefs()
    {
        if (!loaded)
        {
            terrainDefs = new DefFile(FilePaths.TERRAIN_FOLDER);
            structureDefs = new DefFile(FilePaths.STRUCTURE_FOLDER);
            plantDefs = new DefFile(FilePaths.PLANT_FOLDER);
            itemDefs = new DefFile(FilePaths.ITEM_FOLDER);
            loaded = true;
        }
    }

    /// <summary>
    /// gets terrain def stored under id from cached defs
    /// </summary>
    public Dictionary<string, object> GetTerrainDef(string id)
    {
        Dictionary<string, object> def = terrainDefs[id];
        if (def != null || def != default)
        {
            return def;
        }
        else
        {
            GD.PushWarning($"{id} isn't defined, setting to defaults");
            return default;
        }
    }

    /// <summary>
    /// gets structure def stored under id from cached defs
    /// </summary>
    public Dictionary<string, object> GetStructureDef(string id)
    {
        Dictionary<string, object> def = structureDefs[id];
        if (def != null || def != default)
        {
            return def;
        }
        else
        {
            GD.PushWarning($"{id} isn't defined, setting to defaults");
            return default;
        }
    }

    /// <summary>
    /// gets plant def stored under id from cached defs
    /// </summary>
    public Dictionary<string, object> GetPlantDef(string id)
    {
        Dictionary<string, object> def = plantDefs[id];
        if (def != null || def != default)
        {
            return def;
        }
        else
        {
            GD.PushWarning($"{id} isn't defined, setting to defaults");
            return default;
        }
    }
    /// <summary>
    /// gets structure def stored under id from cached defs
    /// </summary>
    public Dictionary<string, object> GetItemDef(string id)
    {
        Dictionary<string, object> def = itemDefs[id];
        if (def != null || def != default)
        {
            return def;
        }
        else
        {
            GD.PushWarning($"{id} isn't defined, setting to defaults");
            return default;
        }
    }


}