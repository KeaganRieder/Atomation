using System;
using System.Collections.Generic;
using Godot;
using Atomation.Map;
using Atomation.Thing;
using System.ComponentModel.DataAnnotations;

namespace Atomation.Map
{
    /// <summary>
    /// defines the generation step for generating noise maps, 
    /// which are pass to generation steps following, and help determine
    /// aspects in the game world
    /// </summary>
    public class GenStepNoise : GenStep
    {   
        //debugging
        private float min = 1000;
        private float max = -100;
        //general info
        private int equatorHeight;

        private float waterLevel;

        //noise map info
        private SimplexNoiseMap elevationMap;
        private SimplexNoiseMap moistureMap;
        private SimplexNoiseMap heatMap;

        // private Dictionary<Vector2, Terrain> terrainTiles;

        public GenStepNoise(GenConfigs genConfig)
        {
            worldMaxWidth = genConfig.worldBounds.X;
            worldMaxHeight = genConfig.worldBounds.Y;
            equatorHeight = genConfig.worldBounds.Y/2;

            elevationMap = new SimplexNoiseMap(genConfig.elevationMapConfigs);
            moistureMap = new SimplexNoiseMap(genConfig.moistureMapConfigs);
            heatMap = new SimplexNoiseMap(genConfig.heatMapConfigs);
        }

        /// <summary>
        /// used to get the elevation noise map in order to change
        /// individual config Data
        /// </summary>
        public SimplexNoiseMap GetHeightMap()
        {
            return elevationMap;
        }
        /// <summary>
        /// used to get the moisture noise map in order to change
        /// individual config Data
        /// </summary>
        public SimplexNoiseMap GetMoistureMap()
        {
            return moistureMap;
        }
        /// <summary>
        /// used to get the heat noise map in order to change
        /// individual config Data
        /// </summary>
        public SimplexNoiseMap GetHeatMap()
        {
            return heatMap;
        }

        /// <summary>
        /// handling running the step, using width and height that's based on
        /// chunk info
        /// </summary>
        public override void RunStep(Vector2 origin, ChunkHandler chunkHandler)
        {
            float[,] equatorHeat = GenerateEquatorHeat(origin, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);

            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
                {                    
                    SampleCords(origin, x, y, out float sampleX, out float sampleY);

                    float height = GetElevationValue(sampleX, sampleY)*10;

                    Vector2 cords = new(x, y);

                    Terrain terrain = new(cords);
                    terrain.HeightValue = height ;
                    terrain.HeatValue = GetHeatValue(x, y, equatorHeat,heatMap[sampleX, sampleY], height);
                    terrain.MoistureValue = GetMoistureValue(sampleX, sampleY);
                    
                    SampleChunkPos(origin, x, y, out sampleX, out sampleY);
                    chunkHandler.Set(Mathf.RoundToInt(sampleX), Mathf.RoundToInt(sampleY), terrain);
                }
            }
            // GD.Print($"MIN:{min}, MAX: {max}");
        }

        //
        //generating heat map
        //   

        /// <summary>
        /// generating elevation map which outlines the primary parts of the terrain
        /// this generates values generally between -.6 and .5
        /// </summary>
        private float GetElevationValue(float x, float y)
        {
            float heightVal = elevationMap[x, y];

            return Mathf.Clamp(heightVal, -1, 1);
        }

        //
        //generating heat map
        //    

        /// <summary>
        /// handles generating the equator heat map
        /// which decrease in heat the further away form teh center point
        /// it gets
        /// </summary>
        private float[,] GenerateEquatorHeat(Vector2 origin, int width, int height)
        {
            float[,] noiseMap = new float[width, height];

            // decide the temperature based on distance from central point/equator
            for (int y = 0; y < height; y++)
            {
                float sampleY = y + origin.Y;

                //calculate noise value based on it's distance
                // well also ensuring that it's within the bounds
                float noise = Math.Abs(sampleY - equatorHeight) / worldMaxHeight;

                for (int x = 0; x < width; x++)
                {
                    //apply the noise value for all points at this Row
                    noiseMap[x, y] = Mathf.Clamp(noise, 0, 1);
                }
            }
            return noiseMap;
        }

        /// <summary>
        /// gets the heat value for given cords. heat is based on distance form Equator,
        /// heat map value, and the height
        /// </summary>
        private float GetHeatValue(int x, int y, float[,] equatorHeat, float heat, float height)
        {
            float heatVal = equatorHeat[x, y] * heat;
        
            // heatVal += Mathf.Sin(Mathf.Abs(height)) * height;
            // if (heatVal > max)
            // {
            //     max = heatVal;
            // }
            // if (heatVal < min)
            // {
            //     min = heatVal;
            // }
            return Mathf.Clamp(heatVal, -1, 1f);
        }

        /// <summary>
        /// get moisture value at given cords
        /// </summary>
        private float GetMoistureValue(float x, float y)
        {   
            // with current settings generates values between 0 to .7
            float moistureVal = Mathf.Abs(moistureMap[x,y]);

            // moisture += Mathf.Sin(Mathf.Abs(height)) * height; 
            moistureVal = Mathf.Clamp(moistureVal, 0, 1);
       
            return moistureVal;
;
        }
    }
}