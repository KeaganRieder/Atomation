namespace Atomation.GameMap;

using Atomation.Player;
using Godot;
using System;
using System.Collections.Generic;

// todo readd biomes, and make mountains structures
// also re integrate chunk loading and that sorta stuff
//todo make an actual limit

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

    private WorldSettings settings;

    private MapGenerator mapGenerator;
    private ChunkHandler chunkHandler;

    private Map()
    {
        Name = "World Map";
        settings = new WorldSettings();
        mapGenerator = new MapGenerator(settings);

        mapGenerator.SetChunkMode(true);
        chunkHandler = new ChunkHandler(this);
    }

    public MapGenerator MapGenerator { get => mapGenerator; }
    public ChunkHandler ChunkHandler { get => chunkHandler; }

    public WorldSettings Settings { get => settings; set => settings = value; }

    public override void _Ready()
    {
        base._Ready();
        settings.TrueCenter = false;
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
    /// finalizes the maps generation based on the specified settings.
    /// this include actually generating the map, and spawning/placing the player
    /// </summary>
    public void GenerateMap()
    {
        Vector2I startCords = Vector2I.Zero;
        int generationRadius = 1;

        for (int x = -generationRadius; x < generationRadius + 1; x++)
        {
            for (int y = -generationRadius; y < generationRadius + 1; y++)
            {
                Vector2 cords = new Vector2(startCords.X + x, startCords.Y + y);

                chunkHandler.GenerateChunk(cords);
            }
        }

    }

    public void FinalizeGeneration()
    {
        PlayerCharacter player = PlayerCharacter.Instance;
        player.SpawnPlayer();

        player.ChunkLoader.TryLoading();
    }

    public void HandleInteraction()
    {
        // todo/maybe?
    }

}