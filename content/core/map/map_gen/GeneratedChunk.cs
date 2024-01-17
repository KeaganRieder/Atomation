using System.Collections.Generic;
using Godot;

/// <summary>
/// tempeary struct used to pass newely generated chunk data
/// </summary>
public struct GeneratedChunk
{
    public Node2D ChunkNode{get;}
    public Dictionary<Vector2, Terrain> Terrain{get;}

    public GeneratedChunk(Dictionary<Vector2, Terrain> terrain, Node2D chunkNode){
        Terrain = terrain;
        ChunkNode = chunkNode;
    }
}