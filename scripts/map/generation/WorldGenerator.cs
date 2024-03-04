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
        private MapGenSettings genConfig;
        // genSteps
        private GenStepNoise genStepNoise;
        private GenStepTerrain genStepTerrain;

        public WorldGenerator(MapGenSettings genConfig)
        {
            this.genConfig = genConfig;
            genStepNoise = new GenStepNoise(genConfig);
            genStepTerrain = new GenStepTerrain(genConfig);
        }

        //getters and setters
        public MapGenSettings GenConfig { get { return genConfig; } set { genConfig = value; } }
        public GenStepNoise GenStepNoise { get { return GenStepNoise; } set { GenStepNoise = value; } }

        /// <summary>
        /// Used to Generate new Chunks
        /// </summary>
        public void GenerateChunk(Vector2 ChunkCord, ChunkHandler chunkHandler)
        {           
            genStepNoise.RunStep(ChunkCord, chunkHandler);
            genStepTerrain.RunStep(ChunkCord, chunkHandler);
            chunkHandler.UpdateVisualizationMode(default);
        }
    }
}