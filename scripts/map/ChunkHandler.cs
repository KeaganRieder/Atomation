using System.Collections.Generic;
using Godot;
using Atomation.Thing;

namespace Atomation.Map
{
	/// <summary>
	/// ChunkHandler, stores and handles chunks, allow for 
	/// interaction with differnt elemnt in a chunk
	/// </summary>
	public class ChunkHandler
	{
		public const float MAX_LOAD_DIST = 64;

		private readonly int visibleChunks;
		private List<Chunk> lastUpdatedChunks;
		private Dictionary<Vector2, Chunk> chunks;

		private Node2D map;

		public ChunkHandler(Node2D map)
		{
			visibleChunks = Mathf.RoundToInt(MAX_LOAD_DIST / Chunk.CHUNK_SIZE);
			lastUpdatedChunks = new List<Chunk>();
			chunks = new Dictionary<Vector2, Chunk>();
			this.map = map;
		}

		public WorldGenerator WorldGenerator { get; set; }

		/// <summary>
		/// gets the chunk based on chunk cords, 
		/// </summary>
		public Chunk GetChunk(int chunkX, int chunkY){
			Vector2 chunkCords = new Vector2(chunkX, chunkY);
			if (chunks.TryGetValue(chunkCords, out var chunk))
			{
				return chunk;
			}
			else{
				// GD.Print($"Null CHUNK AT{chunkCords}");
				return null;
			}
		}
		/// <summary>
		/// gets the chunk based on chunk cords, 
		/// </summary>
		public Chunk GetChunk(Vector2 chunkCords){
			return GetChunk(Mathf.RoundToInt(chunkCords.X),Mathf.RoundToInt(chunkCords.Y));			
		}

		/// <summary>
		/// uses global tile position to set the terrain into correct Chunk
		/// </summary>
		public void Set(int globalX, int globalY, Terrain terrain){
			//need to fix negative cases
			Vector2 ChunkCords = GetChunkCords(globalX, globalY);

			//finding relative position of the tile
			int tileX = globalX - (int)ChunkCords.X * Chunk.CHUNK_SIZE;
			int tileY = globalY - (int)ChunkCords.Y * Chunk.CHUNK_SIZE;

			Vector2 cords = new(tileX, tileY);

			Chunk chunk = GetChunk(ChunkCords);

			//making sure chunk actually exists
			if(chunk != null){
				
				
				chunk.Set(cords,terrain);
			}
			else
			{
				GD.PrintErr($"ERROR: ATTEMPTED TO access NULL chunk {ChunkCords}");
			}
		}

		/// <summary>
		/// uses global tile position to get terrain data from the correct Chunk
		/// </summary>
		public Terrain GetTerrain(int globalX, int globalY){
			Vector2 ChunkCords = GetChunkCords(globalX, globalY);
			
			int tileX = globalX - (int)ChunkCords.X * Chunk.CHUNK_SIZE;
			int tileY = globalY - (int)ChunkCords.Y * Chunk.CHUNK_SIZE;

			Vector2 cords = new(tileX, tileY);

			Chunk chunk = GetChunk(ChunkCords);

			if(chunk != null){
				
				
				return chunk.GetTerrain(cords);
			}
			else
			{
				GD.PrintErr($"ERROR: ATTEMPTED TO access NULL chunk {ChunkCords}");
				return null;
			}			
		}

		/// <summary>
		/// takes in global cords (as x and y), and converts them to be based on chunk grid 
		/// </summary>
		public Vector2 GetChunkCords(int globalX, int globalY){
			//this may be flawed
			int chunkX = Mathf.FloorToInt(globalX / Chunk.CHUNK_SIZE);
			int chunkY = Mathf.FloorToInt(globalY / Chunk.CHUNK_SIZE);
			
			return new Vector2(chunkX,chunkY);
		}
		/// <summary>
		/// takes in global cords as a vector, and converts them to be based on chunk grid 
		/// </summary>
		public Vector2 GetChunkCords(Vector2 GlobalCords){
			return GetChunkCords(Mathf.RoundToInt(GlobalCords.X), Mathf.RoundToInt(GlobalCords.Y));
		}

		/// <summary>
		/// handles updating and creating new chunks if they haven't been loaded yet
		/// </summary>
		public void UpdateRenderedChunks(Vector2 viewerPosition)
		{
			Vector2 chunkCords =  GetChunkCords(viewerPosition);

			for (int i = 0; i < lastUpdatedChunks.Count; i++)
			{
				lastUpdatedChunks[i].SetRenderState(false);
			}
			lastUpdatedChunks.Clear();

			//run to each cord surround player and check to see if current chunk
			//is active and needs to be rendered/de-rendered
			for (int xOffset = -visibleChunks; xOffset < visibleChunks; xOffset++)
			{
				for (int yOffset = -visibleChunks; yOffset < +visibleChunks; yOffset++)
				{
					Vector2 viewedChunkCord = new Vector2(chunkCords.X + xOffset, chunkCords.Y + yOffset);
					UpdateChunk(viewedChunkCord);
				}
			}
		}

		/// <summary>
		/// decides wether to keep a chunk currently loaded or unload it. if a new chunk is being 
		/// loaded also calls function to handle creating it
		/// </summary>
		private void UpdateChunk(Vector2 chunkCords)
		{
			Chunk chunk = GetChunk(chunkCords);
			// GD.Print($"\nUPDATE for chunk at:{chunkCords * Chunk.CHUNK_SIZE}\n");
			if (chunk != null)
			{
				chunk.UpdateChunk(chunkCords);
				if (chunk.Rendered())
				{
					lastUpdatedChunks.Add(chunk);
				}
			}
			//creating chunk sense it's never been loaded before
			else{
				//make based on global ie aligned to intervals of 32 for positioning
				Vector2 globalCords =  chunkCords * Chunk.CHUNK_SIZE;

				//creating new chunk 	
				chunks.Add(chunkCords, new(globalCords, map));

				//call generator to actually generate chunk
				WorldGenerator.GenerateChunk(globalCords, this);
			}
		}
	}
}
