namespace Atomation.GameMap;

using Atomation.Player;
using Godot;
using System;
using System.Collections.Generic;

// todo readd biomes, and make mountains structures
// also re integrate chunk loading and that sorta stuff

/// <summary>
/// The Games map
/// </summary>
public partial class Map : Node2D
{
    public const int CELL_SIZE = 32;

    private static Map instance;
    public static Map Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Map();
            }
            return instance;
        }
    }

    private MapSettings settings;

    private MapGenerator mapGenerator;
    private ChunkHandler chunkHandler;
    // private ChunkLoader chunkLoader;

    private Map()
    {
        Name = "World Map";
        settings = new MapSettings();
        mapGenerator = new MapGenerator(settings);

        mapGenerator.SetChunkMode(true);
        chunkHandler = new ChunkHandler(this);
    }

    public MapGenerator MapGenerator { get => mapGenerator; }
    public ChunkHandler ChunkHandler { get => chunkHandler; }

    public MapSettings Settings { get => settings; set => settings = value; }

    public override void _Ready()
    {
        base._Ready();

        // GeneratePreview();
        settings.trueCenter = false;
    }

    public override void _Process(double delta)
    {
        // chunkLoader.TryLoading();
    }


    /// <summary> 
    /// clears the map 
    /// </summary>
    public void ClearMap()
    {
        //make delete kids  if in map preview
        GD.PushError("clearing not implemented");
        chunkHandler.Clear();
    }

    /// <summary>
    /// generate the map 
    /// need to figure out a different way of doing this maybe something 
    /// like a map generator class
    /// </summary>
    public void GeneratePreview()
    {
        mapGenerator.updateSize();
        // mapGenerator.GenerateMap(Vector2I.Zero, this);
    }

    /// <summary>
    /// finalizes the maps generation based on the specified settings.
    /// this include actually generating the map, and spawning/placing the player
    /// </summary>
    public void Generate()
    {
        PlayerCharacter player = PlayerCharacter.Instance;
        player.SpawnPlayer();

        player.ChunkLoader.TryLoading();
    }
}