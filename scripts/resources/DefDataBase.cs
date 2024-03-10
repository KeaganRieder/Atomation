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
        private static DefFile<BiomeDef> BiomeDefs;

        /// <summary>
        /// Loads resources and def files form files
        /// </summary>
        public static void LoadResources()
        {
             
            GD.Print("Loading Terrain Def Files");
            TerrainDefs = new DefFile<TerrainDef>(FilePath.TERRAIN_FOLDER);
            GD.Print("Loading Biome Def Files");
            BiomeDefs = new DefFile<BiomeDef>(FilePath.BIOME_FOLDER);           
        }

        /// <summary>
        /// access cached terrain config data, and returns the terrain
        /// based on the ID
        /// </summary>
        public static TerrainDef GetTerrainConfig(string terrainID)
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
                //converting the key to a vector that stores
                // a biomes temperature (x) and it's moisture (y) requirements
                BiomeDef.BiomeLabel biomeRequirements = JsonConvert.DeserializeObject<BiomeDef.BiomeLabel>(biome.Key);
                bool temperatureReqMet = temperate > biomeRequirements.minTemperature && temperate < biomeRequirements.maxTemperature;
                bool moistureReqMet =true;// moisture > biomeRequirements.minMoisture && moisture < biomeRequirements.maxMoisture;

                if (temperatureReqMet && moistureReqMet)
                {
                    //todo make moisture requirements

                    return new Biome(biome.Value);
                }
            }

            //FileContents
            return null;
        }
    }
}