namespace Atomation.PlayerChar;

using Godot;
using Atomation.Resources;
using Atomation.Things;
using System.Collections.Generic;
using Atomation.Map;



/// <summary>
/// defines the player
/// </summary>
public partial class Player : CompThing
{
	public CharacterBody2D Body { get; private set; }
	public Camera Camera { get; private set; }

	public Player()
	{
		Initialize();
		Name = "player";
		Description = "The player Character";

		SetSpawn(new Coordinate(Vector2.Zero));

		Body = new CharacterBody2D();
		Camera = new Camera();
		Graphic = new StaticGraphic("player", 1, new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE), Colors.White);

		AddChild(Camera);
		AddChild(Graphic);
		AddChild(Body);
	}

	private void Initialize()
	{
		Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
				{StatKeys.MOVE_SPEED, new ModifiableStat(StatKeys.MOVE_SPEED, "players moveSpeed", 1)},
				{StatKeys.MAX_HEALTH, new ModifiableStat(StatKeys.MAX_HEALTH, "players hit points", 100)},
				{StatKeys.ATTACK_DAMAGE, new ModifiableStat(StatKeys.ATTACK_DAMAGE, "players Attack dmg", 10)}};

		StatSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
	}

	public void SetSpawn(Coordinate coordinate)
	{
		Coordinate = coordinate;
	}

	public override void Damage(float amount){
		//todo death
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);
	}
	public override void Heal(float amount){
		StatSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
	}

	public void Move()
	{
		//figure out how to smooth movement
		Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		Position += velocityVector.Normalized() * StatSheet.GetStat(StatKeys.MOVE_SPEED).CurrentValue;
		Coordinate.UpdateWorldPosition(Position);
		//todo: animation code here at some point
	}

}