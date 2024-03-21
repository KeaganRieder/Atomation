using System;
using System.Linq;
using Atomation.Resources;
using Atomation.Player;
using Atomation.Thing;

using Godot;

namespace Atomation.Map
{
	/// <summary>
	/// defines the games World map, storing and allowing of manipulation/access to the 
	/// variables that relates to the map and it's various aspects
	/// </summary>
	public partial class WorldMap : Node2D
	{
		public static VisualizationMode MapVisualIzation;
		public int MaxWorldWidth { get; set; }
		public int MaxWorldHeight { get; set; }

		public PlayerChar Player{get; private set;}

		public ChunkHandler ChunkHandler {get; private set;}
		private MapSettings mapSettings;
	
		
		//may not need the zoom Value sense it may be base on frequency?
		public WorldMap(PlayerChar player)
		{			
			Name = "World Map";
			Player = player;

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
			// ChunkHandler.WorldGenerator = mapGenerator;
			ChunkHandler.CheckChunkStatus(Player.Position);
			GD.Print("Generation Complete");
		}

		public override void _Input(InputEvent inputEvent){
			ChunkHandler.CheckChunkStatus(Player.Position);

			if (inputEvent.IsActionPressed("GenerateNewMap"))
			{
				//todo
				GD.Print("GenerateNewMap");
			}
			if (inputEvent.IsActionPressed("Default"))
			{
				ChunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Default);
				GD.Print("Default");
			}
			if (inputEvent.IsActionPressed("VisualizeMoisture"))
			{
				ChunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Moisture);
				GD.Print("Moisture");
			}
			if (inputEvent.IsActionPressed("VisualizeHeat"))
			{
				ChunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Heat);
				GD.Print("Heat");
			}
			if (inputEvent.IsActionPressed("VisualizeHeight"))
			{
				ChunkHandler.UpdateVisualizationMode(Thing.VisualizationMode.Height);
				GD.Print("Height");
			}			
		}

	}
}
