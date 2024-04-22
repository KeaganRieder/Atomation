namespace Atomation.Map;

using Godot;

/// <summary>
/// a Coordinate in the game world contains, methods convert
/// it between different grids
/// </summary>
public class Coordinate
{
    private float cellSize;
    private float chunkSize;
    private float totalChunkSize;

    /// <summary> objects current world position </summary>
    public Vector2 WorldPosition { get; private set; }

    /// <summary> objects Chunk position relative to the chunk grid </summary>
    public Vector2 ChunkGridPosition { get; private set; }

    /// <summary> objects Chunk position in the world </summary>
    public Vector2 ChunkWorldPos { get { return ChunkGridPosition * totalChunkSize; } }

    /// <summary> objects current x y position in the world </summary>
    public Vector2I XYPosition { get; private set; }

    private Coordinate()
    {
        cellSize = MapSettings.CELL_SIZE;
        chunkSize = Chunk.CHUNK_SIZE;
        totalChunkSize = chunkSize * cellSize;
    }

    public Coordinate(Vector2 worldPosition) : this()
    {
        WorldPosition = new Vector2(Mathf.FloorToInt(worldPosition.X), Mathf.FloorToInt(worldPosition.Y));
        FindChunkPosition();
        FindXYCords();
    }

    public Coordinate(int x, int y, Vector2 chunkPosition) : this()
    {
        XYPosition = new Vector2I(x, y);
        FindChunkPosition(chunkPosition);
        FindWorldPosition();
    }

    /// <summary>
    /// finds the x y coords based on the position in the world 
    /// and the cell size
    /// </summary>
    private void FindXYCords()
    {
        // y = Mathf.Abs(Mathf.FloorToInt((worldPosition - Origin).Y / CellSize));
        int x = Mathf.Abs(Mathf.FloorToInt((WorldPosition.X - ChunkWorldPos.X) / cellSize));
        int y = Mathf.Abs(Mathf.FloorToInt((WorldPosition.Y - ChunkWorldPos.Y) / cellSize));

        XYPosition = new Vector2I(x, y);
    }

    /// <summary>
    /// finds the world position based on the offset
    /// </summary>
    private void FindWorldPosition()
    {
        float x = (XYPosition.X * cellSize) + ChunkWorldPos.X;
        float y = (XYPosition.Y * cellSize) + ChunkWorldPos.Y;
        WorldPosition = new Vector2(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }

    /// <summary>
    /// finds the chunk cords which the object currently resides in
    /// </summary>
    private void FindChunkPosition()
    {
        int xCord = Mathf.FloorToInt(WorldPosition.X / totalChunkSize);
        int yCord = Mathf.FloorToInt(WorldPosition.Y / totalChunkSize);

        ChunkGridPosition = new Vector2(xCord, yCord);
        // ChunkWorldPos = ChunkPosition * totalChunkSize;
    }

    /// <summary>
    /// finds the chunk cords which the object currently resides in
    /// </summary>
    private void FindChunkPosition(Vector2 chunkPos)
    {
        ChunkGridPosition = chunkPos / chunkSize;
        // ChunkWorldPos = ChunkPosition * totalChunkSize;
    }

    /// <summary>
    /// world distance from given cord
    /// </summary>
    public float Distance(Coordinate cord)
    {
        Vector2 from = WorldPosition;
        Vector2 to = cord.WorldPosition;

        float distance = from.DistanceTo(to);

        return Mathf.Round(distance);
    }

    /// <summary>
    /// distance from chunk
    /// </summary>
    public float ChunkDistance(Coordinate cord)
    {
        Vector2 from = WorldPosition / cellSize;
        Vector2 to = cord.WorldPosition / cellSize;

        float distance = Mathf.Round(from.DistanceTo(to));

        // float x = Mathf.Abs((ChunkWorldPos - cord.ChunkWorldPos).X);
        // float y = Mathf.Abs((ChunkWorldPos - cord.ChunkWorldPos).Y);

        // float distance = Mathf.Min(x, y) / chunkSize;

        return distance;
    }

    /// <summary>
    /// Updates the position in the world to the provided value
    /// </summary>
    public void UpdateWorldPosition(Vector2 coords)
    {
        WorldPosition = coords;

        FindXYCords();
        FindChunkPosition();
    }

    /// <summary>
    /// Updates the X Y position on the cell grid to the provided value 
    /// and then finds total world position based on the provided offset
    /// </summary>
    public void UpdateXYPosition(int x, int y)
    {
        XYPosition = new Vector2I(x, y);
        FindWorldPosition();
    }

    public override string ToString()
    {
        string cords = $" {XYPosition}, {WorldPosition}, ";
        return cords;
    }
}