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
		private float seaLevel; //no sea = 
		private float mountainSize; // no mountains = 1

		//terrain height indexes		

		public GenStepTerrain(GenConfigs genConfig){
			seaLevel = genConfig.seaLevel;
			mountainSize = genConfig.seaLevel;
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
					SetTerrainType(terrain);

					terrain.Display(TerrainDisplayMode.Heat); //this is temporary
				}
			}
		}

		/// <summary>
		/// 
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
			// GD.Print("read");
			terrain.ReadConfigs(DefResources.ReadTerrainConfig(terrainId));
			
		}
	}
}
