using System.Collections.Generic;
using Godot;

/// <summary>
/// ChunkHandler, cstores and handles chunks, allow for 
/// interaction with differnt elemnt in a chunk
/// 
/// </summary>
public class ChunkHandler
{
    private int visableChunks;

    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    private List<Chunk> lastUpdated = new List<Chunk>(); 
    private MapGenerator generator;

    public ChunkHandler(){
        generator = new MapGenerator();

        visableChunks = 0; //todo
    }

    public Chunk GetChunk(Vector2 cords){
        if (chunks.ContainsKey(cords))
        {
            return chunks[cords];
        }
        else{
            GD.PrintErr($"Error invalid cords: chunk at {cords} is missing");
            return default;
        }
    }


    public void UpdateVisableChunks(){
        
        lastUpdated.Clear();
        //todo

    }
}
/*
 public void UpdateVisableChunks(Vector2 veiwerPostion)
    {
        for (int i = 0; i < chunksLastUpdated.Count; i++)
        {
            chunksLastUpdated[i].SetVisible(false);
        }
        chunksLastUpdated.Clear();

        Vector2 currentChunkCords = GetChunkCords(veiwerPostion.x, veiwerPostion.y);

        for (int xOffset = -visableChunks; xOffset <= visableChunks; xOffset++)
        {
            for (int yOffset = -visableChunks; yOffset <= visableChunks; yOffset++)
            {
                Vector2 veiwedChunkCord = new Vector2(currentChunkCords.x + xOffset, currentChunkCords.y + yOffset);
                if (chunks.ContainsKey(veiwedChunkCord))
                {
                    chunks[veiwedChunkCord].UpdateChunk(veiwerPostion);
                    if (chunks[veiwedChunkCord].IsVisible())
                    {
                        chunksLastUpdated.Add(chunks[veiwedChunkCord]);
                    }
                }
                else
                {
                    chunks.Add(veiwedChunkCord, new Chunk(veiwedChunkCord));
                    chunks[veiwedChunkCord].CreateChunk(veiwedChunkCord, mapGenerator);
                }
            }
        }


    }
*/