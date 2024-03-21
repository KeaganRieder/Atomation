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

		public void UpdateNorthNeighbor(Terrain northNeighbor)
		{
			if (northNeighbor != null)
			{
				if (northNeighbor.SouthNeighbor != this)
				{
					NorthNeighbor = northNeighbor;
					northNeighbor.SouthNeighbor = this;
				}
				return;
			}
			else
			{
				// GD.PushError($"Error No Terrain given");
			}
		}
		public void UpdateSouthNeighbor(Terrain southNeighbor)
		{
			if (southNeighbor != null)
			{
				if (southNeighbor.NorthNeighbor != this)
				{
					SouthNeighbor = southNeighbor;
					southNeighbor.NorthNeighbor = this;
				}
				return;
			}
			else
			{
				// GD.PushError($"Error No Terrain given");
			}
		}
		public void UpdateEastNeighbor(Terrain EastNeighbor)
		{
			if (EastNeighbor != null)
			{
				if (EastNeighbor.WestNeighbor != this)
				{
					this.EastNeighbor = EastNeighbor;
					EastNeighbor.WestNeighbor = this;
				}
				return;
			}
			else
			{
				// GD.PushError($"Error No Terrain given");
			}
		}
		public void UpdateWestNeighbor(Terrain westNeighbor)
		{
			if (westNeighbor != null)
			{
				if (westNeighbor.NorthNeighbor != this)
				{
					WestNeighbor = westNeighbor;
					westNeighbor.EastNeighbor = this;
				}
				return;
			}
			else
			{
				// GD.PushError($"Error No Terrain given");
			}
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
