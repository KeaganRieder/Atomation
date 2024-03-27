using Godot;
using Atomation;
using Atomation.Thing;
using Atomation.Resources;
using System.Collections.Generic;
using Atomation.Map;

namespace Atomation.Player
{
	/// <summary>
	/// defines and hold values/objects that relate to the player
	/// </summary>
	public partial class PlayerChar : Node2D
	{
		public Dictionary<string, StatOld> Stats { get; private set; }
		private float move = 2;
		public ColorRect Graphic;// todo make new graphic type
		public CharacterBody2D Body { get; private set; }
		public Camera Camera { get; private set; }

		public PlayerChar()
		{
			// Stats = new Dictionary<string, Stat>(){
			//     {"MoveSpeed", new Stat("MoveSpeed","",2,0.1f,2)}};

			Position = Vector2.Zero;
			Graphic = new ColorRect
			{
				Size = new Vector2(MapSettings.CELL_SIZE, MapSettings.CELL_SIZE*2),
				Color = new Color(Colors.White)
			};
			Graphic.VisibilityLayer = 2;
			Body = new CharacterBody2D();
			Camera = new Camera();

			AddChild(Camera);
			AddChild(Graphic);
			AddChild(Body);
		}

		public PlayerChar(Vector2 cords) : this()
		{
			Position = cords;
		}

		//run every time theres input
		public override void _Input(InputEvent inputEvent)
		{
			base._Input(inputEvent);
			Move();
		}

		public void Move()
		{//figuring out how to smooth movement

			Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
			Position += velocityVector.Normalized() * move;
			//todo: animation code here at some point
		}
	}

}
