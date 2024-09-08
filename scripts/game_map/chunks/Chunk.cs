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
    /// sets terrain at given position
    /// </summary>
    public void SetTerrain(Vector2 cord, Terrain terrain)
    {

        if (terrain == null)
        {
            RemoveTerrain(cord, GameLayers.Terrain);
        }
        if (terrain != null)
        {
            AddChild(terrain.Graphic);
            terrain.Chunk = this;
            chunkGrid.SetValue(cord, terrain, terrain.GridLayer);

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
    /// removes a terrain from the grid at the given position
    /// </summary>
    public void RemoveTerrain(Vector2 cord = default, int gridLayer = GameLayers.Terrain)
    {
        if (cord == default)
        {
            throw new InvalidOperationException("cord is default, can't remove structure at invalid cord");
        }
        GD.Print("terrain removal implementation needed");
    }

    /// <summary> 
    /// sets structure at given position
    /// </summary>
    public void SetStructure(Vector2 cord, Structure structure)
    {
        if (structure != default || structure != null)
        {
            AddChild(structure.Graphic);
            chunkGrid.SetValue(cord, structure, structure.GridLayer);
            structure.Chunk = this;
        }
        if (structure == null)
        {
            RemoveStructure(cord);
        }
    }

    /// <summary>
    /// removes a structure from the grid at the given position
    /// </summary>
    public void RemoveStructure(Vector2 cord, int gridLayer = GameLayers.Structure)
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
    /// sets item at given position
    /// </summary>
    public void SetItem(Vector2 cord, Item worldItem)
    {
        if (worldItem != default || worldItem != null)
        {
            AddChild(worldItem.Graphic);
            chunkGrid.SetValue(cord, worldItem, worldItem.GridLayer);
            worldItem.Chunk = this;
        }
        if (worldItem == null)
        {
            RemoveItem(cord);
        }
    }

    /// <summary> 
    /// gets item at given position 
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
    /// removes a item from the grid at the given position
    /// </summary>
    public void RemoveItem(Vector2 cord, int gridLayer = GameLayers.Items)
    {
        chunkGrid.RemoveValue(cord, gridLayer);
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