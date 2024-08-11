namespace Atomation.Pawns;

using Godot;
using Resources;
using Systems;
using StatSystem;
using Controls;
using System.Collections.Generic;
using Atomation.GameMap;



/// <summary>
/// A player in the game is what the users object is 
/// </summary>
public partial class Player : CharacterBody2D
{
	private static Player playerInstance;
	public static Player Instance
	{
		get
		{
			if (playerInstance == null)
			{
				playerInstance = new Player();
			}

			return playerInstance;
		}
	}

	private StatSheet statSheet;
	// private Inventory inventory;

	private Camera camera;
	// private PlayerController playerController;
	private StaticGraphic graphic;
	//todo Collision

	private Player()
	{
		Name = "player";

		InitializeStats();
		ZIndex = VisualLayer.PLAYER;

		Position = Vector2.Zero;
		// playerController = new PlayerController(this);
		// inventory = new Inventory(this);
		camera = new Camera(this);
		graphic = new StaticGraphic("player", 1, new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE), Colors.White, this);
	}

	private void InitializeStats()
	{
		Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
				{"moveSpeed", new StatBase("moveSpeed", "players moveSpeed", 50,10,200)},
				{"health", new StatBase("health", "players hit points", 100,0,100)},
				{"attack", new StatBase("attack", "players Attack dmg", 10,0,1000)}};

		statSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
	}

	public StatSheet GetStatSheet()
	{
		return statSheet;
	}
	// public Inventory GetInventory()
	// {
	// 	return inventory;
	// }
	public Camera GetCamera()
	{
		return camera;
	}

	public SavedPlayer Save()
	{
		return new SavedPlayer(this);
	}
	public void Load(SavedPlayer loadedData)
	{
		GD.Print("Loading Player");
		Name = loadedData.Name;
		Position = loadedData.Cords;
		statSheet = loadedData.StatSheet;
	}

	public void Move(Vector2 direction)
	{
		// Position += direction.Normalized()
		// cords.SetPosition(Position);
		Velocity = direction * statSheet.GetStat("moveSpeed").CurrentValue;
		MoveAndSlide();
	}

	public void Damage(float amount)
	{
		//todo death make signal
		statSheet.GetStat("health").Damage += amount;
	}
	public void Damage(StatSheet statSheet)
	{
		StatBase dmg = statSheet.GetStat("attack");

		if (dmg != null)
		{
			Damage(dmg.CurrentValue);
		}
	}
	public void Heal(float amount)
	{
		statSheet.GetStat("health").Damage -= amount;
	}
}