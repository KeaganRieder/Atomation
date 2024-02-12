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
    public class GenStepNoise 
    {

        //general info
        private int worldMaxWidth; //maybe?
        private int worldMaxHeight;  //maybe?
        // private float seaLevel; //no sea = 
        // private float mountainSize; // no mountains = 1
        private int equatorHeight;
        private int maxFromEquator;

        //noise map info
        private SimplexNoiseMap elevationMap;
        private SimplexNoiseMap moistureMap;
        private SimplexNoiseMap heatMap;

        // private Dictionary<Vector2, Terrain> terrainTiles;

        public GenStepNoise(GenConfigs genConfig)
        {
            worldMaxWidth = genConfig.worldBounds.X;
            worldMaxHeight = genConfig.worldBounds.Y;

            equatorHeight = 0;
            maxFromEquator = worldMaxHeight;

            // seaLevel = genConfig.seaLevel;
            // mountainSize = genConfig.mountainSize;

            elevationMap = new SimplexNoiseMap(genConfig.elevationMapConfigs);
            moistureMap = new SimplexNoiseMap(genConfig.moistureMapConfigs);
            heatMap = new SimplexNoiseMap(genConfig.heatMapConfigs);
        }

        /// <summary>
        /// used to get the elevation noise map in order to change
        /// individual config Data
        /// </summary>
        public SimplexNoiseMap GetElevationMap()
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
        public void RunStep(Vector2 origin, Chunk chunk)
        {
            // terrainTiles = new Dictionary<Vector2, Terrain>();

            float[,] equatorHeat = GenerateEquatorHeat(origin, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);

            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
                {
                    Vector2 cords = new Vector2(x, y);
                    Terrain terrain = new Terrain(cords);

                    float sampleX = x + origin.X;
                    float sampleY = y + origin.Y;

                    terrain.HeightValue = GetElevationValue(sampleX, sampleY);
                    terrain.HeatValue = GetHeatValue(origin, x, y, equatorHeat);
                    terrain.MoistureValue = GetMoistureValue(sampleX, sampleY);

                    chunk.Terrain(cords, terrain);
                }
            }

            // GD.Print($"MIN:{min} MAX:{max}");
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
            float elevation = elevationMap[x, y];
            // if (elevation < min)
            // {
            //     min = elevation;
            // }
            // if (elevation > max)
            // {
            //     max = elevation;
            // }

            return Mathf.Clamp(elevation, -1, 1);
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
                float noise = Math.Abs(sampleY - equatorHeight) / maxFromEquator;//need a figure out this

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
        private float GetHeatValue(Vector2 origin, int x, int y, float[,] equatorHeat)
        {
            float sampleX = x + origin.X;
            float sampleY = y + origin.Y;

            float height = MathF.Abs(elevationMap[sampleX, sampleY]);

            float heat = equatorHeat[x, y] * Mathf.Abs(heatMap[sampleX, sampleY] * 10);
            heat += Mathf.Sin(height) * height;
            
            return Mathf.Clamp(MathF.Abs(heat), 0, 1f);
        }

        /// <summary>
        /// get moisture value at given cords
        /// </summary>
        private float GetMoistureValue(float x, float y)
        {
            float height = MathF.Abs(elevationMap[x, y]);
            float moisture = Mathf.Abs(moistureMap[x, y]);
            moisture += Mathf.Sin(height);// * height;
            
            return Mathf.Clamp(moisture, 0, 1);
        }
    }
}