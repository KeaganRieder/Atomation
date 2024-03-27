using System;
using Godot;
using Atomation.Thing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Atomation.Resources
{
    /// <summary>
    /// class which is used by the FileManger to load def files 
    /// and cached them to be reference through games runtime
    /// </summary>
    public static class DefDatabase
    {
        private static DefFile<TerrainDef> TerrainDefs;
        private static DefFile<Biome> BiomeDefs;

        /// <summary>
        /// Loads resources and def files form files
        /// </summary>
        public static void LoadResources()
        {             
            GD.Print("Loading Terrain Def Files");
            TerrainDefs = new DefFile<TerrainDef>(FilePath.TERRAIN_FOLDER);

            GD.Print("Loading Biome Def Files");
            BiomeDefs = new DefFile<Biome>(FilePath.BIOME_FOLDER);         
        }

        /// <summary>
        /// access cached terrain config data, and returns the terrain
        /// based on the ID
        /// </summary>
        public static CompThingDef GetTerrainConfig(string terrainID)
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
            
            //FileContents
            return null;
        }
    }
}