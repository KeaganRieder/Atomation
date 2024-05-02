namespace Atomation.Map;

using System.Collections.Generic;
using Godot;
using Atomation.Things;
using Atomation.Resources;

/// <summary> terrain tiles display mode </summary>
public enum VisualizationMode
{
	Undefined = -1,
	Default = 0,
	Height = 1,
	Heat = 2,
	Moisture = 3,
	Biome = 4,
}

/// <summary>
/// the games world is divided into chunks which each contain a set amount 
/// of tiles. this class handles interactions with the world
/// </summary>
public partial class WorldMap : Node2D
{
	private static WorldMap instance;
	private readonly MapData data;
	private readonly WorldGenerator generator;
	private readonly int visibleChunks;
	private List<Chunk> lastUpdatedChunks;
	private Dictionary<Vector2, Chunk> chunkArray;

	private VisualizationMode visualizationMode;

	private WorldMap()
	{
		Name = "WorldMap";

		data = MapData.GetData();

		visualizationMode = VisualizationMode.Default;

		visibleChunks = Mathf.FloorToInt(data.RenderDistance / Chunk.CHUNK_SIZE);

		lastUpdatedChunks = new List<Chunk>();
		chunkArray = new Dictionary<Vector2, Chunk>();
	}

	public static WorldMap GetInstance()
	{
		if (instance == null)
		{
			instance = new WorldMap();
		}
		return instance;
	}

	public SavedMap Save()
	{
		List<SavedChunk> savedChunks = new List<SavedChunk>();

		foreach (var chunk in chunkArray)
		{
			savedChunks.Add(new SavedChunk(chunk.Value));
		}

		return new SavedMap(savedChunks.ToArray());
	}

	/// <summary> generates a map from save </summary>
	public void Load(SavedMap savedMap)
	{
		GD.Print("Loading Map");
		ClearMap();
		data.Load(savedMap.MapSettings);
		foreach (var savedChunk in savedMap.SavedChunks)
		{
			Chunk chunk = new Chunk(savedChunk);
			chunkArray.Add(chunk.coordinate.ChunkGridPosition, chunk);
			AddChild(chunk);
		}
	}

	/// <summary> generates a map from save </summary>
	public void ClearMap()
	{
		foreach (var chunkInfo in chunkArray)
		{
			DestroyChunk(chunkInfo);
		}
		chunkArray = new Dictionary<Vector2, Chunk>();
	}
	/// <summary> destroys chunk at given cords </summary>
	private void DestroyChunk(KeyValuePair<Vector2, Chunk> chunkInfo)
	{
		if (IsInstanceValid(chunkInfo.Value))
		{
			chunkInfo.Value.QueueFree();
		}
	}

	/// <summary> gets the position of a chunk at the given cords </summary>
	private Vector2 GetChunkCords(Vector2 worldPosition)
	{
		int xCord = Mathf.FloorToInt(worldPosition.X / Chunk.TOTAL_CHUNK_SIZE);
		int yCord = Mathf.FloorToInt(worldPosition.Y / Chunk.TOTAL_CHUNK_SIZE);

		return new Vector2(xCord, yCord);
	}

	/// <summary> gets chunks at given world position </summary>
	private Chunk GetChunk(Coordinate cords)
	{
		// Vector2 chunkPosition = GetChunkCords(worldPosition);

		if (chunkArray.ContainsKey(cords.ChunkGridPosition))
		{
			return chunkArray[cords.ChunkGridPosition];
		}
		else
		{
			GD.PushError($"ERROR: tried to access NULL chunk at chunkPos:{cords.ChunkGridPosition} WorldPos:{cords.WorldPosition}");
			return null;
		}
	}

	/// <summary> sets terrain at world position </summary>
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
		terrain.UpdateGraphic(visualizationMode);
		chunk.Terrain.SetObject(terrain.Coordinate.WorldPosition, terrain);
	}

	/// <summary> gets terrain at world position </summary>
	public Terrain GetTerrain(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return null;
		}
		return chunk.Terrain.GetObject(cord.WorldPosition);
	}

	/// <summary> sets structure at world position </summary>
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

	/// <summary> gets structure at world position </summary>
	public Structure GetStructure(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);

		if (chunk == null)
		{
			return null;
		}
		return chunk.Buildings.GetObject(cord.WorldPosition);
	}

	/// <summary> update tile visualization color mode </summary>
	public void SetVisualizationMode(VisualizationMode displayMode)
	{
		if (visualizationMode != displayMode)
		{
			visualizationMode = displayMode;
			for (int i = 0; i < lastUpdatedChunks.Count; i++)
			{
				lastUpdatedChunks[i].UpdateVisualization(displayMode);
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
					Chunk newChunk = new(chunkCord, MapData.CELL_SIZE);
					AddChild(newChunk);

					chunkArray.Add(viewChunkCord, newChunk);

					WorldGenerator.GetInstance().GenerateChunk(chunkCord);

					lastUpdatedChunks.Add(newChunk);
				}
			}
		}
	}

}