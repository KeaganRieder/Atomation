using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// A chunk is a 32 x 32 section of the map that contans
/// various values, it is eitehr loaded or unloaded depedning on where 
/// the player is, as well as other aspects
/// </summary>


public class Chunk 
{
    public const int CHUNK_SIZE = 32;

    public Node2D ChunkNode{get; set;}
    public Vector2 position {get; private set;}
    private Dictionary<Vector2, Terrain> floor; // maybe make new class for this?

    private float bounds;

    //constructors
    public Chunk(){
        floor = new Dictionary<Vector2, Terrain>();
    }
    public Chunk(Vector2 cords, Node2D parentNode) : this(){
        position = cords * CHUNK_SIZE ;
        ChunkNode = new Node2D(){
        Name = $"Chunk {position}",
        Position = position
        };
        ChunkNode.AddChild(new ColorRect(){Color = new Color(255), Size = new Vector2(32,32)});
        parentNode.AddChild(ChunkNode);
        
        // floor = new Dictionary<Vector2, Terrain>();
    }

    //geting compents
    public Terrain Floor(Vector2 key){
        if (floor.ContainsKey(key))
        {
            return floor[key];
        }
        return default;
    }
    public void Floor(Vector2 key, Terrain value){
        floor[key] = value;
        //todo add checks for floor support, and if it is placeAble  
    }

    //rendering stuff
    public void UpdateChunk(Vector2 veiwerCords){
        float distToVeiwer = (position/CHUNK_SIZE).DistanceTo(veiwerCords); 
        // if (distToVeiwer <= ChunkHandler.MAX_LOAD_DIST == false)
        // {
        //     GD.Print("unreandered");
        //     GD.Print($"{ChunkNode.Name} {distToVeiwer/CHUNK_SIZE}");
        //     GD.Print($"veiwer cords {veiwerCords}");
        //     GD.Print($"Chunk cords {position}");
        //     GD.Print($"___________________________");
        // }
        bool visible = distToVeiwer <= ChunkHandler.MAX_LOAD_DIST;
        SetRenderState(visible);
    }
    public bool Rendered(){
        return ChunkNode.Visible;
    }
    public void SetRenderState(bool state){
        ChunkNode.Visible  = state;
    }
}