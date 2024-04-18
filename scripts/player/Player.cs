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

		coordinate = new Coordinate(Vector2.Zero);

		Body = new CharacterBody2D();
		Camera = new Camera();
		Graphic = new StaticGraphic("player", 1, new Vector2I(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE), Colors.White);

		AddChild(Camera);
		AddChild(Graphic);
		AddChild(Body);
	}

	private void Initialize()
	{
		Dictionary<string, StatBase> stats = new Dictionary<string, StatBase>(){
				{StatKeys.MOVE_SPEED, new StatBase(StatKeys.MOVE_SPEED, "players moveSpeed", 1, StatType.Constant)},
				{StatKeys.MAX_HEALTH, new DamageAbleStat(StatKeys.MAX_HEALTH, "players hit points", 100)},
				{StatKeys.ATTACK_DAMAGE, new DamageAbleStat(StatKeys.ATTACK_DAMAGE, "players Attack dmg", 10)}};

		StatSheet = new StatSheet(stats, new Dictionary<string, StatModifierBase>());
	}

	public void Move()
	{
		//figure out how to smooth movement
		Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		Position += velocityVector.Normalized() * StatSheet.GetStat(StatKeys.MOVE_SPEED).Value;
		Coordinate.UpdateWorldPosition(Position);
		//todo: animation code here at some point
	}

}