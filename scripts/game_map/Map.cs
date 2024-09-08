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
    /// finalizes the maps generation based on the specified settings.
    /// this include actually generating the map, and spawning/placing the player
    /// </summary>
    public void GenerateMap()
    {
        //todo make it so the player doesn't generate but rather it's generated at 0,0
        // and then chunks get added to chunkhandler, and then the player is spawned
        PlayerCharacter player = PlayerCharacter.Instance;
        player.SpawnPlayer();

        player.ChunkLoader.TryLoading();
    }

    public void FinalizeGeneration(){
        //todo
    }

    public void HandleInteraction(){
        //maybe?
    }

}