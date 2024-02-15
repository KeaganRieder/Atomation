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
    /// the games world generators, which manage and oversees
    /// the running/execution of gensteps to generate the game
    /// world or chunks with in it
    /// </summary>
    public class WorldGenerator //maybe make this static?
    {
        //configs
        private GenConfigs genConfig;
        // genSteps
        private GenStepNoise genStepNoise;
        private GenStepTerrain genStepTerrain;

        public WorldGenerator(GenConfigs genConfig)
        {
            this.genConfig = genConfig;
            genStepNoise = new GenStepNoise(genConfig);
        }

        //getters and setters
        public GenConfigs GenConfig { get { return genConfig; } set { genConfig = value; } }
        public GenStepNoise GenStepNoise { get { return GenStepNoise; } set { GenStepNoise = value; } }

        /// <summary>
        /// Used to Generate new Chunks
        /// </summary>
        public void GenerateChunk(Vector2 ChunkCord, Chunk chunk)
        {           
            genStepNoise.RunStep(ChunkCord, chunk);
            // GenStep(Chunk chunk)

            // return null;
        }
    }
}