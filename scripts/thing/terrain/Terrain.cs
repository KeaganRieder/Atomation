using System.Collections.Generic;
using Godot;
using Atomation.Map;
using Atomation.Resources;
using System;

namespace Atomation.Thing
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
	/// defines what a terrain object is in the game world 
	/// </summary>
	public class Terrain : CompThing
	{
		public float HeightValue { get; set; }
		public float HeatValue { get; set; }
		public float MoistureValue { get; set; }
		public FloorGraphics FloorGraphic { get; set; }


		/// <summary>
		/// the neighbor Below
		/// </summary>
		public Terrain NorthNeighbor { get; private set; }
		/// <summary>
		/// the neighbor above
		/// </summary>
		public Terrain SouthNeighbor { get; private set; }
		/// <summary>
		/// the neighbor to the right
		/// </summary>
		public Terrain WestNeighbor { get; private set; }
		/// <summary>
		/// the neighbor to the left
		/// </summary>
		public Terrain EastNeighbor { get; private set; }

		//constructors
		public Terrain(Vector2 cords)
		{
			string name = $"Tile {cords}";
			Vector2 position = cords * MapSettings.CELL_SIZE;

			ThingNode = new Node2D()
			{
				Name = name,
				Position = position,
			};

			FloorGraphic = new FloorGraphics(ThingNode);
		}

		/// <summary>
		/// reading the configuration data for the given tile
		/// and setting it for anything in which is needing
		/// configuration at current call
		/// </summary>
		public void ReadConfigs(TerrainDef config)
		{
			name = config.Name;
			description = config.Description;
			stats = config.FormatStats();
			modifiers = config.FormatStatModifers();
			FloorGraphic.ConfigureGraphic(config.GraphicData);
		}

		public void UpdateNorthNeighbor(ChunkHandlerOld chunkHandler)
		{
			GlobalPosition(out int x, out int y);
			Terrain terrain = chunkHandler.GetTerrain(x, y - 1);

			if (terrain == null)
			{
				return;
			}
			terrain.SouthNeighbor ??= this;

			NorthNeighbor = terrain;

		}
		public void UpdateSouthNeighbor(ChunkHandlerOld chunkHandler)
		{
			GlobalPosition(out int x, out int y);
			Terrain terrain = chunkHandler.GetTerrain(x, y + 1);
			if (terrain == null)
			{
				return;
			}
			terrain.NorthNeighbor ??= this;

			SouthNeighbor = terrain;
		}
		public void UpdateEastNeighbor(ChunkHandlerOld chunkHandler)
		{
			GlobalPosition(out int x, out int y);
			Terrain terrain = chunkHandler.GetTerrain(x - 1, y);
			if (terrain == null)
			{
				return;
			}
			terrain.WestNeighbor ??= this;

			EastNeighbor = terrain;
		}
		public void UpdateWestNeighbor(ChunkHandlerOld chunkHandler)
		{
			GlobalPosition(out int x, out int y);
			Terrain terrain = chunkHandler.GetTerrain(x + 1, y);
			if (terrain == null)
			{
				return;
			}
			terrain.EastNeighbor ??= this;

			WestNeighbor = terrain;
		}

		public void UpdateGraphic(VisualizationMode displayMode)
		{
			if (displayMode == VisualizationMode.Default)
			{
				FloorGraphic.DefaultGraphic();
			}
			else if (displayMode == VisualizationMode.Height)
			{
				FloorGraphic.HeightGraphic(HeightValue);
			}
			else if (displayMode == VisualizationMode.Heat)
			{
				FloorGraphic.HeatGraphic(HeatValue);
			}
			else
			{
				FloorGraphic.MoistureGraphic(MoistureValue);
			}
		}

	}
}
