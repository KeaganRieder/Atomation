using System.Collections.Generic;
using Godot;
using Atomation.Thing;

namespace Atomation.Map
{
	/// <summary>
	/// ChunkHandler, stores and handles chunks, allow for 
	/// interaction with different element in a chunk
	/// </summary>
	public class ChunkHandler
	{
		private readonly int visibleChunks;

		private List<Chunk> lastUpdatedChunks;
		private Dictionary<Vector2, Chunk> chunkArray;

		private Node2D worldMap;

		public ChunkHandler(Node2D map)
		{
			worldMap = map;

			visibleChunks = Mathf.FloorToInt(MapSettings.MAX_LOAD_DIST / Chunk.CHUNK_SIZE);//  - 1;

			lastUpdatedChunks = new List<Chunk>();
			chunkArray = new Dictionary<Vector2, Chunk>();
		}

		/// <summary>
		/// gets the position of the chunk aligned on the tile grid
		/// </summary>
		private Vector2 GetChunkWorldPosition(Vector2 worldPosition)
		{
			int x = Mathf.RoundToInt(worldPosition.X * Chunk.CHUNK_SIZE * MapSettings.CELL_SIZE);
			int y = Mathf.RoundToInt(worldPosition.Y * Chunk.CHUNK_SIZE * MapSettings.CELL_SIZE);
			Vector2 cords = new Vector2(x, y);
			return cords;
		}

		/// <summary>
		/// Get the cords aligned to pixel grid of current chunk 
		/// </summary>
		public Vector2 GetCurrentChunkCords(Vector2 worldPosition)//possibly the problem 
		{
			float x = Mathf.RoundToInt(worldPosition.X / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
			float y = Mathf.RoundToInt(worldPosition.Y / Chunk.CHUNK_SIZE / MapSettings.CELL_SIZE);
			Vector2 cords = new Vector2(x, y);
			return cords;
		}

		/// <summary>
		/// gets chunks at given chunk cords 
		/// </summary>
		public Chunk GetChunk(Vector2 worldPosition)
		{
			worldPosition = GetCurrentChunkCords(worldPosition);
			if (chunkArray.ContainsKey(worldPosition))
			{
				return chunkArray[worldPosition];
			}
			else
			{
				GD.PushError($"ERROR: tried to access NULL chunk {worldPosition}");
				return null;
			}
		}

		public void SetTerrain(Vector2 worldPosition, Terrain terrain)
		{
			Chunk chunk = GetChunk(worldPosition);

			if (chunk == null)
			{
				return;
			}

			chunk.Terrain.SetObject(worldPosition, terrain);

			//assign/update neighbors 
			// terrain.UpdateNorthNeighbor(GetTerrain(new Vector2(worldPosition.X,worldPosition.Y-1)));
			// terrain.UpdateSouthNeighbor(GetTerrain(new Vector2(worldPosition.X,worldPosition.Y+1)));
			// terrain.UpdateEastNeighbor(GetTerrain(new Vector2(worldPosition.X+1,worldPosition.Y)));
			// terrain.UpdateWestNeighbor(GetTerrain(new Vector2(worldPosition.X-1,worldPosition.Y)));

		}
		public Terrain GetTerrain(Vector2 worldPosition)
		{
			Chunk chunk = GetChunk(worldPosition);
			if (chunk == null)
			{
				return null;
			}
			
			return chunk.Terrain.GetObject(worldPosition);
		}

		/// <summary>
		/// update tile visualization color mode
		/// </summary>
		public void UpdateVisualizationMode(VisualizationMode displayMode)
		{
			if (WorldMap.MapVisualIzation != displayMode)
			{
				WorldMap.MapVisualIzation = displayMode;
				for (int i = 0; i < lastUpdatedChunks.Count; i++)
				{
					lastUpdatedChunks[i].UpdateTerrainVisualization(displayMode);
				}
			}
		}

		/// <summary>
		/// runs through surrounding chunks and decides wether or not 
		/// to hide them based on distance form player
		/// </summary>
		public void CheckChunkStatus(Vector2 playerPosition)
		{
			Vector2 currentChunkCords = GetCurrentChunkCords(playerPosition);

			//un render all last active chunks
			foreach (var chunk in lastUpdatedChunks)
			{
				chunk.SetVisibility(false);
			}
			lastUpdatedChunks.Clear();
			
			//Run through surrounding chunks at player position 
			for (int xOffset = -visibleChunks+2; xOffset < visibleChunks-1 /*+1 todo when threaded*/; xOffset++)
			{
				for (int yOffset = -visibleChunks+2; yOffset < visibleChunks-1 /*+1 todo when threaded*/; yOffset++)
				{
					Vector2 viewChunkCord = new Vector2(currentChunkCords.X + xOffset, currentChunkCords.Y + yOffset);
					
					if (chunkArray.ContainsKey(viewChunkCord))
					{
						Chunk chunk = chunkArray[viewChunkCord];

						chunk.UpdateVisibility(viewChunkCord);
						if (chunk.Rendered)
						{
							lastUpdatedChunks.Add(chunk);
						}
					}
					else
					{
						Vector2 chunkCord = GetChunkWorldPosition(viewChunkCord);
						Chunk newChunk = new(chunkCord, MapSettings.CELL_SIZE);
						worldMap.AddChild(newChunk);

						chunkArray.Add(viewChunkCord, newChunk);

						WorldGenerator.GenerateChunk(chunkCord, this);
					}
				}

			}
		}

	}
}
