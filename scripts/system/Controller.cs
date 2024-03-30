using Atomation.Map;
using Atomation.Player;
using Atomation.Thing;
using Godot;

namespace Atomation
{
	/// <summary>
	/// class which handles controls for the game
	/// </summary>
	public partial class Controller : Node2D
	{
		private KeyBindings keyBindings;

		public WorldMap Map { get; set; }
		public PlayerChar PlayerBody { get; set; }

		public Controller()
		{
			Name = "Controller";
			Position = Vector2.Zero;
			keyBindings = new KeyBindings("default_bindings");
		}

		public override void _Input(InputEvent inputEvent)
		{
			Map.ChunkHandler.CheckChunkStatus(PlayerBody.Position);
			PlayerBody.Move();

			if (inputEvent is InputEventMouseButton mouseInputEvent)
			{
				Vector2 mousePos = mouseInputEvent.Position - GetViewport().CanvasTransform.Origin;
				int x = Mathf.Abs(Mathf.FloorToInt(mousePos.X / MapSettings.CELL_SIZE));
				int y = Mathf.Abs(Mathf.FloorToInt(mousePos.Y / MapSettings.CELL_SIZE));
				GD.Print($"Converted: {x} {y} ");

				GD.Print($"Mouse: {mouseInputEvent.Position},{PlayerBody.GetViewport().GetMousePosition()} ");

				if (inputEvent.IsActionPressed("Left Click"))
				{
					Terrain terrain = Map.ChunkHandler.GetTerrain(GetGlobalMousePosition());

					if (terrain != null)
					{
						terrain.Visible = false;

					}
				}
				if (inputEvent.IsActionPressed("Right Click"))
				{
					GD.Print("right");

				}

			}

			if (inputEvent.IsActionPressed("Default"))
			{
				Map.ChunkHandler.UpdateVisualizationMode(VisualizationMode.Default);
				GD.Print("Default");
			}
			if (inputEvent.IsActionPressed("VisualizeMoisture"))
			{
				Map.ChunkHandler.UpdateVisualizationMode(VisualizationMode.Moisture);
				GD.Print("Moisture");
			}
			if (inputEvent.IsActionPressed("VisualizeHeat"))
			{
				Map.ChunkHandler.UpdateVisualizationMode(VisualizationMode.Heat);
				GD.Print("Heat");
			}
			if (inputEvent.IsActionPressed("VisualizeHeight"))
			{
				Map.ChunkHandler.UpdateVisualizationMode(VisualizationMode.Height);
				GD.Print("Height");
			}
		}


	}
}