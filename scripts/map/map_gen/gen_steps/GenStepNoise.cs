using System;
using System.Collections.Generic;
using Godot;
using Atomation.Map;
using Atomation.Thing;

namespace Atomation.Map
{
    /// <summary>
    /// defiens the generation step for generating noise maps, 
    /// which are pass to generation steps following, and help determine
    /// asspects in the game world
    /// </summary>
    public class GenStepNoise : GenStep
    {
        //general info
        private int worldMaxWidth; //maybe?
        private int worldMaxHeight;  //maybe?
        private float seaLevel; //no sea = 
        private float mountainSize; // no mounatins = 1
        private int equatorHeight;
        private int maxFromEquator;

        //noise map info
        private SimplexNoiseMap elevationMap;
        private SimplexNoiseMap moistureMap;
        private SimplexNoiseMap heatMap;

        private Dictionary<Vector2, Terrain> terrainTiles;

        public GenStepNoise(GenConfigs genConfig)
        {
            worldMaxWidth = genConfig.worldBounds.X;
            worldMaxHeight = genConfig.worldBounds.Y;

            equatorHeight = 0;
            maxFromEquator = worldMaxHeight;

            seaLevel = genConfig.seaLevel;
            mountainSize = genConfig.mounatinSize;

            elevationMap = new SimplexNoiseMap(genConfig.elevationMapConfigs);
            moistureMap = new SimplexNoiseMap(genConfig.moistureMapConfigs);
            heatMap = new SimplexNoiseMap(genConfig.heatMapConfigs);
        }

        /// <summary>
        /// used to get the elevation noise map inorder to change
        /// indvidual conifg Data
        /// </summary>
        public SimplexNoiseMap GetElevationMap()
        {
            return elevationMap;
        }
        /// <summary>
        /// used to get the moisture noise map inorder to change
        /// indvidual conifg Data
        /// </summary>
        public SimplexNoiseMap GetMoistureMap()
        {
            return moistureMap;
        }
        /// <summary>
        /// used to get the heat noise map inorder to change
        /// indvidual conifg Data
        /// </summary>
        public SimplexNoiseMap GetHeatMap()
        {
            return heatMap;
        }

        /// <summary>
        /// handling runing the step
        /// </summary>
        public Dictionary<Vector2, Terrain> RunStep(Vector2 origin, int width, int height)
        {
            terrainTiles = new Dictionary<Vector2, Terrain>();

            float[,] equatorHeat = GenerateEquaitorHeat(origin, width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2 cords = new Vector2(x, y);

                    Terrain terrain = new Terrain(cords);

                    float sampleX = x + origin.X;
                    float sampleY = y + origin.Y;

                    terrain.HeightValue = GetElevationValue(sampleX, sampleY);
                    terrain.HeatValue = GetHeatValue(origin, x, y, equatorHeat);
                    terrain.MoistureValue = GetMoistureValue(sampleX, sampleY);

                    terrainTiles.Add(new Vector2(x, y), terrain);
                }
            }

            return terrainTiles;
        }

        /// <summary>
        /// handling runing the step, using width and height that's based on
        /// chunk info
        /// </summary>
        public void RunStep(Vector2 origin, Chunk chunk)
        {
            // terrainTiles = new Dictionary<Vector2, Terrain>();

            float[,] equatorHeat = GenerateEquaitorHeat(origin, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);

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

                    chunk.ChunkTerrain(cords, terrain);
                }
            }
        }

        /// <summary>
        /// generateing elevation map which outlines the primary parts of the terrain
        /// </summary>
        private float GetElevationValue(float x, float y)
        {
            return Mathf.Clamp(Mathf.Abs(elevationMap[x, y]), -1, 1);
        }

        /// <summary>
        /// handles generating the equatir heat map
        /// which decrease in heat the furtehr away form teh center point
        /// it gets
        /// </summary>
        private float[,] GenerateEquaitorHeat(Vector2 origin, int width, int height)
        {
            float[,] noiseMap = new float[width, height];

            // dicide the tempeture based on distnace from central point/equator
            for (int y = 0; y < height; y++)
            {
                float sampleY = y + origin.Y;

                //calaculate noise value based on it's distnace
                // well also ensuring that it's within the bounds
                float noise = Math.Abs(sampleY - equatorHeight) / maxFromEquator;//need a figure out this

                for (int x = 0; x < width; x++)
                {
                    //apply the noise vallue for all points at this Row
                    noiseMap[x, y] = Mathf.Clamp(noise, 0, 1);
                }
            }
            return noiseMap;
        }

        /// <summary>
        /// gets the heat value for given cords. heat is based on didstance form Equaitor,
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