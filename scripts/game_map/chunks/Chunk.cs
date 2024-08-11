namespace Atomation.GameMap;

using System;
using Resources;
using Things;
using Godot;

public partial class Chunk : Node2D
{
    public const int CHUNK_SIZE = 32;
    private Sprite2D graphic;

    Vector2 chunkPosition;

    private bool loaded;

    private Grid chunkGrid;

    public Chunk()
    {
    }

    public Chunk(Vector2 position)
    {
        Name = $"chunk:{position * Map.CELL_SIZE * (CHUNK_SIZE - 1)}";
        Vector2 pos = position * Map.CELL_SIZE * (CHUNK_SIZE - 1); //figure out weird 16 pixel offset
        SetPosition(pos);

        chunkGrid = new Grid(false);

        // Unload();
    }

    /// <summary>
    /// clears chunk
    /// </summary>
    public void Clear()
    {
        chunkGrid.Clear();
    }

    /// <summary> 
    /// sets chunks position to given 
    /// </summary>
    public void SetPosition(Vector2 cord)
    {
        chunkPosition = cord;
        Position = cord;
    }

    /// <summary> 
    /// sets terrain at given position
    /// </summary>
    public void SetTerrain(Vector2 cord, Terrain terrain)
    {
        if (terrain != null)
        {
            AddChild(terrain.Graphic);
        }
        chunkGrid.SetValue(cord, terrain, terrain.GridLayer);
    }

    /// <summary> 
    /// gets terrain at given position 
    /// </summary>
    public Terrain GetTerrain(Vector2 cord)
    {
        Terrain terrain = null;

        object obj = chunkGrid.GetValue(cord, 1);
        if (obj is Terrain)
        {
            terrain = obj as Terrain;
        }
        else
        {
            throw new InvalidOperationException("Object on map layer {terrain} is not terrain");
        }

        return terrain;
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