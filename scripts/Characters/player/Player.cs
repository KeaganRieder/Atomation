namespace Atomation.PlayerChar;

using Godot;
using Atomation.Resources;
using Atomation.Things;
using System.Collections.Generic;
using Atomation.Map;
using Atomation.Controls;


/// <summary>
/// A player in the game is what the users object is 
/// </summary>
public partial class Player : Node2D
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

	private Coordinate cords;
	private StatSheet statSheet;
	private Inventory inventory;

	private GameController gameController;
	private StaticGraphic graphic;
	private CharacterBody2D body;
	private Camera camera;

	private Player()
	{
		Name = "player";

		InitializeStats();
		ZIndex = VisualLayer.PLAYER;

		SetPosition(Vector2.Zero);
		body = new CharacterBody2D();

		inventory = new Inventory(this);
		camera = new Camera(this);
		gameController = new GameController(this);
		graphic = new StaticGraphic("player", 1, new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE), Colors.White);

		Item test = new Item(new Coordinate(Vector2.Zero));
		test.ReadConfigs(DefDatabase.Instance.GetItemDef("Stone"));
		inventory.SetItem(0,0,test);

		inventory.SetItem(1,1,test);
		inventory.SetItem(2,2,test);


		AddChild(graphic);
		AddChild(body);
	}

	private void InitializeStats()
	{
		Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
				{StatKeys.MOVE_SPEED, new ModifiableStat(StatKeys.MOVE_SPEED, "players moveSpeed", 1)},
				{StatKeys.MAX_HEALTH, new ModifiableStat(StatKeys.MAX_HEALTH, "players hit points", 100)},
				{StatKeys.ATTACK_DAMAGE, new ModifiableStat(StatKeys.ATTACK_DAMAGE, "players Attack dmg", 10)}};

		statSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
	}

	public void SetPosition(Coordinate cord)
	{

		cords = cord;
		Position = cord.GetWorldPosition();
	}
	public virtual void SetPosition(Vector2 cord)
	{
		if (cords != null)
		{
			cords.SetPosition(cord);
		}
		else
		{
			cords = new Coordinate(cord);
		}
		Position = cords.GetWorldPosition();
	}

	public Coordinate GetCoordinate()
	{
		if (cords != null)
		{
			return cords;
		}
		return new Coordinate(Position);
	}
	public StatSheet GetStatSheet()
	{
		return statSheet;
	}
	public Inventory GetInventory()
	{
		return inventory;
	}
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
		SetPosition(loadedData.Cords);
		statSheet = loadedData.StatSheet;
	}

	public void Move()
	{
		//figure out how to smooth movement
		Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		Position += velocityVector.Normalized() * statSheet.GetStat(StatKeys.MOVE_SPEED).CurrentValue;
		cords.SetPosition(Position);
		WorldMap.Instance.UpdateVisibleChunks(cords);
	}

	public void Damage(float amount)
	{
		//todo death
		statSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);
	}
	public void Damage(StatSheet statSheet)
	{
		StatBase dmg = statSheet.GetStat(StatKeys.ATTACK_DAMAGE);

		if (dmg != null)
		{
			Damage(dmg.CurrentValue);
		}
	}
	public void Heal(float amount)
	{
		statSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
	}
}