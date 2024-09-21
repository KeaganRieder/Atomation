namespace Atomation.Things;

using Atomation.GameMap;
using Godot;

public interface IThing
{
    public Node Node { get; }

    public int GridLayer { get; set; }

    public Chunk Chunk{get;set;}
}