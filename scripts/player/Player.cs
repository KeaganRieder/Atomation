using Godot;
using Atomation.Thing;
using System.Collections.Generic;
using Atomation.Map;

namespace Atomation.PlayerChar
{
	/// <summary>
	/// defines the player
	/// </summary>
	public partial class Player : CompThing
	{
		public ColorRect Graphic;
		public CharacterBody2D Body { get; private set; }
		public Camera Camera { get; private set; }

		public Player()
		{	
			Initialize();	
			Name = "player";

			coordinate = new Coordinate(Vector2.Zero);

			Body = new CharacterBody2D();
			Camera = new Camera();
			Graphic = new ColorRect
			{
				Size = new Vector2(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE * 2),
				Color = new Color(Colors.White),
				VisibilityLayer = 2
			};

			AddChild(Camera);
			AddChild(Graphic);
			AddChild(Body);
		}

		private void Initialize(){
			stats = new Dictionary<string, Stat>(){
				{"MoveSpeed", new Stat("MoveSpeed", "players moveSpeed", 1, 0.1f, 2)}};
		}

		public void Move()
		{
			//figure out how to smooth movement

			Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
			Position += velocityVector.Normalized() * Stat("MoveSpeed").Value;
			Coordinate.UpdateWorldPosition(Position);
			//todo: animation code here at some point
		}

		
	}

}
