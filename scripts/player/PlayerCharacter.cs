namespace Atomation.Player;

using System.Collections.Generic;
using GameMap;
using Resources;
using StatSystem;
using Atomation.Systems;
using Godot;
using Atomation.InventorySystems;

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
    private Graphic graphic;

    private ChunkLoader chunkLoader;

    private Inventory inventory;

    private StatSheet statSheet;

    private PlayerCharacter()
    {
        Name = "player"; 
        camera = new CustomCamera(this);
        controller = new PlayerController(this);
        inventory = new Inventory(Name);
        camera.AddChild(inventory);
    }

    public CustomCamera Camera { get => camera; set => camera = value; }
    public Graphic Graphic { get => graphic; set => graphic = value; }
    public ChunkLoader ChunkLoader { get => chunkLoader; }
    public PlayerController Controller { get => controller; }

    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    public Inventory Inventory { get => inventory; set => inventory = value; }

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
        graphic = new Graphic("player", 1, new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE), Colors.Black);
        graphic.RenderingLayer = GameLayers.Player;
        controller.SetMapTarget(Map.Instance);

        Position = spawnCords;
        AddChild(graphic);
        graphic.UpdateTexture();
        graphic.SetToDefaultColor();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        MoveAndSlide();
        ChunkLoader.TryLoading();
    }

    private void InitializeStats()
    {
        Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
                {"moveSpeed", new StatBase("moveSpeed", "players moveSpeed", 100,10,200)},
                {"health", new StatBase("health", "players hit points", 100,0,100)},
                {"attack", new StatBase("attack", "players Attack dmg", 100,0,1000)}};

        statSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
    }

    /// <summary>
    /// updates the players velocity 
    /// </summary>
    public void UpdateVelocity(Vector2 direction){
        Velocity = direction * StatSheet.GetStat("moveSpeed").CurrentValue;
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