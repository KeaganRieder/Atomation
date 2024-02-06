using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// A chunk is a 32 x 32 section of the map that contans
/// various values, it is eitehr loaded or unloaded depedning on where 
/// the player is, as well as other aspects
/// </summary>
public class Chunk
{
    public const int CHUNK_SIZE = 32;

    private Node2D chunkNode;
    private Vector2 position;

    private Dictionary<Vector2, Terrain> chunkTerrain; // maybe make new class for this?

    //constructors
    public Chunk()
    {
        chunkTerrain = new Dictionary<Vector2, Terrain>();
    }

    public Chunk(Vector2 chunkCords, Node2D parentNode) : this()
    {
        position = chunkCords * WorldMap.CELL_SIZE;
        // position.X += 16;
        chunkNode = new Node2D()
        {
            Name = $"Chunk {chunkCords}",
            Position = position,
        };

        parentNode.AddChild(chunkNode);
    }

    //getters and setters 
    public Node2D ChunkNode { get => chunkNode; }

    public Terrain ChunkTerrain(Vector2 key)
    {
        if (chunkTerrain.ContainsKey(key))
        {
            return chunkTerrain[key];
        }
        return default;
    }

    public void ChunkTerrain(Vector2 key, Terrain terrain)
    {
        chunkTerrain[key] = terrain;
        chunkNode.AddChild(chunkTerrain[key].TerrainObj);
        chunkTerrain[key].Display(TerrainDispalyMode.Height);
    }

    //rendering stuff
    public void UpdateChunk(Vector2 veiwerCords)
    {
        float distToVeiwer = (position / CHUNK_SIZE).DistanceTo(veiwerCords);
        bool visible = distToVeiwer <= ChunkHandler.MAX_LOAD_DIST;
        SetRenderState(visible);
    }
    public bool Rendered()
    {
        return ChunkNode.Visible;
    }
    public void SetRenderState(bool state)
    {
        ChunkNode.Visible = state;
    }
}