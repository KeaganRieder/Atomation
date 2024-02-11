using System.Collections.Generic;
using Godot;
using Atomation.Utility;
using Atomation.Thing;

namespace Atomation.Map
{
    /// <summary>
    /// class which is used to pass a collection of data that relates to the 
    /// configuration of world world generation.
    /// this data is passed between the many world generation classes
    /// </summary>
    public class GenConfigs
    {
        //general configs
        /// <summary>
        /// bounderys/max size of the world
        /// x = width
        /// y = height
        /// </summary>
        public Vector2I worldBounds;

        //generation configs    
        public NoiseMapConfig elevationMapConfigs;
        public NoiseMapConfig moistureMapConfigs;
        public NoiseMapConfig heatMapConfigs;

        //terrain configs
        public float seaLevel = .2f;
        public float mounatinSize = .8f; //no mountains is anything above 1

    }

    /// <summary>
    /// the games world generators, which manange and oversees
    /// the running/executaion of genesteps to genertate the game
    /// world or chunks with in it
    /// </summary>
    public class WorldGenerator
    {
        //configs
        private GenConfigs genConfig;
        // genSteps
        private GenStepNoise genStepNoise;

        public WorldGenerator(GenConfigs genConfig)
        {
            this.genConfig = genConfig;
            genStepNoise = new GenStepNoise(genConfig);
        }

        //getters and setters
        public GenConfigs GenConfig { get { return genConfig; } set { genConfig = value; } }
        public GenStepNoise GenStepNoise { get { return GenStepNoise; } set { GenStepNoise = value; } }

        public void ExecuteGenSteps(Vector2 origin, Node2D parent)
        {

            // generationData = 
        }

        public Chunk GenerateChunk(Vector2 chunkPos, Chunk chunk)
        {
            // Dictionary<Vector2, Terrain> generatedTerrain = genStepNoise.RunStep(chunkPos, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
            genStepNoise.RunStep(chunkPos, chunk);
            // for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            // {
            //     for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            //     {
            //         Vector2 cords = new Vector2(x, y);
            //         Terrain terrain = generatedTerrain[cords];

            //         // terrain.Display(TerrainDispalyMode.Heat);

            //         chunk.ChunkTerrain(cords, terrain);
            //     }
            // }

            return chunk;
        }
    }
}