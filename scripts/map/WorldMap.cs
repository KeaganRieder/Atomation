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
		

		//map components
		private WorldGenerator mapGenerator;
		private ChunkHandler chunkHandler;
		private Node2D PlayerNode;

		public WorldMap()
		{
			MaxWorldWidth = 256;
			MaxWorldHeight = 256;
			GenConfigs genConfig = new GenConfigs() 
			{
				worldBounds = new Vector2I(MaxWorldWidth, MaxWorldHeight),
				seaLevel = -0.1f,
				mountainSize = 0.2f,
				elevationMapConfigs = new NoiseMapConfig()
				{
					seed = 0,
					octaves = 5,
					zoom = 1f,
					frequency = 0.01f,
					lacunarity = 2,
					persistence = 0.5f,
				},
				moistureMapConfigs = new NoiseMapConfig()
				{
					seed = 0,
					octaves = 4,
					zoom = 1f,
					frequency = 0.01f,
					lacunarity = 2,
					persistence = 0.5f,
				},
				heatMapConfigs = new NoiseMapConfig()
				{
					seed = 0,
					octaves = 4,
					zoom = 1f,
					frequency = 0.01f,
					lacunarity = 2,
					persistence = 0.5f,
				}
			};

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
			// mapGenerator.GenerateMap(this);

			GD.Print("Generation Complete");
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
