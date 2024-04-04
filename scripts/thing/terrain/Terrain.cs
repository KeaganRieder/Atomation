using Godot;
using Atomation.Map;
using Atomation.Resources;
using System;

namespace Atomation.Thing
{
	/// <summary>
	/// defines what a terrain object is in the game world 
	/// </summary>
	public partial class Terrain : CompThing
	{
		public float HeightValue { get; set; }
		public float HeatValue { get; set; }
		public float MoistureValue { get; set; }

		public FloorGraphics FloorGraphic { get; set; }

		public Terrain NorthNeighbor { get; private set; }
		public Terrain SouthNeighbor { get; private set; }
		public Terrain WestNeighbor { get; private set; }
		public Terrain EastNeighbor { get; private set; }

		public Terrain(Coordinate coord)
		{
			coordinate = coord;
			Position = coordinate.WorldPosition*MapSettings.CELL_SIZE;

			FloorGraphic = new FloorGraphics(this,coord.WorldPosition);
		}


		/// <summary>
		/// reading the configuration data for the given tile
		/// and setting it for anything in which is needing
		/// configuration at current call
		/// </summary>
		public void ReadConfigs(CompThingDef config)
		{
			Name = config.Name +coordinate.ToString();
			Description = config.Description;
			stats = config.FormatStats();
			modifiers = config.FormatStatModifers();
			FloorGraphic.ConfigureGraphic(config.GraphicData);
		}

		public void UpdateNorthNeighbor(Terrain northNeighbor)
		{
			if (northNeighbor != null)
			{
				NorthNeighbor = northNeighbor;

				if (northNeighbor.SouthNeighbor != this)
				{
					northNeighbor.SouthNeighbor = this;
				}
				return;
			}
		}
		public void UpdateSouthNeighbor(Terrain southNeighbor)
		{
			if (southNeighbor != null)
			{
				SouthNeighbor = southNeighbor;

				if (southNeighbor.NorthNeighbor != this)
				{
					southNeighbor.NorthNeighbor = this;
				}
				return;
			}
		}
		public void UpdateEastNeighbor(Terrain eastNeighbor)
		{
			if (eastNeighbor != null)
			{
				EastNeighbor = eastNeighbor;

				if (eastNeighbor.WestNeighbor != this)
				{
					eastNeighbor.WestNeighbor = this;
				}
				return;
			}
		}
		public void UpdateWestNeighbor(Terrain westNeighbor)
		{
			if (westNeighbor != null)
			{
				WestNeighbor = westNeighbor;

				if (westNeighbor.EastNeighbor != this)
				{
					westNeighbor.EastNeighbor = this;
				}
				return;
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
