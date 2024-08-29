namespace Atomation.GameMap;

using System;
using Resources;
using Things;
using Godot;

public partial class Chunk : Node2D
{
    public const int CHUNK_SIZE = 32;
    // private Sprite2D graphic;
    // private Vector2 chunkPosition;

    private bool loaded;

    private Grid chunkGrid;

    public Chunk() { }

    public Chunk(Vector2 position)
    {
        Name = $"chunk:{position}";// * Map.CELL_SIZE * (CHUNK_SIZE - 1)
        Vector2 pos = position * Map.CELL_SIZE * (CHUNK_SIZE - 1); //figure out weird 16 pixel offset
        ChunkPosition = pos;

        chunkGrid = new Grid(false);
        loaded = true;
    }

    /// <summary>
    /// clears chunk
    /// </summary>
    public void Clear()
    {
        chunkGrid.Clear();
    }

    /// <summary> 
    /// property for setting and getting the chunks position
    /// </summary> 
    public Vector2 ChunkPosition
    {
        get => Position; set
        {
            // chunkPosition = value;
            Position = value;
        }
    }

    /// <summary> 
    /// sets terrain at given position
    /// </summary>
    public void SetTerrain(Vector2 cord, Terrain terrain)
    {
        if (terrain == null)
        {
            GD.Print("terrain is null");
        }
        if (terrain != null)
        {
            AddChild(terrain.Graphic);
        }
        chunkGrid.SetValue(cord, terrain, terrain.GridLayer);
    }

    /// <summary> 
    /// gets terrain at given position 
    /// </summary>
    public Terrain GetTerrain(Vector2 cord, int gridLayer = 0)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Terrain)
        {
            return obj as Terrain;
        }
        else
        {
            throw new InvalidOperationException("Object on map layer {terrain} is not terrain");
        }
    }

    /// <summary> 
    /// sets terrain at given position
    /// </summary>
    public void SetStructure(Structure structure, Vector2 cord = default)
    {
        if (structure != default ||structure != null)
        {
            AddChild(structure.Graphic);
            chunkGrid.SetValue(structure.Position, structure, structure.GridLayer);
        }
        // if (structure == null) //todo deleting structures
        // {
        //     chunkGrid.SetValue(cord, structure, 2);
        // }
    }

    /// <summary> 
    /// gets terrain at given position 
    /// </summary>
    public Structure GetStructure(Vector2 cord, int gridLayer = 2)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Structure)
        {
            return obj as Structure;
        }
        else
        {
            throw new InvalidOperationException("Object on map layer {structure} is not Structure");
        }
    }

    /// <summary> 
    /// unloads chunk 
    /// </summary>
    public void Unload()
    {
        loaded = false;
        Visible = false;
    }
    /// <summary>
    /// loads chunk
    /// </summary>
    public void Load()
    {
        loaded = true;
        Visible = true;
    }

    /// <summary>
    /// returns if chunks loaded (true) or unloaded (false)
    /// </summary>
    public bool Loaded()
    {
        return loaded;
    }
}