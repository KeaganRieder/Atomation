namespace Atomation.Map;

using System.Collections.Generic;
using Godot;
using Atomation.Things;

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

		visibleChunks = Mathf.FloorToInt(MapSettings.MAX_LOAD_DIST / Chunk.CHUNK_SIZE) - 1;

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
			GD.PushError($"ERROR: tried to access NULL chunk at chunkPos:{chunkPosition} WorldPos:{worldPosition}");
			return null;
		}
	}

	/// <summary>
	/// gets chunks at given world position 
	/// </summary>
	private Chunk GetChunk(Coordinate coords)
	{
		// Vector2 chunkPosition = GetChunkCords(worldPosition);

		if (chunkArray.ContainsKey(coords.ChunkGridPosition))
		{
			return chunkArray[coords.ChunkGridPosition];
		}
		else
		{
			GD.PushError($"ERROR: tried to access NULL chunk at chunkPos:{coords.ChunkGridPosition} WorldPos:{coords.WorldPosition}");
			return null;
		}
	}

	/// <summary>
	/// sets terrain at world position
	/// </summary>
	public void SetTerrain(Terrain terrain)
	{
		if (terrain == null)
		{
			return;
		}

		Chunk chunk = GetChunk(terrain.Coordinate);

		if (chunk == null)
		{
			return;
		}
		chunk.Terrain.SetObject(terrain.Coordinate.WorldPosition, terrain);
	}

	/// <summary>
	/// gets terrain at world position
	/// </summary>
	public Terrain GetTerrain(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return null;
		}
		return chunk.Terrain.GetObject(cord.WorldPosition);
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
	/// sets structure at world position
	/// </summary>
	public void SetStructure(Structure structure)
	{
		if (structure == null)
		{
			return;
		}

		Chunk chunk = GetChunk(structure.Coordinate);

		if (chunk == null)
		{
			return;
		}
		chunk.Buildings.SetObject(structure.Coordinate.WorldPosition, structure);
	}

	/// <summary>
	/// gets terrain at world position
	/// </summary>
	public Structure GetStructure(Vector2 worldPosition)
	{
		Chunk chunk = GetChunk(worldPosition);
		if (chunk == null)
		{
			return null;
		}
		return chunk.Buildings.GetObject(worldPosition);
	}
	/// <summary>
	/// gets structure at world position
	/// </summary>
	public Structure GetStructure(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);

		if (chunk == null)
		{
			return null;
		}
		return chunk.Buildings.GetObject(cord.WorldPosition);
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
	public void CheckChunkStatus(Coordinate playerPosition)
	{
		Vector2 currentChunkCords = playerPosition.ChunkGridPosition;

		//un render all last active chunks
		foreach (var chunk in lastUpdatedChunks)
		{
			chunk.SetVisibility(false);
		}
		lastUpdatedChunks.Clear();

		//Run through surrounding chunks at player position 
		for (int xOffset = -visibleChunks; xOffset < visibleChunks; xOffset++)//+1
		{
			for (int yOffset = -visibleChunks; yOffset < visibleChunks; yOffset++)//+1
			{
				Vector2 viewChunkCord = new Vector2(currentChunkCords.X + xOffset, currentChunkCords.Y + yOffset);

				if (chunkArray.ContainsKey(viewChunkCord))
				{
					Chunk chunk = chunkArray[viewChunkCord];

					chunk.UpdateVisibility(playerPosition);
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
