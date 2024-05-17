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
	private bool Rendered;
	public ChunkCoordinate Coordinate { get; private set; }

	public Grid<Terrain> TerrainGrid { get; private set; }
	public Grid<Structure> StructureGrid { get; private set; }
	public Grid<Item> ItemGrid { get; private set; }

	public Chunk(Vector2 worldPosition)
	{
		CellSize = MapData.CELL_SIZE;
		Coordinate = new ChunkCoordinate(worldPosition);
		Rendered = true;

		Name = $"Chunk {Coordinate}";

		TerrainGrid = new Grid<Terrain>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		StructureGrid = new Grid<Structure>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		ItemGrid = new Grid<Item>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this); 
	}

	public Chunk(int x, int y)
	{
		CellSize = MapData.CELL_SIZE;
		Coordinate = new ChunkCoordinate(x, y);
		Rendered = true;
		Name = $"Chunk {Coordinate}";

		TerrainGrid = new Grid<Terrain>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		StructureGrid = new Grid<Structure>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		ItemGrid = new Grid<Item>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this); 
	}

	public Chunk(SavedChunk savedChunk)
	{
		CellSize = savedChunk.CellSize;
		Coordinate = savedChunk.Cords;
		Rendered = savedChunk.Rendered;

		Name = $"Chunk {Coordinate}";

		TerrainGrid = new Grid<Terrain>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		StructureGrid = new Grid<Structure>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this);
		ItemGrid = new Grid<Item>(new Vector2(CHUNK_SIZE, CHUNK_SIZE), Coordinate, this); 

		foreach (var savedTerrain in savedChunk.SavedTerrain)
		{
			Terrain terrain = new Terrain(savedTerrain);
			TerrainGrid.SetObject(terrain.GetCoordinate(), terrain);
		}
		foreach (var savedStructure in savedChunk.SavedStructure)
		{
			Structure structure = new Structure(savedStructure);
			StructureGrid.SetObject(structure.GetCoordinate(), structure);
		}
	}

	~Chunk()
	{
		if (IsInstanceValid(this))
		{
			foreach (var child in GetChildren())
			{
				child.QueueFree();
			}
			QueueFree();
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
				TerrainGrid.GetObject(x, y).UpdateGraphic(displayMode);
				if ((structure = StructureGrid.GetObject(x, y)) != null)
				{
					if (displayMode != VisualizationMode.Default)
					{
						structure.GetNode().Visible = false;
					}
					else
					{
						structure.GetNode().Visible = true;
					}
				}
			}
		}
	}

	/// <summary>
	/// updates viability of chunk if it's within render distance
	/// </summary>
	public void UpdateVisibility(Coordinate viewerCords)
	{
		MapData data = MapData.GetData();

		float distanceToViewier = Mathf.Sqrt(Coordinate.DistanceTo(viewerCords));

		bool visible = distanceToViewier <= data.GetRenderDistance();
		SetVisibility(visible);
	}
	/// <summary> sets if chunk is render or not </summary>
	public void SetVisibility(bool visible)
	{
		Rendered = visible;

		Visible = Rendered;
	}
	/// <summary> returns Visibility of chunk </summary>
	public bool CheckVisibility()
	{
		return Rendered;
	}

	
}