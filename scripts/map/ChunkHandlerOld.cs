// using System.Collections.Generic;
// using Godot;
// using Atomation.Thing;

// namespace Atomation.Map
// {
// 	/// <summary>
// 	/// ChunkHandler, stores and handles chunks, allow for 
// 	/// interaction with different element in a chunk
// 	/// </summary>
// 	public class ChunkHandlerOld
// 	{
// 		private readonly int visibleChunks;
// 		private Node2D map;
// 		private List<Chunk> lastUpdatedChunks;
// 		private Dictionary<Vector2, Chunk> chunks;

// 		private VisualizationMode currTileVisuals;

// 		public ChunkHandlerOld(Node2D map)
// 		{
// 			currTileVisuals = VisualizationMode.Default;
// 			visibleChunks = Mathf.RoundToInt(MapSettings.MAX_LOAD_DIST / Chunk.CHUNK_SIZE);
// 			GD.Print(visibleChunks);
// 			lastUpdatedChunks = new List<Chunk>();
// 			chunks = new Dictionary<Vector2, Chunk>();
// 			this.map = map;
// 		}

// 		public WorldGenerator WorldGenerator { get; set; }

// 		/// <summary>
// 		/// gets the chunk based on chunk cords, 
// 		/// </summary>
// 		public Chunk GetChunk(int chunkX, int chunkY)
// 		{
// 			Vector2 chunkCords = new Vector2(chunkX, chunkY);

// 			if (chunks.TryGetValue(chunkCords, out var chunk))
// 			{
// 				return chunk;
// 			}
// 			else
// 			{
// 				return null;
// 			}
// 		}

// 		/// <summary>
// 		/// gets the chunk based on chunk cords, 
// 		/// </summary>
// 		public Chunk GetChunk(Vector2 chunkCords)
// 		{
// 			return GetChunk(Mathf.RoundToInt(chunkCords.X), Mathf.RoundToInt(chunkCords.Y));
// 		}

// 		//
// 		// setting/getting
// 		//

// 		/// <summary>
// 		/// uses global tile position to set the terrain into correct Chunk
// 		/// </summary>
// 		public void Set(int globalX, int globalY, Terrain terrain)
// 		{
// 			Vector2 ChunkCords = GetChunkCords(globalX, globalY);

// 			Chunk chunk = GetChunk(ChunkCords);

// 			//making sure chunk actually exists
// 			if (chunk == null)
// 			{
// 				GD.PrintErr($"ERROR: tried to access NULL chunk {ChunkCords}");
// 				return;
// 			}

// 			//finding relative position of the tile
// 			int tileX = globalX - (int)ChunkCords.X * Chunk.CHUNK_SIZE;
// 			int tileY = globalY - (int)ChunkCords.Y * Chunk.CHUNK_SIZE;

// 			// chunk.Set(new(tileX, tileY), terrain);

// 			terrain.UpdateNorthNeighbor(this);
// 			terrain.UpdateSouthNeighbor(this);
// 			terrain.UpdateEastNeighbor(this);
// 			terrain.UpdateWestNeighbor(this);
// 		}

// 		/// <summary>
// 		/// uses global tile position to get terrain data from the correct Chunk
// 		/// </summary>
// 		public Terrain GetTerrain(int globalX, int globalY)
// 		{
// 			Vector2 chunkCords = GetChunkCords(globalX, globalY);

// 			Chunk chunk = GetChunk(chunkCords);

// 			if (chunk == null)
// 			{
// 				return null;
// 			}

// 			int tileX = globalX - (int)chunkCords.X * Chunk.CHUNK_SIZE;
// 			int tileY = globalY - (int)chunkCords.Y * Chunk.CHUNK_SIZE;

// 			return null; //chunk.Terrain.getx(new(tileX, tileY));
// 		}

// 		/// <summary>
// 		/// takes in global cords (as x and y), and converts them to be based on chunk grid 
// 		/// </summary>
// 		public Vector2 GetChunkCords(int globalX, int globalY)
// 		{
// 			int chunkX = Mathf.FloorToInt(globalX / Chunk.CHUNK_SIZE);
// 			int chunkY = Mathf.FloorToInt(globalY / Chunk.CHUNK_SIZE);

// 			return new Vector2(chunkX, chunkY);
// 		}

// 		/// <summary>
// 		/// takes in global cords as a vector, and converts them to be based on chunk grid 
// 		/// </summary>
// 		public Vector2 GetChunkCords(Vector2 GlobalCords)
// 		{
// 			return GetChunkCords(Mathf.RoundToInt(GlobalCords.X), Mathf.RoundToInt(GlobalCords.Y));
// 		}

// 		/// <summary>
// 		/// updates the visualization mode for terrain
// 		/// </summary>
// 		public void UpdateVisualizationMode(VisualizationMode displayMode)
// 		{
// 			if (currTileVisuals != displayMode)
// 			{
// 				currTileVisuals = displayMode;
// 				for (int i = 0; i < lastUpdatedChunks.Count; i++)
// 				{
// 					lastUpdatedChunks[i].UpdateTerrainVisualization(displayMode);
// 				}
// 			}
// 		}

// 		/// <summary>
// 		/// handles updating and creating new chunks if they haven't been loaded yet
// 		/// </summary>
// 		public void UpdateRenderedChunks(Vector2 viewerPosition)
// 		{
// 			Vector2 chunkCords = GetChunkCords(viewerPosition);

// 			for (int i = 0; i < lastUpdatedChunks.Count; i++)
// 			{
// 				lastUpdatedChunks[i].Visible = false;
// 			}
// 			lastUpdatedChunks.Clear();

// 			//run to each cord surround player and check to see if current chunk
// 			//is active and needs to be rendered/de-rendered
// 			for (int xOffset = -visibleChunks; xOffset < visibleChunks; xOffset++)
// 			{
// 				for (int yOffset = -visibleChunks; yOffset < visibleChunks; yOffset++)
// 				{
// 					Vector2 viewedChunkCord = new Vector2(chunkCords.X + xOffset, chunkCords.Y + yOffset);
// 					UpdateChunk(viewedChunkCord);
// 				}
// 			}
// 		}

// 		/// <summary>
// 		/// decides wether to keep a chunk currently loaded or unload it. if a new chunk is being 
// 		/// loaded also calls function to handle creating it
// 		/// </summary>
// 		private void UpdateChunk(Vector2 viewedChunkCord)
// 		{
// 			Chunk chunk = GetChunk(viewedChunkCord);

// 			if (chunk != null)
// 			{
// 				// //align viewer cords to be on MapSettings.CELL_SIZE grid
// 				// Vector2 viewedCellGrid = viewedChunkCord*MapSettings.CELL_SIZE;
// 				// if (chunk.UpdateVisibility(viewedCellGrid))
// 				// {
// 				// 	//if chunk exists and is currently rendered add to last rendered 
// 				// 	lastUpdatedChunks.Add(chunk);
// 				// }
// 			}
// 			else
// 			{
// 				GD.Print($"Placing {(viewedChunkCord* Chunk.CHUNK_SIZE).X +MapSettings.CELL_SIZE }");

// 				Vector2 globalCords = viewedChunkCord * Chunk.CHUNK_SIZE;

// 				// chunk = new(globalCords,MapSettings.CELL_SIZE, map);
// 				chunks.Add(viewedChunkCord, chunk);

// 				// WorldGenerator.GenerateChunk(globalCords, this);
// 			}
// 			lastUpdatedChunks.Add(chunk);
// 		}
// 	}
// }
