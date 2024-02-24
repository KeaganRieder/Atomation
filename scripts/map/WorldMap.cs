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
		public const float CELL_SIZE = 16;
		public int MaxWorldWidth { get; set; }
		public int MaxWorldHeight { get; set; }

		GenConfigs genConfig;

		//map components
		private WorldGenerator mapGenerator;
		private ChunkHandler chunkHandler;
		private Node2D PlayerNode;
		
		//may not need the zoom Value sense it may be base on frequency?
		public WorldMap()
		{
			Name = "World Map";
			// MaxWorldWidth = 256;
			// MaxWorldHeight = 256;
		
			genConfig = JsonReader.ReadJson<GenConfigs>(FileManger.CONFIGS +"map_configs.json");
			
			mapGenerator = new WorldGenerator(genConfig);
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
				chunkHandler.UpdateVisualizationMode(TerrainDisplayMode.Default);
				GD.Print("Default");
			}
			if (inputEvent.IsActionPressed("VisualizeMoisture"))
			{
				chunkHandler.UpdateVisualizationMode(TerrainDisplayMode.Moisture);
				GD.Print("Moisture");
			}
			if (inputEvent.IsActionPressed("VisualizeHeat"))
			{
				chunkHandler.UpdateVisualizationMode(TerrainDisplayMode.Heat);
				GD.Print("Heat");
			}
			if (inputEvent.IsActionPressed("VisualizeHeight"))
			{
				chunkHandler.UpdateVisualizationMode(TerrainDisplayMode.Height);
				GD.Print("Height");
			}
			
		}

		/// <summary>
		/// runs every frame
		/// </summary>
		public override void _Process(double delta)
		{
			base._Process(delta);
			// chunkHandler.UpdateRenderedChunks(PlayerNode.Position);

		}

		// public void finalizedWorld
	}
}
