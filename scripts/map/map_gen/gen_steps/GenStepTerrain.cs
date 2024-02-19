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
		private int riverCount;		

		public GenStepTerrain(GenConfigs genConfig){
			
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
					GenerateBiomeMap(terrain);

					terrain.Display(TerrainDisplayMode.Default); //this is temporary
				}
			}
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
