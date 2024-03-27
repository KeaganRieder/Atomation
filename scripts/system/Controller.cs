using Atomation.Map;
using Atomation.Player;
using Godot;

namespace Atomation
{
    /// <summary>
    /// class which handles controls for the game
    /// </summary>
    public partial class Controller : Node
    {
        private KeyBindings keyBindings;

        public WorldMap Map { get; set; }
	    public PlayerChar Player { get; set; }

        public Controller()
        {
            Name = "Controller";
            keyBindings = new KeyBindings("default_bindings");
        }

        public override void _Input(InputEvent inputEvent)
        {
            Map.ChunkHandler.CheckChunkStatus(Player.Position); 
            Player.Move();

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