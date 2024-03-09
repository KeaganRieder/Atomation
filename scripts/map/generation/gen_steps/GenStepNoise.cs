// using System;
// using Godot;
// using Atomation.Map;
// using Atomation.Thing;
// using Atomation.Utility;


// namespace Atomation.Map
// {
// 	/// <summary>
// 	/// defines the generation step for generating noise maps, 
// 	/// which are pass to generation steps following, and help determine
// 	/// aspects in the game world
// 	/// </summary>
// 	public class GenStepNoise : GenStep
// 	{ 
// 		//general info
// 		private int equatorHeight;
// 		private float scale;

// 		//noise map info
// 		private SimplexNoiseMap elevationMap;
// 		private SimplexNoiseMap moistureMap;
// 		private SimplexNoiseMap heatMap;

// 		// private Dictionary<Vector2, Terrain> terrainTiles;

// 		public GenStepNoise(MapGenSettings genConfig)
// 		{
// 			worldMaxWidth = genConfig.worldSize.X;
// 			worldMaxHeight = genConfig.worldSize.Y;
// 			equatorHeight = 0;// worldMaxHeight/2;//genConfig.worldSize.Y/2;

// 			scale = genConfig.scale; 

// 			elevationMap = new SimplexNoiseMap(genConfig.elevationSettings, genConfig.seed);
// 			moistureMap = new SimplexNoiseMap(genConfig.moistureSettings, genConfig.seed);
// 			heatMap = new SimplexNoiseMap(genConfig.temperatureSettings, genConfig.seed);
// 		}

// 		/// <summary>
// 		/// used to get the elevation noise map in order to change
// 		/// individual config Data
// 		/// </summary>
// 		public SimplexNoiseMap GetHeightMap()
// 		{
// 			return elevationMap;
// 		}
// 		/// <summary>
// 		/// used to get the moisture noise map in order to change
// 		/// individual config Data
// 		/// </summary>
// 		public SimplexNoiseMap GetMoistureMap()
// 		{
// 			return moistureMap;
// 		}
// 		/// <summary>
// 		/// used to get the heat noise map in order to change
// 		/// individual config Data
// 		/// </summary>
// 		public SimplexNoiseMap GetHeatMap()
// 		{
// 			return heatMap;
// 		}

// 		/// <summary>
// 		/// handling running the step, using width and height that's based on
// 		/// chunk info
// 		/// </summary>
// 		public override void RunStep(Vector2 origin, ChunkHandler chunkHandler)
// 		{
//             elevationMap.Offset = Vector2.Zero;
//             moistureMap.Offset = Vector2.Zero;
//             heatMap.Offset = Vector2.Zero;

// 			float[,] equatorHeat = GenerateEquatorHeat(origin, Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);

// 			for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
// 			{
// 				for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
// 				{                    
// 					SampleCords(x, y,origin,scale, out float sampleX, out float sampleY);

// 					float height = GetElevationValue(sampleX, sampleY);
// 					// GD.Print(height);
// 					Vector2 cords = new(x, y);

// 					Terrain terrain = new(cords);
// 					terrain.HeightValue = height;
// 					terrain.HeatValue = getEquatorHeat(y+Mathf.RoundToInt(origin.Y));
// 					//GetHeatValue(x, y, equatorHeat,heatMap[sampleX, sampleY], height);
// 					terrain.MoistureValue = GetMoistureValue(sampleX,sampleY,height);
					
// 					CordConversion.SampleChunkPos(origin, x, y, out sampleX, out sampleY);
// 					chunkHandler.Set(Mathf.RoundToInt(sampleX), Mathf.RoundToInt(sampleY), terrain);
// 				}
// 			}
// 		}


// 		/// <summary>
// 		/// generating elevation map which outlines the primary parts of the terrain
// 		/// this generates values generally between -.6 and .5
// 		/// </summary>
// 		private float GetElevationValue(float x, float y)
// 		{
// 			float heightVal = elevationMap[x, y];

// 			return Mathf.Clamp(heightVal, -1, 1);
// 		} 

// 		/// <summary>
// 		/// handles generating the equator heat map
// 		/// which decrease in heat the further away form teh center point
// 		/// it gets
// 		/// </summary>
// 		private float[,] GenerateEquatorHeat(Vector2 origin, int width, int height)
// 		{
// 			float[,] noiseMap = new float[width, height];

// 			// decide the temperature based on distance from central point/equator
// 			for (int y = 0; y < height; y++)
// 			{
// 				float sampleY = y + origin.Y;

// 				//calculate noise value based on it's distance
// 				// well also ensuring that it's within the bounds
// 				float noise = Math.Abs(sampleY - equatorHeight) / 32;

// 				for (int x = 0; x < width; x++)
// 				{
// 					//apply the noise value for all points at this Row
// 					noiseMap[x, y] = Mathf.Clamp(noise, 0, 1);
// 				}
// 			}
// 			return noiseMap;
// 		}

// private float getEquatorHeat(int y){
// 	  	float latitude = Mathf.Abs(y - equatorHeight);
//         float equatorHeat = latitude / worldMaxHeight;
// 		return equatorHeat;
// }
// 		/// <summary>
// 		/// gets the heat value for given cords. heat is based on distance form Equator,
// 		/// heat map value, and the height
// 		/// </summary>
// 		private float GetHeatValue(int x, int y, float[,] equatorHeat, float heat, float height)
// 		{
// 			float globalAvgTemp = 2;
// 			float heatVal = equatorHeat[x, y] * (heat*globalAvgTemp);
		
// 			heatVal += Mathf.Sin(Mathf.Abs(height)) * height;
			
// 			return Mathf.Clamp(heatVal, -1, 1f);
// 		}

// 		/// <summary>
// 		/// get moisture value at given cords
// 		/// </summary>
// 		private float GetMoistureValue(float x, float y, float height)
// 		{   		
// 			//maybe make global avg for all, make between 0.1 and 1 
// 			float globalMoisture = 1f;
// 			float moistureVal = Mathf.Abs(moistureMap[x,y]) * globalMoisture;
// 			moistureVal += Mathf.Abs(height) * Mathf.Sin(Mathf.Abs(height)); 
// 			// moistureVal = ;

// 			return  Mathf.Clamp(moistureVal, 0, 1);
// 		}



// 	}
// }
