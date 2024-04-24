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
	/// <summary>
	/// how many pixels a tile is 16 x 16 is the normal
	/// </summary>
	public const int CELL_SIZE = 16;

	private readonly int visibleChunks;
	private List<Chunk> lastUpdatedChunks;
	private Dictionary<Vector2, Chunk> chunkArray;

	private VisualizationMode visualizationMode;

	public MapSettings MapSettings { get; set; }

	public WorldMap()
	{
		visualizationMode = VisualizationMode.Default;

		visibleChunks = Mathf.FloorToInt(MapSettings.MaxLoadDistance / Chunk.CHUNK_SIZE);

		lastUpdatedChunks = new List<Chunk>();
		chunkArray = new Dictionary<Vector2, Chunk>();

		MapSettings = FileManger.ReadJsonFile<MapSettings>(FilePaths.CONFIG_FOLDER, "map_settings");
		WorldGenerator.Initialize(MapSettings.GenSettings);
	}

	/// <summary> generates a map from save </summary>
	public void LoadMap()
	{
		GD.Print("Generating Map");

		Coordinate coordinate = new Coordinate(MapSettings.GenSettings.Center);
		CheckChunkStatus(coordinate);

		GD.Print("Generation Complete");

	}

	/// <summary> generates the final map following customization </summary>
	public void FinalizeMapSetUp()
	{
		GD.Print("Generating Map");

		//todo

		GD.Print("Generation Complete");
	}

	/// <summary> gets the position of a chunk at the given cords </summary>
	private Vector2 GetChunkCords(Vector2 worldPosition)
	{
		int xCord = Mathf.FloorToInt(worldPosition.X / Chunk.TOTAL_CHUNK_SIZE);
		int yCord = Mathf.FloorToInt(worldPosition.Y / Chunk.TOTAL_CHUNK_SIZE);

		return new Vector2(xCord, yCord);
	}

	/// <summary> gets chunks at given world position </summary>
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
	public void UpdateVisualizationMode(VisualizationMode displayMode)
	{
		if (visualizationMode != displayMode)
		{
			visualizationMode = displayMode;
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
					Chunk newChunk = new(chunkCord, CELL_SIZE);
					AddChild(newChunk);

					chunkArray.Add(viewChunkCord, newChunk);
					WorldGenerator.GenerateChunk(chunkCord, this);
				}
			}

		}
	}

}