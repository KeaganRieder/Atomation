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
		private int chunkSize;

		private List<Chunk> lastUpdatedChunks;
		private Dictionary<Vector2, Chunk> chunkArray;

		private Node2D worldMap;

		public ChunkHandler(Node2D map)
		{
			worldMap = map;

			visibleChunks = 1;// Mathf.FloorToInt(MapSettings.MAX_LOAD_DIST / Chunk.CHUNK_SIZE);//  - 1;

			// making chunk size based on tiles not pixels
			chunkSize = Chunk.CHUNK_SIZE * MapSettings.CELL_SIZE;

			lastUpdatedChunks = new List<Chunk>();
			chunkArray = new Dictionary<Vector2, Chunk>();
		}

		/// <summary>
		/// gets the position of a chunk at the given cords
		/// </summary>
		private Vector2 GetChunkCords(Vector2 worldPosition)
		{
			int xCord = Mathf.FloorToInt(worldPosition.X / chunkSize);
			int yCord = Mathf.FloorToInt(worldPosition.Y / chunkSize);

			return new Vector2(xCord, yCord);
		}

		/// <summary>
		/// gets chunks at given world position 
		/// </summary>
		private Chunk GetChunk(Vector2 worldPosition)
		{
			Vector2 chunkPosition = GetChunkCords(worldPosition);
			if (chunkArray.ContainsKey(chunkPosition))
			{
				return chunkArray[chunkPosition];
			}
			else
			{
				GD.PushError($"ERROR: tried to access NULL chunk at {chunkPosition} {chunkPosition}");
				return null;
			}
		}

		/// <summary>
		/// sets terrain at world position
		/// </summary>
		public void SetTerrain(Terrain terrain)
		{
			Chunk chunk = GetChunk(terrain.coordinate.ChunkCords);

			if (chunk == null)
			{
				return;
			}
			// Coordinate cord = new Coordinate(terrain.coordinate.ChunkCords);

			// terrain.coordinate = cord;
			chunk.Terrain.SetObject(terrain.coordinate.WorldPosition, terrain);

			//assign/update neighbors todo
		}

		/// <summary>
		/// sets terrain at world position
		/// </summary>
		public void SetTerrain(int x, int y, Terrain terrain)
		{
			Chunk chunk = GetChunk(terrain.coordinate.ChunkCords);

			if (chunk == null)
			{
				return;
			}
			// terrain.coordinate = new Coordinate(x,y,chunk.coordinate.WorldPosition);
			chunk.Terrain.SetObject(x, y, terrain);

			//assign/update neighbors todo
		}

		/// <summary>
		/// gets terrain at world position
		/// </summary>
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
		/// sets terrain at world position
		/// </summary>
		public Terrain GetTerrain(int x, int y)
		{
			Chunk chunk = GetChunk(new Vector2(x,y));
			if (chunk == null)
			{
				return null;
			}
			return chunk.Terrain.GetObject(x, y);
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
			Vector2 currentChunkCords = GetChunkCords(playerPosition);

			//un render all last active chunks
			foreach (var chunk in lastUpdatedChunks)
			{
				chunk.SetVisibility(false);
			}
			lastUpdatedChunks.Clear();

			//Run through surrounding chunks at player position 
			for (int xOffset = -visibleChunks; xOffset < visibleChunks /*+1 todo when threaded*/; xOffset++)
			{
				for (int yOffset = -visibleChunks; yOffset < visibleChunks /*+1 todo when threaded*/; yOffset++)
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
						Vector2 chunkCord = viewChunkCord * Chunk.CHUNK_SIZE;
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
