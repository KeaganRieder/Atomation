using System;
using Godot;
using Atomation.Thing;

namespace Atomation.Resources
{
    /// <summary>
    /// class which is used by the FileManger to load def files 
    /// and cached them to be reference through games runtime
    /// </summary>
    public static class DefResources
    {
        public const string TERRAIN_DEFS_PATH = "data/core/defs/terrain/";
        public const string BIOME_DEFS_PATH = "data/core/defs/biomes/";

        public static DefDatabase<TerrainDef> TerrainDefs;
        public static DefDatabase<Biome> BiomeDefs; //this is a todo still

        public static void LoadResources()
        {
            GD.Print("loading Terrain Files");
            TerrainDefs = new DefDatabase<TerrainDef>(TERRAIN_DEFS_PATH);
            GD.Print("loading Biome Files");
            BiomeDefs = new DefDatabase<Biome>(BIOME_DEFS_PATH);
        }

        public static TerrainDef ReadTerrainConfig(string terrainID)
        {
            return TerrainDefs[terrainID];
        }
        public static Biome ReadBiome(string biomeID)
        {
            return BiomeDefs[biomeID];
        }
        // public static TerrainDef ReadTerrainDef(string terrainID)
        // {
        //     return TerrainDefs[terrainID];
        //     // return default;
        // }
        // public static BiomeDef ReadBiomeDef(string biomeID)
        // {
        //     // return BiomeDefs[biomeID];
        //     return default;
        // }
    }
}