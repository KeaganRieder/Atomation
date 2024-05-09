namespace Atomation.Resources;

using Godot;
using Atomation.Things;
using Newtonsoft.Json;

/// <summary>
/// class which is used by the FileManger to load def files 
/// and cached them to be reference through games runtime
/// </summary>
public class DefDatabase
{
    private static DefDatabase instance;
    private static DefFile<TerrainDef> TerrainDefs;
    private static DefFile<StructureDef> StructureDefs;
    // private static DefFile<ItemDef> ItemDefs;
    private static DefFile<Biome> BiomeDefs;

    private DefDatabase()
    {
        GD.Print("Loading Terrain Def Files");
        TerrainDefs = new DefFile<TerrainDef>(FilePaths.TERRAIN_FOLDER);
        GD.Print("Loading Biome Def Files");
        BiomeDefs = new DefFile<Biome>(FilePaths.BIOME_FOLDER);
        GD.Print("Loading Structure Def Files");
        StructureDefs = new DefFile<StructureDef>(FilePaths.STRUCTURE_FOLDER);
        GD.Print("Loading Item Def Files");
        // ItemDefs = new DefFile<ItemDef>(FilePaths.ITEM_FOLDER);
    }

    public static DefDatabase GetInstance()
    {
        if (instance == null)
        {
            instance = new DefDatabase();
        }
        return instance;
    }

    /// <summary>
    /// access cached terrain config data, and returns the terrain
    /// based on the ID
    /// </summary>
    public TerrainDef GetTerrainDef(string terrainID)
    {
        if (terrainID != null && terrainID != "Undefined Terrain")
        {
            return TerrainDefs[terrainID];
        }
        return TerrainDef.Undefined();
    }

    /// <summary>
    /// access cached terrain config data, and returns the terrain
    /// based on the moistureVal and temperateVal
    /// </summary>
    public Biome GetBiome(float moisture, float temperate)
    {
        foreach (var biome in BiomeDefs.FileContents)
        {
            BiomeLabel biomeRequirements = JsonConvert.DeserializeObject<BiomeLabel>(biome.Key);
            bool temperatureReqMet = temperate > biomeRequirements.minTemperature && temperate < biomeRequirements.maxTemperature;
            bool moistureReqMet = true;// moisture > biomeRequirements.minMoisture && moisture < biomeRequirements.maxMoisture;
            if (temperatureReqMet && moistureReqMet)
            {
                biome.Value.Color.A = 1;
                return biome.Value;
            }
        }

        return null;
    }

    public StructureDef GetStructureDef(string StructureID)
    {
        if (StructureID != null && StructureID != "Undefine Structure")
        {
            return StructureDefs[StructureID];
        }
        return StructureDef.Undefined();
    }

    public ItemDef GetItemDef(string itemID)
    {
        if (itemID != null && itemID != "Undefine Item")
        {
            // return ItemDefs[itemID];
            return ItemDef.Undefined();

        }
        return ItemDef.Undefined();
    }
}
