namespace Atomation.GameMap;

using Atomation.Player;
using Atomation.Things;
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

    private bool mapGenerated;
    private WorldSettings settings;
    WorldConfigs configs;

    private List<GenStep> genSteps;

    private ChunkHandler chunkHandler;

    private Map()
    {
        Name = "World Map";
        mapGenerated = false;
        settings = new WorldSettings();

        chunkHandler = new ChunkHandler(this);
    }

    public ChunkHandler ChunkHandler { get => chunkHandler; }

    public WorldSettings Settings { get => settings; set => settings = value; }

    public override void _Ready()
    {
        base._Ready();
        settings.TrueCenter = false;
    }

    /// <summary> 
    /// clears the map 
    /// </summary>
    public void ClearMap()
    {
        //make delete kids  if in map preview
        // GD.PushError("clearing not implemented");
        chunkHandler.Clear();
    }

    /// <summary>
    /// generates new chunks based on the current configs, 
    /// assigning them to the maps chunk handler
    /// </summary>
    public void GenerateChunk(Vector2I genOffset)
    {
        if (chunkHandler == null)
        {
            GD.PushError("can't generate sense chunk handler not provided");
            return;
        }
        Vector2I genSize = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
        GenStepData GenStepData = new GenStepData(genOffset, genSize, settings);

        genSteps = new List<GenStep>
        {
            new GenStepNoiseMaps(),
            new GenStepLandScape(),
            new GenStepPlants()
        };

        foreach (var genStep in genSteps)
        {
            genStep.RunStep(GenStepData);
        }

        //finalize generation of chunk
        Chunk chunk = chunkHandler.GetChunk(genOffset);
        if (chunk == null)
        {
            GD.PushError($"Chunk at {genOffset} is null, can't assigned generated values");
            return;
        }

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                chunk.SetGridObject(new Vector2(x, y), GenStepData.GeneratedTerrain[x, y]);
                Structure structure = GenStepData.GeneratedStructures[x, y];
                Plant plant = GenStepData.GeneratedFoliage[x, y];

                if (structure != null)
                {
                    chunk.SetGridObject(new Vector2(x, y), structure);
                }
                if (plant != null)
                {
                    chunk.SetGridObject(new Vector2(x, y), plant);
                }
            }
        }
    }

    /// <summary>
    /// generates the games map based on current settings
    /// </summary>
    public void GenerateMap(Vector2I startCords = default, int generationRadius = 1)
    {
        ClearMap();

        if (startCords == default)
        {
            startCords = Vector2I.Zero;
        }


        for (int x = -generationRadius; x < generationRadius + 1; x++)
        {
            for (int y = -generationRadius; y < generationRadius + 1; y++)
            {
                Vector2 cords = new Vector2(startCords.X + x, startCords.Y + y);

                chunkHandler.GenerateChunk(cords);
            }
        }
        mapGenerated = true;
    }

    public void GenerateMap(WorldConfigs settings)
    {
        ClearMap();

        configs = settings;
        int generationRadius = 1;
        Vector2I startCords = Vector2I.Zero;

        for (int x = -generationRadius; x < generationRadius + 1; x++)
        {
            for (int y = -generationRadius; y < generationRadius + 1; y++)
            {
                Vector2 cords = new Vector2(startCords.X + x, startCords.Y + y);

                chunkHandler.GenerateChunk(cords);
            }
        }
        mapGenerated = true;
    }

    /// <summary>
    /// finalizes the game's map by assigning the things generated during generateMap
    /// to correct places
    /// </summary>
    public void FinalizeGeneration()
    {
        if (mapGenerated == false)
        {
            GenerateMap();
        }

        PlayerCharacter player = PlayerCharacter.Instance;
        player.SpawnPlayer();

        player.ChunkLoader.TryLoading();
    }
    public void FinalizeGeneration(WorldConfigs settings)
    {
        GD.Print("setWorld");
        if (mapGenerated == false)
        {
            GenerateMap(settings);
        }
        GD.Print("FinalizeWorld");

        PlayerCharacter player = PlayerCharacter.Instance;
        player.SpawnPlayer();
        GetParent().AddChild(player);
        player.ChunkLoader.TryLoading();
    }

}