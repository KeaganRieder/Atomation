using System;
using System.Linq;
using Atomation.Resources;
using Godot;

namespace Atomation.Map
{
	/// <summary>
	/// defines the games World map, storing and allowing of manipulation/access to the 
	/// variables that relates to the map and it's various aspects
	/// </summary>
	public partial class WorldMap : Node2D
	{
		public int MaxWorldWidth { get; set; }
		public int MaxWorldHeight { get; set; }

		private MapSettings mapSettings;

		//map components
		private WorldGenerator mapGenerator;
		private ChunkHandler chunkHandler;
		private Node2D PlayerNode;
		
		//may not need the zoom Value sense it may be base on frequency?
		public WorldMap()
		{
			Name = "World Map";
			
			mapSettings = FileManger.ReadJsonFile<MapSettings>(FileManger.CONFIGS, "map_settings");
			
			mapGenerator = new WorldGenerator(mapSettings.genSettings);
			chunkHandler = new ChunkHandler(this);

			PlayerNode = new Node2D() { Name = "player" };
			PlayerNode.AddChild(new ColorRect() { Color = new Color(100, 100, 100), Size = new Vector2(16, 16) });
			AddChild(PlayerNode);
		}

		/// <summary>
		/// runs upon node creation
		/// </summary>
		public override void _Ready()
		{
			base._Ready();
			
			GD.Print("Generating Map");
			chunkHandler.WorldGenerator = mapGenerator;
			chunkHandler.UpdateRenderedChunks(PlayerNode.Position);
			GD.Print("Generation Complete");
		}

		public override void _Input(InputEvent inputEvent){
			if (inputEvent.IsActionPressed("GenerateNewMap"))
			{
				//todo
				GD.Print("GenerateNewMap");
			}
			if (inputEvent.IsActionPressed("Default"))
			{
				chunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Default);
				GD.Print("Default");
			}
			if (inputEvent.IsActionPressed("VisualizeMoisture"))
			{
				chunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Moisture);
				GD.Print("Moisture");
			}
			if (inputEvent.IsActionPressed("VisualizeHeat"))
			{
				chunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Heat);
				GD.Print("Heat");
			}
			if (inputEvent.IsActionPressed("VisualizeHeight"))
			{
				chunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Height);
				GD.Print("Height");
			}			
		}

	}
}
