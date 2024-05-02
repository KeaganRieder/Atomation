namespace Atomation.Map;

using Godot;
using Atomation.Things;

/// <summary>
/// A chunk is a 32 x 32 tiles section of the map that contains
/// various values, it is either loaded or unloaded depending on where 
/// the player is, as well as other aspects
/// </summary>
public partial class Chunk : Node2D
{
	/// <summary> how many pixels make up the length and width of a chunk </summary>
	public const int CHUNK_SIZE = 32;

	/// <summary> how many tiles make up the length and width of a chunk </summary>
	public const int TOTAL_CHUNK_SIZE = CHUNK_SIZE * MapData.CELL_SIZE;

	public float CellSize { get; private set; }
	public bool Rendered { get; private set; }
	public Coordinate coordinate { get; private set; }

	public Grid<Terrain> Terrain { get; private set; }
	public Grid<Structure> Buildings { get; private set; }

	public Chunk(Vector2 worldPosition, float cellSize)
	{
		CellSize = cellSize;
		coordinate = new Coordinate(worldPosition);
		Rendered = true;

		Name = $"Chunk {coordinate.ChunkGridPosition}, {coordinate.ChunkWorldPos}";

		Terrain = new Grid<Terrain>(CHUNK_SIZE, CHUNK_SIZE, cellSize, worldPosition, this);
		Buildings = new Grid<Structure>(CHUNK_SIZE, CHUNK_SIZE, cellSize, worldPosition, this);
	}

	public Chunk(SavedChunk savedChunk)
	{
		CellSize = savedChunk.CellSize;
		coordinate = savedChunk.Cords;
		Rendered = savedChunk.Rendered;

		Name = $"Chunk {coordinate.ChunkGridPosition}, {coordinate.ChunkWorldPos}";

		Terrain = new Grid<Terrain>(CHUNK_SIZE, CHUNK_SIZE, CellSize, coordinate.WorldPosition, this);
		Buildings = new Grid<Structure>(CHUNK_SIZE, CHUNK_SIZE, CellSize, coordinate.WorldPosition, this);

		foreach (var savedTerrain in savedChunk.SavedTerrain)
		{
			Terrain terrain = new Terrain(savedTerrain);
			Vector2I cord = terrain.Coordinate.XYPosition;
			Terrain.SetObject(cord.X,cord.Y, terrain);
		}
		foreach (var savedStructure in savedChunk.SavedStructure)
		{
			Structure structure = new Structure(savedStructure);
			Vector2I cord = structure.Coordinate.XYPosition;
			Buildings.SetObject(cord.X,cord.Y, structure);
		}
	}

	/// <summary>
	/// updates the visualization mode of all tiles
	/// </summary>
	public void UpdateVisualization(VisualizationMode displayMode)
	{
		Structure structure;

		for (int x = 0; x < CHUNK_SIZE; x++)
		{
			for (int y = 0; y < CHUNK_SIZE; y++)
			{
				Terrain.GetObject(x, y).UpdateGraphic(displayMode);
				if ((structure = Buildings.GetObject(x, y)) != null)
				{
					if (displayMode != VisualizationMode.Default)
					{
						structure.Visible = false;
					}
					else
					{
						structure.Visible = true;
					}
				}
			}
		}
	}

	/// <summary>
	/// checks viewer distance from chunk and then decided based on rendered distance
	/// to decide weather or not to hide/un render chunk.
	/// </summary>
	public void UpdateVisibility(Coordinate viewerCords)
	{
		bool visible = coordinate.ChunkDistance(viewerCords) <= MapData.GetData().RenderDistance;

		SetVisibility(visible);
	}

	public void SetVisibility(bool visible)
	{
		Rendered = visible;

		Visible = Rendered;
	}
}