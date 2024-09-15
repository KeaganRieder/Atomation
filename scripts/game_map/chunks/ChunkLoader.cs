namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

/// <summary>
/// a chunk loaders handle updating wether or not chunks are loaded (rendered) or not.
/// this is decided based on the actors position which is updated on set delate
/// 
/// todo add bouldery so chunks only generate if they are within teh world size
/// </summary>
public class ChunkLoader
{
    protected Node2D actor;

    protected ChunkHandler chunkHandler;
    protected int renderDistance;
    
    
    /// <summary>
    /// determines the delay (in milliseconds) between checks. 
    /// </summary>
    protected int updateDelay;

    protected int lastUpdate;
    protected Vector2I lastPosition;

    public ChunkLoader() { }
    public ChunkLoader(ChunkHandler chunkHandler, Node2D actor = null)
    {
        this.actor = actor;
        this.chunkHandler = chunkHandler;

        renderDistance = 1;

        updateDelay = 1000; // run the chunk loader every half second 

        lastUpdate = 0;
        lastPosition = new Vector2I(-1, -1);
    }

    /// <summary>
    /// checks if actor is at the same chunk they were last time, 
    /// if they are then doesn't update chunks. otherwise updates chunks
    /// </summary>
    public void TryLoading()
    {
        int currentTime = (int)Time.GetTicksMsec();
        if (!(currentTime - lastUpdate > updateDelay))
        {
            return;
        }
        else
        {
            lastUpdate = currentTime;
        }

        Vector2I actorPosition = GetActorPosition();
        if (actorPosition == lastPosition)
        {
            return;
        }

        lastPosition = actorPosition;
        UpdateLoaded(actorPosition);
    }

    /// <summary>
    /// decides base on position wether to load or unload a chunk
    /// </summary>
    protected void UpdateLoaded(Vector2I position)
    {
        if (chunkHandler == null)
        {
            GD.PushError("can't load chunks chunkHandler property is null");
            return;
        }

        List<Vector2I> chunksToUpdate = GetLoadedChunks(position);

        UnloadChunks(chunksToUpdate);
        LoadChunks(chunksToUpdate);
    }

    /// <summary>
    /// gets the actor/players position in terms of chunk cords
    /// </summary>
    protected virtual Vector2I GetActorPosition()
    {
        Vector2 actorPosition = Vector2I.Zero;

        // todo get player instance and then set that as chunk cords
        if (actor != null)
        {
            actorPosition = actor.GlobalPosition.Floor();
        }

        Vector2 mapPosition = actorPosition.GlobalToMap();
        Vector2 chunkPosition = mapPosition.MapToChunk();
        return new Vector2I(Mathf.RoundToInt(chunkPosition.X),Mathf.RoundToInt(chunkPosition.Y));
    }

    /// <summary>
    /// gets required chunks to update based on given position
    /// </summary>
    protected List<Vector2I> GetLoadedChunks(Vector2I actorCords)
    {
        Vector2I actorPosition = actorCords;
        List<Vector2I> chunksToLoad = new List<Vector2I>();

        for (int x = -renderDistance; x < renderDistance+1; x++)
        {
            for (int y = -renderDistance; y < renderDistance+1; y++)
            {
                Vector2I chunkCord = new Vector2I(actorPosition.X + x, actorPosition.Y + y);
                chunksToLoad.Add(chunkCord);
            }
        }
        return chunksToLoad;
    }

    /// <summary>
    /// runs through chunks at given positions and unloads all that 
    /// should be 
    /// </summary>
    protected void UnloadChunks(List<Vector2I> chunksToLoad)
    {
        List<Vector2I> LoadedChunks = new List<Vector2I>();

        foreach (var chunk in chunkHandler.GetLastLoaded())
        {
            LoadedChunks.Add(chunk);
        }

        foreach (var chunk in LoadedChunks)
        {
            if (!chunksToLoad.Contains(chunk))
            {
                chunkHandler.UnloadChunk(chunk);
            }
        }
    }

    /// <summary>
    /// runs through chunks at given positions and unloads all that 
    /// should be 
    /// </summary>
    protected void LoadChunks(List<Vector2I> chunksToLoad)
    {
        foreach (var toLoad in chunksToLoad)
        {
            chunkHandler.LoadChunk(toLoad);
        }
    }
}