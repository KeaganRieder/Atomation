using System.Collections.Generic;
using Godot;

/// <summary>
/// ChunkHandler, cstores and handles chunks, allow for 
/// interaction with differnt elemnt in a chunk
/// 
/// </summary>
public class ChunkHandler
{
    public const float MAX_VIEW_DIST  = 64;

    private readonly int visableChunks;
    private List<Chunk> lastUpdatedChunks;
    private Dictionary<Vector2,Chunk> chunks;

    private MapGenerator generator;
    private Node2D map;

    public ChunkHandler(){

        visableChunks = Mathf.RoundToInt(MAX_VIEW_DIST / Chunk.CHUNK_SIZE - 1);
        lastUpdatedChunks = new List<Chunk>();
        chunks = new Dictionary<Vector2, Chunk>();
    }
    public ChunkHandler(MapGenerator generator, Node2D map) : this(){
        this.map = map;
        this.generator = generator;
    }

    public Chunk this[Vector2 key]{
         get{
            if (chunks.ContainsKey(key))
            {
                return chunks[key];
            }
            else{
                throw new KeyNotFoundException($"Chunk at {key} doesn't exsit");
                // return default;
            }
        }
        private set{
            chunks[key] = value;
        }
    }

    /// <summary>
    /// normalize the provide x and y global cords to chunk cords
    /// divide globals by chunk size(32) and round to int
    /// </summary>
    public Vector2 GetChunkCords(float globalX, float globalY)
    {
        int chunkXCord = Mathf.RoundToInt(globalX / Chunk.CHUNK_SIZE);
        int chunkYCord = Mathf.RoundToInt(globalY / Chunk.CHUNK_SIZE);
        return new Vector2(chunkXCord, chunkYCord);
    }

    /// <summary>
    /// normalize the provided global cords to chunk cords
    /// divide global cords by chunkSize(32) and round to int
    /// </summary>
    public Vector2 GetChunkCords(Vector2 cords)
    {
        return GetChunkCords(cords.X, cords.Y);
    }

    /// <summary>
    /// handles updating and creating new chunks if they haven't been loaded yet
    /// </summary>
    public void UpdateChunks(Vector2 veiwerPostion, MapGenerator mapGenerator){
        for (int i = 0; i < lastUpdatedChunks.Count; i++)
        {
            lastUpdatedChunks[i].Rendered(false);
        }
        lastUpdatedChunks.Clear();
        Vector2 currentCords = GetChunkCords(veiwerPostion);

        //run to each cord surround player and check to see if current chunk
        //is active and needs to be rendered/de-rendered
        for (int xOffset = -visableChunks; xOffset <= visableChunks; xOffset++)
        {
            for (int yOffset = -visableChunks; yOffset <= visableChunks; yOffset++)
            {
                Vector2 veiwedChunkCord = new Vector2(currentCords.X + xOffset, currentCords.Y + yOffset);
                UpdateChunk(veiwedChunkCord, mapGenerator);
            }
        }
    }

    private void UpdateChunk(Vector2 chunkCord, MapGenerator mapGenerator){
       
        if (chunks.TryGetValue(chunkCord, out var chunk))
        {
            if (chunk.Rendered(chunkCord))
            {
                lastUpdatedChunks.Add(chunk);
            }
        }
        else{
            // mapGenerator
            GD.Print("added newly laoded chunk");
            Chunk newChunk = new Chunk(chunkCord, new Node2D());
            map.AddChild(newChunk.ChunkNode);    
            chunks.Add(chunkCord, newChunk);
        }
    }
}