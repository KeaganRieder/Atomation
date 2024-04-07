using System;
using System.Linq;
using Atomation.Resources;
using Atomation.PlayerChar;
using Atomation.Things;

using Godot;

namespace Atomation.Map
{
	/// <summary>
	/// terrain tiles display mode
	/// Default = graphic
	/// Height = height map
	/// Heat = heat map
	/// Moisture = moisture map
	/// </summary>
	public enum VisualizationMode
	{
		Undefined = -1,
		Default = 0,
		Height = 1,
		Heat = 2,
		Moisture = 3,
		Biome = 4,
	}

	/// <summary>
	/// defines the games World map, storing and allowing of manipulation/access to the 
	/// variables that relates to the map and it's various aspects
	/// </summary>
	public partial class WorldMap : Node2D
	{
		public static VisualizationMode MapVisualIzation;
		public int MaxWorldWidth { get; set; }
		public int MaxWorldHeight { get; set; }

		public Player Player { get; private set; }

		public ChunkHandler ChunkHandler { get; private set; }
		private MapSettings mapSettings;

		public WorldMap(Player Player)
		{
			Name = "World Map";
			this.Player = Player;
			MapVisualIzation = VisualizationMode.Default;

			mapSettings = FileManger.ReadJsonFile<MapSettings>(FilePath.CONFIG_FOLDER, "map_settings");

			WorldGenerator.Initialize(mapSettings.genSettings);
			ChunkHandler = new ChunkHandler(this);
		}

		/// <summary>
		/// runs upon node creation
		/// </summary>
		public override void _Ready()
		{
			base._Ready();

			GD.Print("Generating Map");
			ChunkHandler.CheckChunkStatus((Vector2)Player.Position);
			GD.Print("Generation Complete");
		}

		// public override void _Input(InputEvent inputEvent)
		// {
		// 	bool moved = inputEvent.IsActionPressed("MoveLeft") || inputEvent.IsActionPressed("MoveRight") ||
		// 	inputEvent.IsActionPressed("MoveUp") || inputEvent.IsActionPressed("MoveDown");

		// 	if (moved)
		// 	{
		// 		ChunkHandler.CheckChunkStatus(Player.Position);
		// 	}
		// 	if (inputEvent.IsActionPressed("Default"))
		// 	{
		// 		ChunkHandler.UpdateVisualizationMode(VisualizationMode.Default);
		// 		GD.Print("Default");
		// 	}
		// 	if (inputEvent.IsActionPressed("VisualizeMoisture"))
		// 	{
		// 		ChunkHandler.UpdateVisualizationMode(VisualizationMode.Moisture);
		// 		GD.Print("Moisture");
		// 	}
		// 	if (inputEvent.IsActionPressed("VisualizeHeat"))
		// 	{
		// 		ChunkHandler.UpdateVisualizationMode(VisualizationMode.Heat);
		// 		GD.Print("Heat");
		// 	}
		// 	if (inputEvent.IsActionPressed("VisualizeHeight"))
		// 	{
		// 		ChunkHandler.UpdateVisualizationMode(VisualizationMode.Height);
		// 		GD.Print("Height");
		// 	}
		// }

	}
}
