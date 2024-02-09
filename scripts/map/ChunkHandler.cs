using System.Collections.Generic;
using Godot;
using Atomation.Utility;

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

    public ChunkHandler(Node2D map)
    {
        visableChunks = Mathf.RoundToInt(MAX_LOAD_DIST / Chunk.CHUNK_SIZE);
        lastUpdatedChunks = new List<Chunk>();
        chunks = new Dictionary<Vector2, Chunk>();
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

    public WorldGenerator WorldGenerator{get; set;}

    /// <summary>
    /// handles updating and creating new chunks if they haven't been loaded yet
    /// </summary>
    public void UpdateRenderedChunks(Vector2 veiwerPostion)
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
                UpdateChunk(veiwedChunkCord);
            }
        }
    }

    /// <summary>
    /// decides wether to keep a chunk currently loaded or unload it. if a new chunk is being 
    /// loaded also calls function to handle createing it
    /// </summary>
    private void UpdateChunk(Vector2 globalCord)
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
            // chunks.Add(globalCord, WorldGenerator.GenerateChunk(globalCord, map));
            CreateChunk(globalCord);
        }
    }

    private void CreateChunk(Vector2 globalCord){
        
        //make chunk be based on cords which are alighend to teh chunk grid and CellSize Grid
        Vector2 normalizedCords = CordConversion.ToChunkGrid(globalCord);

        //asigning chunks neighbours if they exsit
        //todo

        Chunk chunk = new(normalizedCords, map);

        chunks.Add(globalCord, WorldGenerator.GenerateChunk(normalizedCords, chunk));
    }

     
}

