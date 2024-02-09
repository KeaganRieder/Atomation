using System;
using System.Collections.Generic;
using Godot;
using Atomation.Utility;

/// <summary>
/// A chunk is a 32 x 32 section of the map that contans
/// various values, it is eitehr loaded or unloaded depedning on where 
/// the player is, as well as other aspects
/// </summary>
public class Chunk
{
    public const int CHUNK_SIZE = 32;

    private Node2D chunkNode;

    private Dictionary<Vector2, Terrain> chunkTerrain; // maybe make new class for this?

    //constructors
    public Chunk()
    {
        chunkTerrain = new Dictionary<Vector2, Terrain>();
    }

    public Chunk(Vector2 chunkCords, Node2D parentNode) : this()
    {   
        //making it so names based on intervals of CHUNK_SIZE not CHUNK_SIZE * cellsize
        //making also alligned to cell size grid
        Vector2 Cords = CordConversion.ToCellSizeGrid(chunkCords);
        chunkNode = new Node2D()
        {
            Name = $"Chunk {chunkCords}",
            Position = Cords,
        };

        parentNode.AddChild(chunkNode);
    }

    //getters and setters 
    public Node2D ChunkNode { get => chunkNode; }

    public Chunk NorthChunk { get; set; } //up
    public Chunk SouthChunk { get; set; } //down
    public Chunk WestChunk { get; set; } //left
    public Chunk EastChunk { get; set; } //right

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
        chunkTerrain[key].Display(TerrainDispalyMode.Heat);
    }

    //rendering stuff
    public void UpdateChunk(Vector2 veiwerCords)
    {
        float distToVeiwer = (chunkNode.Position / CHUNK_SIZE).DistanceTo(veiwerCords);
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

    //todo make show grid 
}