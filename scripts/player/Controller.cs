using Atomation.Map;
using Atomation.Resources;
using Atomation.Things;
using Godot;

namespace Atomation.PlayerChar
{
	/// <summary>
	/// class which handles controls for the game
	/// </summary>
	public partial class Controller : Node2D
	{
		private ControlBindings keyBindings;

		public WorldMap Map { get; set; }
		public Player PlayerBody { get; set; }

		public Controller()
		{
			Name = "Controller";
			Position = Vector2.Zero;
			keyBindings = new ControlBindings("default_bindings");
		}

		public override void _Input(InputEvent inputEvent)
		{
			Map.ChunkHandler.CheckChunkStatus(PlayerBody.Coordinate);
			PlayerBody.Move();

			VisualizationEvents(inputEvent);

			if (inputEvent is InputEventMouseButton mouseInputEvent)
			{
				MouseInputs(mouseInputEvent);
			}

		}

		/// <summary>
		/// handles processing input events from the mouse
		/// </summary>
		private void MouseInputs(InputEventMouseButton inputEvent)
		{
			// Input.ParseInputEvent(ie);
			// MakeInputLocal(inputEvent);
			Vector2 mousePos = inputEvent.Position - GetViewport().CanvasTransform.Origin;

			if (inputEvent.IsActionPressed("Left Click"))
			{

				int x = Mathf.FloorToInt(mousePos.X / MapSettings.CELL_SIZE);
				int y = Mathf.FloorToInt(mousePos.Y / MapSettings.CELL_SIZE);
				Vector2 terrainCords = new Vector2(x, y);
				Coordinate coordinate = new Coordinate(terrainCords);

				Terrain terrain = Map.ChunkHandler.GetTerrain(mousePos);

				if (terrain != null)
				{
					if (terrain.Visible)
					{
						terrain.Visible = false;
					}
					else
					{
						terrain.Visible = true;
					}
				}
				else if (terrain == null)
				{
					

					terrain = new Terrain(coordinate);
					terrain.ReadConfigs(DefDatabase.GetTerrainDef("Grass"));

					Map.ChunkHandler.SetTerrain(terrain);
				}
			}
			if (inputEvent.IsActionPressed("Right Click"))
			{
				int x = Mathf.FloorToInt(mousePos.X / MapSettings.CELL_SIZE);
				int y = Mathf.FloorToInt(mousePos.Y / MapSettings.CELL_SIZE);
				Vector2 cords = new Vector2(x, y);

				GD.Print($"Mouse: {PlayerBody.GetViewport().GetMousePosition()} Global {GetGlobalMousePosition()}");

				GD.Print($"converted: {cords} ");

			}

		}

		/// <summary>
		/// handles processing input events to change terrain
		/// visualization
		/// </summary>
		private void VisualizationEvents(InputEvent inputEvent)
		{

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

