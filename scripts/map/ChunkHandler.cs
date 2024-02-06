using System.Collections.Generic;
using Godot;

/// <summary>
/// ChunkHandler, cstores and handles chunks, allow for 
/// interaction with differnt elemnt in a chunk
/// 
/// </summary>
public class ChunkHandler
{
    public const float MAX_LOAD_DIST = 64;

    private readonly int visableChunks;
    private List<Chunk> lastUpdatedChunks;
    private Dictionary<Vector2, Chunk> chunks;

    private Node2D map;
    public ChunkHandler()
    {
        visableChunks = Mathf.RoundToInt(MAX_LOAD_DIST / Chunk.CHUNK_SIZE);
        lastUpdatedChunks = new List<Chunk>();
        chunks = new Dictionary<Vector2, Chunk>();
    }
    public ChunkHandler(Node2D map) : this()
    {
        this.map = map;
    }

    public Chunk this[Vector2 key]
    {
        get
        {
            if (chunks.ContainsKey(key))
            {
                return chunks[key];
            }
            else
            {
                throw new KeyNotFoundException($"Chunk at {key} doesn't exsit");
                // return default;
            }
        }
        private set
        {
            chunks[key] = value;
        }
    }

    /// <summary>
    /// handles updating and creating new chunks if they haven't been loaded yet
    /// </summary>
    public void UpdateRenderedChunks(Vector2 veiwerPostion, WorldGenerator mapGenerator)
    {
        int currentXCord = Mathf.RoundToInt(veiwerPostion.X / Chunk.CHUNK_SIZE);
        int currentYCord = Mathf.RoundToInt(veiwerPostion.Y / Chunk.CHUNK_SIZE);

        for (int i = 0; i < lastUpdatedChunks.Count; i++)
        {
            lastUpdatedChunks[i].SetRenderState(false);
        }
        lastUpdatedChunks.Clear();

        //run to each cord surround player and check to see if current chunk
        //is active and needs to be rendered/de-rendered
        for (int xOffset = -visableChunks; xOffset < visableChunks; xOffset++)
        {
            for (int yOffset = -visableChunks; yOffset < +visableChunks; yOffset++)
            {
                Vector2 veiwedChunkCord = new Vector2(currentXCord + xOffset, currentYCord + yOffset);
                UpdateChunk(veiwedChunkCord, mapGenerator);
            }
        }
    }

    private void UpdateChunk(Vector2 globalCord, WorldGenerator mapGenerator)
    {
        //check if chunk has been loaded before
        if (chunks.TryGetValue(globalCord, out var chunk))
        {
            chunk.UpdateChunk(globalCord);
            if (chunk.Rendered())
            {
                lastUpdatedChunks.Add(chunk);
            }
        }
        //it hasn't so create new chunk
        else
        {
            chunks.Add(globalCord, mapGenerator.GenerateChunk(globalCord, map));
        }
    }
}
/*
  private void LoadUnloadChunks(Vector3 playerPosition)
    {
        int playerChunkX = Mathf.FloorToInt(playerPosition.x / chunkSize);
        int playerChunkZ = Mathf.FloorToInt(playerPosition.z / chunkSize);

        for (int x = playerChunkX - maxLoadDistance; x <= playerChunkX + maxLoadDistance; x++)
        {
            for (int z = playerChunkZ - maxLoadDistance; z <= playerChunkZ + maxLoadDistance; z++)
            {
                Vector3 chunkPosition = new Vector3(x * chunkSize, 0, z * chunkSize);
                float distance = Vector3.Distance(playerPosition, chunkPosition);

                if (distance < maxLoadDistance * chunkSize)
                {
                    LoadChunk(chunkPosition);
                }
                else
                {
                    UnloadChunk(chunkPosition);
                }
            }
        }
    }

*/
