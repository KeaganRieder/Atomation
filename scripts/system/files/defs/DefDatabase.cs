namespace Atomation.Resources;

using Godot;
using Atomation.Things;
using Newtonsoft.Json;

/// <summary>
/// class which is used by the FileManger to load def files 
/// and cached them to be reference through games runtime
/// </summary>
public static class DefDatabase
{
    private static DefFile<TerrainDef> TerrainDefs;
    private static DefFile<StructureDef> StructureDefs;
    private static DefFile<Biome> BiomeDefs;

    /// <summary>
    /// Loads resources and def files form files
    /// </summary>
    public static void LoadResources()
    {
        GD.Print("Loading Terrain Def Files");
        TerrainDefs = new DefFile<TerrainDef>(FilePaths.TERRAIN_FOLDER);
        GD.Print("Loading Biome Def Files");
        BiomeDefs = new DefFile<Biome>(FilePaths.BIOME_FOLDER);
        GD.Print("Loading Structure Def Files");
        StructureDefs = new DefFile<StructureDef>(FilePaths.STRUCTURE_FOLDER);
    }

    /// <summary>
    /// access cached terrain config data, and returns the terrain
    /// based on the ID
    /// </summary>
    public static TerrainDef GetTerrainDef(string terrainID)
    {
        return TerrainDefs[terrainID];
    }

    /// <summary>
    /// access cached terrain config data, and returns the terrain
    /// based on the moistureVal and temperateVal
    /// </summary>
    public static Biome GetBiome(float moisture, float temperate)
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

    public static StructureDef GetStructureDef(string StructureID)
    {
        return StructureDefs[StructureID];
    }
}
