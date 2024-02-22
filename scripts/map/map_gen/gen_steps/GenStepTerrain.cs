using Godot;
using Atomation.Thing;
using Atomation.Resources;


namespace Atomation.Map
{
	/// <summary>
	/// defines the 2nd generation step for generating the terrain.
	/// this include deciding lakes, ocean and where rivers are, mountains,
	/// biomes and general landmasses are
	/// </summary>
	public class GenStepTerrain : GenStep
	{
		// terrain class heights
		private float deepWater = 0.1f;
		private float shallowWater = -0.6f;
		private float shore = -0.5f;

		/// <summary>
		/// determines mountains
		/// this value changes based on configs, 
		/// </summary>
		private float mountain = 0.4f;
		private float rockyGround = 0.5f;


		public GenStepTerrain(GenConfigs genConfig){

			// seaLevel = genConfig.seaLevel;
			// shallowWater += genConfig.seaLevel;
			// shore += genConfig.seaLevel;

			// mountain -= genConfig.mountainSize;
			// rockyGround -= genConfig.mountainSize;
		}

		/// <summary>
		/// main function that is called in order to execute step and it's parts
		/// </summary>
		public override void RunStep(Vector2 origin, ChunkHandler chunkHandler){
			for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
			{
				for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
				{
					SampleChunkPos(origin, x, y, out float sampleX, out float sampleY);
					Terrain terrain = chunkHandler.GetTerrain(Mathf.RoundToInt(sampleX), Mathf.RoundToInt(sampleY));
					// GenerateBiomeMap(terrain);
					GenerateElevation(terrain);

					terrain.Display(TerrainDisplayMode.Heat); //this is temporary
				}
			}
		}

		/// <summary>
		/// Updates some values in noise maps to be correct given there
		/// elevation
		/// </summary>
		private void GenerateElevation(Terrain terrain){
			FloorGraphics graphic;
			if (terrain.HeightValue<deepWater)
			{
				graphic = new FloorGraphics(new Color(0,0,0));
				terrain.Graphic = graphic;
			}
			else if (terrain.HeightValue>mountain)
			{
				graphic = new FloorGraphics(new Color(1,1,1));
				terrain.Graphic = graphic;
			}
			else
			{
				graphic = new FloorGraphics(new Color(terrain.HeightValue,terrain.HeightValue,terrain.HeightValue));
				terrain.Graphic = graphic;
			}
			
			
			// if (terrain.HeightValue < deepWater)
			// {
			// 	// terrain.MoistureValue +=Mathf.Abs(8 *terrain.HeightValue);
			// }
			// else if (terrain.HeightValue < shallowWater)
			// {
			// 	// terrain.HeightValue =0;
			// 	terrain.MoistureValue +=Mathf.Abs(3 *terrain.HeightValue);
			// }
			// else if (terrain.HeightValue < shore)
			// {
			// 	// terrain.HeightValue = .5f;
			// 	// terrain.MoistureValue +=Mathf.Abs(1 *terrain.HeightValue);
			// }
			// // else{
			// // 	terrain.HeightValue = 1;
			// // }
		}

		public void GenerateBiomeMap(Terrain terrain){
			float moisture = terrain.MoistureValue;
			float temperate = terrain.HeatValue;
			float height = terrain.HeightValue;
			Biome biome = DefDatabase.ReadBiome(moisture,temperate);
			FloorGraphics graphic;

			if (height < -.5)
			{
				//deep water
				terrain.ReadConfigs(DefDatabase.ReadTerrainConfig("DeepOcean"));
			}
			else if (height < -.3)
			{
				//shallow water
				terrain.ReadConfigs(DefDatabase.ReadTerrainConfig("ShallowOcean"));
			}
			// else if(height < .5)
			// {
			// 	//mountain
			// 	terrain.ReadConfigs(DefDatabase.ReadTerrainConfig("Slate"));
			// }
			else if(biome != null){
				graphic = new FloorGraphics(biome.Color);
				terrain.Graphic = graphic;
			}
			else
			{
				// GD.Print($"Moisture:{moisture},temperate:{temperate}");
				graphic = new FloorGraphics(Colors.Gray);
				terrain.Graphic = graphic;
			}
			
			
			
		}

		/// <summary>
		/// Work in progress
		/// </summary>
		private void SetTerrainType(Terrain terrain){
			float height = terrain.HeightValue;
			string terrainId;

			if (height < -.5)
			{
				//deep water
				terrainId = "DeepOcean";
			}
			else if (height < -.3)
			{
				//shallow water
				terrainId = "ShallowOcean";
			}
			else if (height < -.2)
			{
				//sand
				terrainId = "Sand";
			}
			else if (height < .2)
			{
				//grass
				terrainId = "Grass";
			}
			else if (height < .3)
			{
				//soil
				terrainId = "Soil";
			}
			else if (height < .4)
			{
				//gravel
				terrainId = "Gravel";
			}
			else
			{
				//mountain
				terrainId = "Slate";
			}
			terrain.ReadConfigs(DefDatabase.ReadTerrainConfig(terrainId));
			
		}
	}
}
