using System.Collections.Generic;
using Godot;

namespace Atomation.Map
{

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
            genStepTerrain = new GenStepTerrain(genConfig);
        }

        /// <summary>
        /// used during game load, inOrder to know the preset settings/
        /// finalized configuration for world gen
        /// </summary>
        public void ReadConfigs(){
            //todo
        }

        //getters and setters
        public GenConfigs GenConfig { get { return genConfig; } set { genConfig = value; } }
        public GenStepNoise GenStepNoise { get { return GenStepNoise; } set { GenStepNoise = value; } }

        /// <summary>
        /// Used to Generate new Chunks
        /// </summary>
        public void GenerateChunk(Vector2 ChunkCord, ChunkHandler chunkHandler)
        {           
            genStepNoise.RunStep(ChunkCord, chunkHandler);
            genStepTerrain.RunStep(ChunkCord, chunkHandler);
        }
    }
}