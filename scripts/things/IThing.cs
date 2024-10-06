namespace Atomation.Things;

using Atomation.GameMap;
using Godot;

public interface IThing
{
    /// <summary>
    /// the main godot object that represents a thing
    /// </summary>
    public Node2D Node { get; }

    public int GridLayer { get; set; }

    public Chunk Chunk{get;set;}
}