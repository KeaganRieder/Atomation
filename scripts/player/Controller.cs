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
			Vector2 mousePos = inputEvent.Position - GetViewport().CanvasTransform.Origin;

			if (inputEvent.IsActionPressed("Left Click"))
			{
				// int x = Mathf.FloorToInt(mousePos.X / MapSettings.CELL_SIZE);
				// int y = Mathf.FloorToInt(mousePos.Y / MapSettings.CELL_SIZE);
				// Vector2 mouseCords = new Vector2(x, y);
				Coordinate mouseCords = new Coordinate(mousePos);

				if (Map.ChunkHandler.GetStructure(mouseCords) != null)
				{
					Structure structure = Map.ChunkHandler.GetStructure(mouseCords);
					structure.StatSheet.GetStat(StatKeys.MAX_HEALTH).Damage(PlayerBody.StatSheet.GetStat(StatKeys.ATTACK_DAMAGE).Value);
					if (structure.StatSheet.GetStat(StatKeys.MAX_HEALTH).Value <= 0)
					{
						structure.Visible = false;
					}
					if (structure.Visible)
					{
						GD.Print($"{structure.Name} HP: {structure.StatSheet.GetStat(StatKeys.MAX_HEALTH).Value}");
					}
				}

				else if (Map.ChunkHandler.GetTerrain(mouseCords) != null)
				{
					GD.Print("terrain");
					Terrain terrain = Map.ChunkHandler.GetTerrain(mouseCords);
					if (terrain.Visible)
					{
						terrain.Visible = false;
					}
					else
					{
						terrain.Visible = true;
					}
				}
				else
				{
					GD.Print("nothing here");
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

