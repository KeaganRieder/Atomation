using System.Collections.Generic;
using Godot;
using Atomation.Thing;
using System.Text;

namespace Atomation.Map
{
    /// <summary>
    /// ChunkHandler, stores and handles chunks, allow for 
    /// interaction with different element in a chunk
    /// </summary>
    public class ChunkHandler
    {
        private readonly int visibleChunks;

        private List<Chunk> lastUpdatedChunks;
        private Dictionary<Vector2, Chunk> chunkArray;
        private VisualizationMode tileVisual;

        private Node2D map;
        public WorldGenerator WorldGenerator { get; set; }

        public ChunkHandler(Node2D map)
        {
            this.map = map;

            tileVisual = VisualizationMode.Default;
            visibleChunks = Mathf.FloorToInt(MapSettings.MAX_LOAD_DIST / (Chunk.CHUNK_SIZE)) - 1;

            // GD.Print(visibleChunks);

            lastUpdatedChunks = new List<Chunk>();

            chunkArray = new Dictionary<Vector2, Chunk>();
        }

        /// <summary>
        /// gets the position of the chunk aligned on the tile grid
        /// </summary>
        private Vector2 GetChunkWorldPosition(Vector2 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.X * Chunk.CHUNK_SIZE * MapSettings.CELL_SIZE);
            int y = Mathf.RoundToInt(worldPosition.Y * Chunk.CHUNK_SIZE * MapSettings.CELL_SIZE);
            Vector2 cords = new Vector2(x, y);
            return cords;
        }

        /// <summary>
        /// gets the position of the chunk aligned on the tile grid
        /// </summary>
        private void GetChunkXY(Vector2 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt(worldPosition.X / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
            y = Mathf.FloorToInt(worldPosition.Y / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
        }

        /// <summary>
        /// Get the cords aligned to pixel grid of current chunk 
        /// </summary>
        private Vector2 GetCurrentChunk(Vector2 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.X / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
            int y = Mathf.RoundToInt(worldPosition.Y / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
            Vector2 cords = new Vector2(x, y);

            return cords;
        }

        /// <summary>
        /// gets chunks at given chunk cords 
        /// </summary>
        public Chunk GetChunk(int chunkX, int chunkY)
        {
            Vector2 chunkCords = new Vector2(chunkX, chunkY);

            if (chunkArray.TryGetValue(chunkCords, out var chunk))
            {
                return chunk;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// gets chunks at given chunk cords 
        /// </summary>
        public Chunk GetChunk(Vector2 worldPosition)
        {
            GetChunkXY(worldPosition, out int x, out int y);
            return GetChunk(x, y);
        }

        /// <summary>
        /// update tile visualization color mode
        /// </summary>
        public void UpdateVisualizationMode(VisualizationMode displayMode)
        {
            if (tileVisual != displayMode)
            {
                tileVisual = displayMode;
                for (int i = 0; i < lastUpdatedChunks.Count; i++)
                {
                    lastUpdatedChunks[i].UpdateTerrainVisualization(displayMode);
                }
            }
        }

        /// <summary>
        /// runs through surrounding chunks and decides wether or not 
        /// to hide them based on distance form player
        /// </summary>
        public void CheckChunkStatus(Vector2 playerPosition)
        {
            //un render all last active chunks
            foreach (var chunk in lastUpdatedChunks)
            {
                chunk.SetVisibility(false);
            }
            lastUpdatedChunks.Clear();

            Vector2 currentChunkCords = GetCurrentChunk(playerPosition);

            //Run through surrounding chunks at player position 
            for (int xOffset = -visibleChunks; xOffset < visibleChunks /*+1 todo when threaded*/; xOffset++)
            {

                for (int yOffset = -visibleChunks; yOffset < visibleChunks /*+1 todo when threaded*/; yOffset++)
                {
                    Vector2 viewChunkCord = new Vector2(currentChunkCords.X + xOffset, currentChunkCords.Y + yOffset);
                    if (chunkArray.TryGetValue(viewChunkCord, out var chunk))
                    {
                        chunk.UpdateVisibility(viewChunkCord);
                        if (chunk.Rendered)
                        {
                            lastUpdatedChunks.Add(chunk);
                        }
                    }
                    else
                    {
                        Chunk newChunk = new(GetChunkWorldPosition(viewChunkCord), MapSettings.CELL_SIZE);
                        map.AddChild(newChunk);
                        chunkArray.Add(viewChunkCord, newChunk);
                    }
                }

            }
        }

    }
}