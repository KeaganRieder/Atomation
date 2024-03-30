using Godot;
using Atomation.Thing;
using Atomation.Resources;
using Atomation.Utility;

namespace Atomation.Map
{
	public class GenStepLandScape : GenStep
	{
		float minValue = 1000;
		float maxValue = -1000;
		private HeightMap heightMap;
		private TemperatureMap temperatureMap;
		private MoistureMap moistureMap;
		private float deepWater;
		private float shallowWater;
		private float shoreHeight;
		private float mountainHeight;
		private float mountainBase;

		public GenStepLandScape(MapGenSettings genConfig)
		{
			heightMap = new HeightMap(genConfig, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
			temperatureMap = new TemperatureMap(genConfig, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
			moistureMap = new MoistureMap(genConfig, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);

			deepWater = genConfig.seaLevel;
			shallowWater = genConfig.seaLevel + 0.1f;
			shoreHeight = genConfig.seaLevel + 0.2f;

			mountainHeight = genConfig.mountainSize;
			mountainBase = genConfig.mountainSize - 0.1f;
		}

		public void UpdateConfigs(MapGenSettings genConfig)
		{
			heightMap.UpdateConfigs(genConfig);
			temperatureMap.UpdateConfigs(genConfig);
			moistureMap.UpdateConfigs(genConfig);
		}

		public override void RunStep(Vector2 origin, ChunkHandler chunkHandler)
		{
			heightMap.Offset = origin / MapSettings.CELL_SIZE;
			temperatureMap.Offset = origin / MapSettings.CELL_SIZE;
			moistureMap.Offset = origin / MapSettings.CELL_SIZE;

			for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
			{
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
				{
					//origin is chunk pos
					Vector2 terrainCords = new Vector2(x + origin.X, y + origin.Y);
					
					Terrain terrain = chunkHandler.GetTerrain(terrainCords);

					if (terrain == null)
					{
						//create new terrain
						terrain = new(new(x, y));
						chunkHandler.SetTerrain(terrainCords, terrain);
					}

					heightMap.CalculateHeight(x, y, terrain);
					temperatureMap.CalculateHeat(y, terrain);
					moistureMap.CalculateMoisture(x, y, terrain);

					determineTerrainType(terrain);

					terrain.UpdateGraphic(WorldMap.MapVisualIzation);
				}
			}

			// GD.Print($"moisture MIN: {moistureMap.minValue} MAX: {moistureMap.maxValue}");
			// GD.Print($"temp MIN: {temperatureMap.minValue} MAX: {temperatureMap.maxValue}");
	
		}

		/// <summary>
		/// using a terrains hight, determines if it's elevated ground, water or 
		/// just land
		/// </summary>
		private void determineTerrainType(Terrain terrain)
		{
			float height = terrain.HeightValue;

			if (height <= shoreHeight)
			{
				SetWaterType(terrain, height);
			}
			else if (height >= mountainBase)
			{
				SetElevatedTerrain(terrain, height);
			}
			else
			{
				SetBiome(terrain);
			}
		}

		/// <summary>
		/// using given tiles temperature generates a biome, and then assigns
		/// the proper information to tile 
		/// </summary>
		private void SetBiome(Terrain terrain)
		{
			Biome biome = DefDatabase.GetBiome(terrain.MoistureValue, terrain.HeatValue);

			if (biome == null)
			{
				terrain.FloorGraphic.Color = new Color(terrain.HeightValue, terrain.HeightValue, terrain.HeightValue);
				return;
			}

			TerrainDef def = biome == null ? null : biome.GetTerrain(terrain.HeightValue);
			if (def == null)
			{
				terrain.FloorGraphic.Color = Colors.Red;
				GD.Print($"{biome.Name} {terrain.HeightValue}");

				return;
			}
			terrain.ReadConfigs(def);
					}
		/// <summary>
		/// using provided terrain determines the type of water it is
		/// </summary>
		private void SetWaterType(Terrain terrain, float height)
		{
			if (height < deepWater)
			{
				terrain.FloorGraphic.Color = Colors.DarkBlue;

			}
			else if (height < shallowWater)
			{
				terrain.FloorGraphic.Color = Colors.Blue;

			}
			else if (height < shoreHeight)
			{
				terrain.FloorGraphic.Color = Colors.Yellow;
			}
		}
		/// <summary>
		/// using provided terrain determines the type of Elevated ground it is
		/// </summary>
		private void SetElevatedTerrain(Terrain terrain, float height)
		{
			if (height > mountainHeight)
			{
				terrain.FloorGraphic.Color = new Color(.05f, .05f, .05f);
			}
			else if (height > mountainBase)
			{
				terrain.FloorGraphic.Color = new Color(.3f, .3f, .3f);
			}
		}
	}
}
