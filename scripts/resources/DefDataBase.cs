using System;
using Godot;
using Atomation.Thing;
using System.Collections.Generic;

namespace Atomation.Resources
{
    /// <summary>
    /// class which is used by the FileManger to load def files 
    /// and cached them to be reference through games runtime
    /// </summary>
    public static class DefDatabase
    {
        public const string TERRAIN_DEFS_PATH = "data/core/defs/terrain/";
        public const string BIOME_DEFS_PATH = "data/core/defs/biomes/";

        public static DefFile<TerrainDef> TerrainDefs;
        public static DefFile<BiomeDef> BiomeDefs;  

        /// <summary>
        /// Loads resources and def files form files
        /// </summary>
        public static void LoadResources()
        {
            GD.Print("Loading Terrain Def Files");
            TerrainDefs = new DefFile<TerrainDef>(TERRAIN_DEFS_PATH);

            GD.Print("Loading Biome Def Files");
            BiomeDefs = new DefFile<BiomeDef>(BIOME_DEFS_PATH);
        }

        /// <summary>
        /// access cached terrain config data, and returns the terrain
        /// based on the ID
        /// </summary>
        public static TerrainDef ReadTerrainConfig(string terrainID)
        {
            return TerrainDefs[terrainID];
        }
        /// <summary>
        /// access cached terrain config data, and returns the terrain
        /// based on the moistureVal and temperateVal
        /// </summary>
        public static Biome ReadBiome(float moistureVal, float temperateVal)
        {
            foreach (BiomeDef biomeDef in BiomeDefs.Defs.Values)
            {
                if (biomeDef.Suitable(temperateVal, moistureVal))
                {
                    return new Biome(biomeDef);
                }
            }

            // GD.Print("No Biome Found");
            return null;
        }
    }
}