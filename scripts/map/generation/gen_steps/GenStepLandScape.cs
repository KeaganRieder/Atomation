namespace Atomation.Map;

using Godot;
using Atomation.Things;
using Atomation.Resources;

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
		chunkPos = origin * MapSettings.CELL_SIZE;
		heightMap.Offset = origin;
		temperatureMap.Offset = origin;
		moistureMap.Offset = origin;

		for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
		{
			for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
			{
				Terrain terrain = new Terrain(new Coordinate(x, y, origin));

				//calculate generation values
				heightMap.CalculateHeight(x, y, terrain);
				temperatureMap.CalculateHeat(y, terrain);
				moistureMap.CalculateMoisture(x, y, terrain);

				SetLandscape(terrain, chunkHandler);

				terrain.UpdateGraphic(WorldMap.MapVisualIzation);
			}
		}

	}

	/// <summary>
	/// using a terrains hight, determines if it's elevated ground, water or 
	/// just land
	/// </summary>
	private void SetLandscape(Terrain terrain, ChunkHandler chunkHandler)
	{
		float height = terrain.HeightValue;
		Structure structure = null;

		if (height > mountainBase)
		{
			SetElevationType(terrain, out structure);
		}
		else if (height <= shoreHeight)
		{
			SetWater(terrain);
		}
		else
		{
			SetBiome(terrain);
		}

		chunkHandler.SetTerrain(terrain);
		chunkHandler.SetStructure(structure);


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
			terrain.Graphic.DefaultColor = Colors.Red;

			return;
		}

		TerrainDef def = biome == null ? null : biome.GetTerrain(terrain.HeightValue);
		if (def == null)
		{
			terrain.Graphic.DefaultColor = Colors.Red;
			GD.Print($"{biome.Name} {terrain.HeightValue}");

			return;
		}
		terrain.ReadConfigs(def);
	}

	/// <summary>
	/// sets the terrain to be a typeof water depending on how it's height 
	/// </summary>
	private void SetWater(Terrain terrain)
	{
		float height = terrain.HeightValue;

		if (height < deepWater)
		{
			terrain.ReadConfigs(DefDatabase.GetTerrainDef("Deep Water"));
		}
		else if (height < shallowWater)
		{
			terrain.ReadConfigs(DefDatabase.GetTerrainDef("Shallow Water"));
		}
		else
		{
			terrain.ReadConfigs(DefDatabase.GetTerrainDef("Sand"));
		}

	}

	/// <summary>
	/// checks the provided terrains elevation to see if it's water or if its mountain.
	/// if it's mountain or water return true, otherwise false saying it's just normal
	/// ground
	/// </summary>
	private void SetElevationType(Terrain terrain, out Structure structure)
	{
		float height = terrain.HeightValue;

		//it's some point in a mountain
		if (height > mountainBase && height < mountainHeight)
		{
			terrain.ReadConfigs(DefDatabase.GetTerrainDef("Gravel"));
			structure = null;
		}
		else if (height > mountainHeight)
		{
			terrain.ReadConfigs(DefDatabase.GetTerrainDef("Slate"));
			structure = new Structure(terrain.Coordinate);
			structure.ReadConfigs(DefDatabase.GetStructureDef("Slate Wall"));

		}
		else
		{
			structure = null;
		}
	}
}

