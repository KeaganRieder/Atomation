using System.Collections.Generic;
using Godot;
using Atomation.Map;
using Atomation.Resources;

/// <summary>
/// terrain tiles diplay mode
/// Default = graphic
/// Height = height map
/// Heat = heat map
/// Moisture = moisture map
/// </summary>
public enum TerrainDisplayMode
{
	Default = 0,
	Height = 1,
	Heat = 2,
	Moisture = 3,
}

namespace Atomation.Thing
{
	/// <summary>
	/// defines what a terrain object is in the game world 
	/// </summary>
	public partial class Terrain : CompThing
	{
		private float heightValue;
		private float heatValue;
		private float moistureValue;


		private Gradient heatGradient;
		private ColorRect colorRect; //this is temporary and will be changed

		private bool water;

		//constructors
		public Terrain(Vector2 cords)
		{
			string name = $"Tile {cords}";
			Vector2 position = cords * WorldMap.CELL_SIZE;

			node = new Node2D()
			{
				Name = name,
				Position = position,
			};
			colorRect = new ColorRect()
			{
				Size = new Vector2(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE),
			};

			node.AddChild(colorRect);
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
			stats = config.CreateStats();
			Graphic = new FloorGraphics(config.GraphicData);
		}

		//getters and setters
		public override Resources.Graphic Graphic { get => graphic; set { graphic = value; } }

		public float HeightValue { get => heightValue; set { heightValue = value; } }
		public float HeatValue { get => heatValue; set { heatValue = value; } }
		public float MoistureValue { get => moistureValue; set { moistureValue = value; } }

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

		
        //
        // functions
        //

		public void UpdateNorthNeighbor(ChunkHandler chunkHandler)
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
		public void UpdateSouthNeighbor(ChunkHandler chunkHandler)
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
        public void UpdateEastNeighbor(ChunkHandler chunkHandler)
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
        public void UpdateWestNeighbor(ChunkHandler chunkHandler)
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

        public void Display(TerrainDisplayMode displayMode)
		{
			//todo move function to graphic class
			Color color;
			if (displayMode == TerrainDisplayMode.Default)
			{
				color = DefaultColor(heightValue); //todo make graphic rather then color
			}
			else if (displayMode == TerrainDisplayMode.Height)
			{
				color = HeightColor(heightValue);
			}
			else if (displayMode == TerrainDisplayMode.Heat)
			{
				color = HeatColor(heatValue);
			}
			else
			{
				color = MoistureColor(moistureValue);
			}
			colorRect.Color = color;
		}
		private Color DefaultColor(float value)
		{
			// // value is in range of -.6 and .5
			// if (value < -.5 )
			// {
			// 	//deep water
			// 	return new Color(Colors.DarkBlue);
			// }
			// else if (value < -.3)
			// {
			//    //shallow water
			// 	return new Color(Colors.Blue);
			// }
			// else if (value < -.2)
			// {
			//    //sand
			//    return new Color(Colors.Yellow);
			// }
			// else if (value < .1)
			// {
			// 	//grass
			// 	return new Color(Colors.Green);
			// }
			// else if (value < .2)
			// {
			// 	// forest
			// 	return new Color(Colors.DarkGreen);
			// }
			// else if (value< .3)
			// {
			// 	//rockey terrain
			// 	return new Color(Colors.Gray);
			// }
			// else if (value< .6)
			// {
			// 	//rockey terrain
			// 	return new Color(Colors.DarkGray);
			// }
			//mountain
			// return new Color(Colors.Black);

			return graphic.Color;


		}
		private Color HeatColor(float value)
		{
			heatGradient = new Gradient();
			heatGradient.AddPoint(0f, Colors.DarkRed);
			heatGradient.AddPoint(0.18f, Colors.Orange);
			heatGradient.AddPoint(0.3f, Colors.Yellow); //cold
			heatGradient.AddPoint(0.5f, Colors.Green);
			heatGradient.AddPoint(0.6f, Colors.Cyan);
			heatGradient.AddPoint(0.7f, Colors.Blue); //coldest
			heatGradient.AddPoint(.9f, Colors.DarkBlue);

			if (value < .9)
			{
				return heatGradient.Sample(value);
			}
			else
			{
				return new Color(Colors.DarkBlue);
			}

		}
		private Color HeightColor(float value)
		{
			return new Color(value, value, value);
		}
		private Color MoistureColor(float value)
		{
			//todo
			if (value < 0.27)
			{
				return new Color(Colors.Orange);
			}
			if (value < 0.4)
			{
				return new Color(Colors.Yellow);
			}
			if (value < 0.6)
			{
				return new Color(Colors.Green);
			}
			else if (value < 0.8)
			{
				return new Color(Colors.Blue);
			}
			else
			{
				return new Color(Colors.DarkBlue);
			}
		}
	}
}
