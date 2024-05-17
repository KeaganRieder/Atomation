namespace Atomation.Map;

using System.Collections.Generic;
using Godot;
using Atomation.Things;
using Atomation.Resources;
using Atomation.Pawns;


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
	public static WorldMap Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new WorldMap();
			}
			return instance;
		}
	}

	private readonly MapData data;
	private readonly int visibleChunks;
	private List<Chunk> lastUpdatedChunks;
	private Dictionary<Vector2, Chunk> chunkArray;

	private VisualizationMode visualizationMode;

	private WorldMap()
	{
		Name = "WorldMap";

		data = MapData.GetData();

		visualizationMode = VisualizationMode.Default;

		visibleChunks = Mathf.FloorToInt(data.GetRenderDistance() / Chunk.CHUNK_SIZE);

		lastUpdatedChunks = new List<Chunk>();
		chunkArray = new Dictionary<Vector2, Chunk>();

	}



	private void OnTimerTimeout()
	{

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
			chunkArray.Add(chunk.Coordinate.GetXYPosition(), chunk);
			AddChild(chunk);
		}
	}

	/// <summary> generates a map from save </summary>
	public void ClearMap()
	{
		foreach (var chunk in chunkArray)
		{
			DestroyChunk(chunk);
		}
		chunkArray = new Dictionary<Vector2, Chunk>();
		lastUpdatedChunks = new List<Chunk>();
	}

	/// <summary> destroys chunk at given cords </summary>
	private void DestroyChunk(KeyValuePair<Vector2, Chunk> chunkInfo)
	{
		if (IsInstanceValid(chunkInfo.Value))
		{
			chunkInfo.Value.QueueFree();
		}
	}

	/// <summary> gets chunks at given world position </summary>
	private Chunk GetChunk(Coordinate cords)
	{
		ChunkCoordinate ChunkCords = cords.ToChunkCords();

		if (chunkArray.ContainsKey(ChunkCords.GetXYPosition()))
		{
			return chunkArray[ChunkCords.GetXYPosition()];
		}
		else
		{
			GD.PushError($"tried to access NULL chunk at chunkPos:{ChunkCords.GetXYPosition} WorldPos:{ChunkCords.GetWorldPosition}");
			return null;
		}
	}

	/// <summary>returns true if chunk at given cords, other wise returns false </summary>
	public bool ChunkExists(Coordinate cords)
	{
		return GetChunk(cords) != null;
	}

	/// <summary> sets terrain at world position </summary>
	public void SetTerrain(Coordinate cord, Terrain terrain)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return;
		}
		chunk.TerrainGrid.SetObject(cord.GetWorldPosition(), terrain);
	}

	/// <summary> gets terrain at world position </summary>
	public Terrain GetTerrain(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return null;
		}
		return chunk.TerrainGrid.GetObject(cord);
	}

	/// <summary> sets structure at world position </summary>
	public void SetStructure(Coordinate cord, Structure structure)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return;
		}
		chunk.StructureGrid.SetObject(cord, structure);
	}
	/// <summary> gets structure at world position </summary>
	public Structure GetStructure(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);

		if (chunk == null)
		{
			return null;
		}
		return chunk.StructureGrid.GetObject(cord);
	}

	public void SetItem(Coordinate cord, Item item)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return;
		}
		chunk.ItemGrid.SetObject(cord, item);

	}

	public Item GetItem(Coordinate cord)
	{
		Chunk chunk = GetChunk(cord);
		if (chunk == null)
		{
			return null;
		}
		return chunk.ItemGrid.GetObject(cord);

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
	public void UpdateVisibleChunks(Coordinate playerPosition)
	{
		Vector2 currentChunkCords = playerPosition.ToChunkCords().GetXYPosition();

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
					if (chunk.CheckVisibility())
					{
						lastUpdatedChunks.Add(chunk);
					}
				}
				else
				{
					Vector2 chunkCord = viewChunkCord * Chunk.CHUNK_SIZE;
					Chunk newChunk = new(Mathf.FloorToInt(viewChunkCord.X), Mathf.FloorToInt(viewChunkCord.Y));
					AddChild(newChunk);

					chunkArray.Add(viewChunkCord, newChunk);

					WorldGenerator.Instance.GenerateChunk(chunkCord);

					lastUpdatedChunks.Add(newChunk);
				}
			}
		}
	}


}