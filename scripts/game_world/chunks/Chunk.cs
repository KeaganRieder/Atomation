namespace Atomation.GameMap;

using System;
using Resources;
using Things;
using Godot;

public partial class Chunk : Node2D
{
    public const int CHUNK_SIZE = 32;

    private bool loaded;

    private Grid chunkGrid;

    public Chunk() { }

    public Chunk(Vector2 position)
    {
        Name = $"chunk:{position}";
        Vector2 pos = position * Map.CELL_SIZE * CHUNK_SIZE;
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
            Position = value;
        }
    }

    /// <summary>
    /// adds object of type to the chunks grid
    /// </summary>
    public void SetGridObject<ObjectType>(Vector2 cord, ObjectType obj, int gridLayer = -1) where ObjectType : IThing
    {
        if (gridLayer == -1)
        {
            gridLayer = obj.GridLayer;
        }
        if (obj == null)
        {
            RemoveGridObject<ObjectType>(cord, gridLayer);
        }
        else
        {
            AddChild(obj.Node);
            chunkGrid.SetValue(cord, obj, gridLayer);
            obj.Chunk = this;
        }
    }
    
    /// <summary>
    /// adds object of type  at cords on the grid
    /// </summary>
    public ObjectType GetGridObject<ObjectType>(Vector2 cord, int gridLayer) where ObjectType : class
    {
        var obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is ObjectType)
        {
            return obj as ObjectType;
        }
        else if (obj == null)
        {
            return null;
        }
        throw new InvalidOperationException($"Object on map layer {gridLayer} is not of type {typeof(ObjectType).Name}");
    }

    /// <summary>
    /// removes object of type from the chunks grid at the cords
    /// </summary>
    public void RemoveGridObject<ObjectType>(Vector2 cord, int gridLayer) where ObjectType : IThing
    {
        chunkGrid.RemoveValue(cord, gridLayer);
    }

    /// <summary> 
    /// gets structure at given position 
    /// </summary>
    public Structure GetStructure(Vector2 cord, int gridLayer = GameLayers.Structure)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Structure)
        {
            return obj as Structure;
        }
        else if (obj == default)
        {
            return null;
        }
        else
        {
            throw new InvalidOperationException($"Object on map layer {GameLayers.Structure} is not Structure");
        }
    }

    /// <summary> 
    /// gets terrain at given position 
    /// </summary>
    public Terrain GetTerrain(Vector2 cord, int gridLayer = GameLayers.Terrain)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Terrain)
        {
            return obj as Terrain;
        }
        else if (obj == default)
        {
            return null;
        }
        else
        {
            throw new InvalidOperationException("Object on map layer {terrain} is not terrain");
        }
    }
    /// <summary> 
    /// gets an item at given position 
    /// </summary>
    public Item GetItem(Vector2 cord, int gridLayer = GameLayers.Items)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Item)
        {
            return obj as Item;
        }
        else if (obj == default)
        {
            return null;
        }
        else
        {
            throw new InvalidOperationException($"Object on map layer {GameLayers.Items} is not Structure");
        }
    }

    /// <summary> 
    /// gets a plant at given position 
    /// </summary>
    public Plant GetPlant(Vector2 cord, int gridLayer = GameLayers.plants)
    {
        object obj = chunkGrid.GetValue(cord, gridLayer);
        if (obj is Plant)
        {
            return obj as Plant;
        }
        else if (obj == default)
        {
            return null;
        }
        else
        {
            throw new InvalidOperationException($"Object on map layer {GameLayers.plants} is not Structure");
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