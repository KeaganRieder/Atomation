namespace Atomation.Player;

using Godot;
using Atomation.Resources;
using Atomation.Things;
using System.Collections.Generic;
using Atomation.Map;

/// <summary>
/// defines the player
/// </summary>
public partial class PlayerChar : BaseThing
{
	private static PlayerChar playerInstance;

	public CharacterBody2D Body { get; private set; }
	public Camera Camera { get; private set; }

	private PlayerChar()
	{
		Name = "player";
		Description = "The player Character";

		Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
				{StatKeys.MOVE_SPEED, new ModifiableStat(StatKeys.MOVE_SPEED, "players moveSpeed", 1)},
				{StatKeys.MAX_HEALTH, new ModifiableStat(StatKeys.MAX_HEALTH, "players hit points", 100)},
				{StatKeys.ATTACK_DAMAGE, new ModifiableStat(StatKeys.ATTACK_DAMAGE, "players Attack dmg", 10)}};

		StatSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());

		SetSpawn(new Coordinate(Vector2.Zero));

		Body = new CharacterBody2D();
		Camera = new Camera();
		Graphic = new StaticGraphic("player", 1, new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE), Colors.White);

		AddChild(Camera);
		AddChild(Graphic);
		AddChild(Body);
	}
	public static PlayerChar GetInstance()
	{
		if (playerInstance == null)
		{
			playerInstance = new PlayerChar();
		}

		return playerInstance;
	}

	public SavedPlayer Save(){
		return new SavedPlayer(this);
	}
	public void Load(SavedPlayer loadedData){
		GD.Print("Loading Player");
		
		Name = loadedData.Name;
        SetSpawn(loadedData.Cords);
        StatSheet = loadedData.StatSheet;
	}

	public void SetSpawn(Coordinate cord)
	{
		coordinate = cord;
		Position = cord.GetWorldPosition();
	}

	public void Move()
	{
		//figure out how to smooth movement
		Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		Position += velocityVector.Normalized() * StatSheet.GetStat(StatKeys.MOVE_SPEED).CurrentValue;
		coordinate.SetPosition(Position);
		WorldMap.GetInstance().UpdateVisibleChunks(coordinate);
	}

	public void Damage(float amount)
	{
		//todo death
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);
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
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
	}


}