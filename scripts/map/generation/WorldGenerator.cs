using System.Collections.Generic;
using Godot;

namespace Atomation.Map
{
    /// <summary>
    /// the games world generators, which manage and oversees
    /// the running/execution of gensteps to generate the game
    /// world or chunks with in it
    /// </summary>
    public static class WorldGenerator
    {
        private static GenStepLandScape genStepLandScape;

        public static MapGenSettings GenConfig { get; set; }

        public static void Initialize(MapGenSettings configs){
            GenConfig = configs;
            genStepLandScape = new GenStepLandScape(GenConfig);
        }

        /// <summary>
        /// Used to Generate new Chunks
        /// </summary>
        public static void GenerateChunk(Vector2 ChunkCord, ChunkHandler chunkHandler)
        {
            genStepLandScape.RunStep(ChunkCord, chunkHandler);
        }
    }
}