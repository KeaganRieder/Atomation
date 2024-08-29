namespace Atomation.Player;

using System.Collections.Generic;
using GameMap;
using Resources;
using StatSystem;
using Atomation.Systems;
using Godot;

/// <summary>
/// a class which represents the games player. holding stats and
/// other values which relate to their character
/// </summary>
public partial class PlayerCharacter : CharacterBody2D
{
    private static PlayerCharacter instance;
    public static PlayerCharacter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerCharacter();
            }

            return instance;
        }
    }

    private PlayerController controller;
    private CustomCamera camera;
    private StaticGraphic graphic;

    private ChunkLoader chunkLoader;

    private StatSheet statSheet;

    private PlayerCharacter()
    {
        Name = "player";
        ZIndex = 1;
    }

    public CustomCamera Camera { get => camera; set => camera = value; }
    public StaticGraphic Graphic { get => graphic; set => graphic = value; }
    public ChunkLoader ChunkLoader { get => chunkLoader; }

    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    public PlayerController Controller { get => controller; }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        MoveAndSlide();
        ChunkLoader.TryLoading();
    }

    /// <summary>
    /// spawns the player into the game world
    /// </summary>
    public void SpawnPlayer(Vector2 spawnCords = default)
    {
        if (spawnCords == default)
        {
            spawnCords = Vector2.Zero;
        }

        InitializeStats();

        chunkLoader = new ChunkLoader(Map.Instance.ChunkHandler, this);
        graphic = new StaticGraphic("player", 1, new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE), Colors.Black, this);
        graphic.RenderingLayer = 1;
        controller = new PlayerController(this);

        Position = spawnCords;
    }

    private void InitializeStats()
    {
        Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
                {"moveSpeed", new StatBase("moveSpeed", "players moveSpeed", 100,10,200)},
                {"health", new StatBase("health", "players hit points", 100,0,100)},
                {"attack", new StatBase("attack", "players Attack dmg", 10,0,1000)}};

        statSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
    }


    /// <summary>
    /// used to damage player by an amount
    /// </summary>
    public void Damage(float amount)
    {
        //todo death make signal
        statSheet.GetStat("health").Damage += amount;
    }
    /// <summary>
    /// used to damage player by an amount specified by the attack stat
    /// </summary>
    public void Damage(StatSheet statSheet)
    {
        StatBase dmg = statSheet.GetStat("attack");

        if (dmg != null)
        {
            Damage(dmg.CurrentValue);
        }
        else
        {
            GD.PushError("no attack stat in given stat sheet");
        }
    }
    /// <summary>
    /// used to heal player by an amount 
    /// </summary>
    public void Heal(float amount)
    {
        statSheet.GetStat("health").Damage -= amount;
    }

}