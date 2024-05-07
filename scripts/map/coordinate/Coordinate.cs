namespace Atomation.Map;

using Newtonsoft.Json;
using Godot;
using System;

/// <summary>
/// an Objects Coordinate in the game world contains, methods convert
/// it between different grids
/// </summary>
public class Coordinate
{
    /// <summary> objects current x y position in the world </summary>
    [JsonProperty("X,Y Cords")]
    protected Vector2I XYPosition;

    /// <summary> objects current world position </summary>
    [JsonProperty("World Position")]
    protected Vector2 worldPosition;

    private Vector2 offset;

    [JsonConstructor]
    protected Coordinate() { }
    public Coordinate(Vector2 position)
    {
        SetPosition(position);
    }
    public Coordinate(int x, int y, Vector2 position)
    {
        int xCord = Mathf.FloorToInt(position.X / Chunk.CHUNK_SIZE);
        int yCord = Mathf.FloorToInt(position.Y / Chunk.CHUNK_SIZE);
        offset = new Vector2(xCord, yCord);
        offset *= Chunk.TOTAL_CHUNK_SIZE;

        SetPosition(Mathf.Abs(x), Mathf.Abs(y));
    }

    protected Vector2 FindChunkWorldPos(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.X / Chunk.TOTAL_CHUNK_SIZE);
        int y = Mathf.FloorToInt(position.Y / Chunk.TOTAL_CHUNK_SIZE);

        return new Vector2(x, y) * Chunk.TOTAL_CHUNK_SIZE;
    }

    /// <summary>
    /// converts X Y position to the world position  
    /// </summary>
    protected virtual void WorldFromXY()
    {
        float x = (XYPosition.X * MapData.CELL_SIZE) + offset.X;
        float y = (XYPosition.Y * MapData.CELL_SIZE) + offset.Y;

        worldPosition = new Vector2I(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }

    /// <summary>
    /// converts world position to the X y position
    /// </summary>
    protected virtual void XYFromWorld()
    {
        int x = Mathf.Abs(Mathf.FloorToInt((worldPosition.X - offset.X) / MapData.CELL_SIZE));
        int y = Mathf.Abs(Mathf.FloorToInt((worldPosition.Y - offset.Y) / MapData.CELL_SIZE));

        XYPosition = new Vector2I(x, y);
    }

    /// <summary>
    /// sets the x y position
    /// </summary>
    protected virtual void SetPosition(int x, int y)
    {
        XYPosition = new Vector2I(x, y);
        WorldFromXY();
    }

    /// <summary>
    /// sets the world position
    /// </summary>
    public virtual void SetPosition(Vector2 position)
    {
        worldPosition = position;
        offset = FindChunkWorldPos(position);
        XYFromWorld();
    }

    public virtual Vector2I GetXYPosition(){
        return XYPosition;
    }
    public virtual Vector2 GetWorldPosition(){
        return worldPosition;
    }

    /// <summary>
    /// distance from cord to provided
    /// </summary>
    public virtual float DistanceTo(Coordinate cord)
    {
        float distance = Mathf.Round(GetWorldPosition().DistanceTo(cord.GetWorldPosition()));

        return distance;
    }

     /// <summary>
    /// squared distance from cord to provided
    /// </summary>
    public virtual float squaredDistanceTo(Coordinate cord)
    {
        float distance = Mathf.Sqrt(GetWorldPosition().DistanceTo(cord.GetWorldPosition()));

        return Mathf.Round(distance);
    }


    /// <summary>
    /// convert to chunk cords from normal cords
    /// </summary>
    public ChunkCoordinate ToChunkCords()
    {
        return new ChunkCoordinate(worldPosition);
    }
    
    /// <summary>
    /// convert to normal cords from chunk cords
    /// </summary>
    public Coordinate ToCords()
    {
        return new Coordinate(worldPosition);
    }

    public override string ToString()
    {
        string cords = $"{XYPosition}, {worldPosition}";
        return cords;
    }
}