using Godot;
using Atomation.Thing;
using Atomation.Resources;

namespace Atomation.Map
{
	public class GenStepLandScape : GenStep
	{
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
			chunkPos = origin*MapSettings.CELL_SIZE;
			heightMap.Offset = origin;
			temperatureMap.Offset = origin;
			moistureMap.Offset = origin;

			for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
			{
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
				{
					Terrain terrain = new Terrain(new Coordinate(x,y,origin));

					//calculate generation values
					heightMap.CalculateHeight(x, y, terrain);
					temperatureMap.CalculateHeat(y, terrain);
					moistureMap.CalculateMoisture(x, y, terrain);

					determineTerrainType(terrain);

					chunkHandler.SetTerrain(x, y, chunkPos,terrain);

					terrain.UpdateGraphic(WorldMap.MapVisualIzation);
				}
			}
			
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
				terrain.ReadConfigs(DefDatabase.GetTerrainConfig("Deep Water"));
			}
			else if (height < shallowWater)
			{
				terrain.ReadConfigs(DefDatabase.GetTerrainConfig("Shallow Water"));
			}
			else if (height < shoreHeight)
			{
				terrain.ReadConfigs(DefDatabase.GetTerrainConfig("Sand"));
			}
		}

		/// <summary>
		/// using provided terrain determines the type of Elevated ground it is
		/// </summary>
		private void SetElevatedTerrain(Terrain terrain, float height)
		{
			if (height > mountainHeight)
			{
				terrain.ReadConfigs(DefDatabase.GetTerrainConfig("Gravel"));
			}
			else if (height > mountainBase)
			{
				terrain.ReadConfigs(DefDatabase.GetTerrainConfig("Slate"));
			}
		}
	}
}
