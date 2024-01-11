using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// A chunk is a 32 x 32 section of the map that contans
/// various values, it is eitehr loaded or unloaded depedning on where 
/// the player is, as well as other aspects
/// </summary>

//look at the old chunk obj in world gen on git
public class Chunk
{
    public const int CHUNK_SIZE = 32;

    public Node2D ChunkObj{get; set;}
    public Vector2 origin;
    private bool rendered;

    private Dictionary<Vector2, Terrain> floor; // maybe make new class for this?

    //constructors
    public Chunk(){
        floor = new Dictionary<Vector2, Terrain>();
    }
    public Chunk(Vector2 cords) : this(){
        origin = cords;
        rendered = true;
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
    public bool Rendered(Vector2 veiwerCords){
        float veiwerDist = veiwerCords.DistanceSquaredTo(origin);
        GD.Print($"Veiwer cords {veiwerCords} are {veiwerDist} from chunk at {origin}");
        return veiwerDist <= ChunkHandler.MAX_VIEW_DIST;
    }
    public void Rendered(bool state){
        rendered = state;
    }

    public void ShowOutLine(){
        for (int x = 0; x < CHUNK_SIZE; x++)
        {
            for (int y = 0; y < CHUNK_SIZE; y++)
            {
                // node.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color,1);//ver
                // node.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, 1);//hor
            }
        }
    }

}
/*
public void ShowOutline(Node2D node){
        //todo
        Color color = new Color(255,0,0);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                node.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color,1);//ver
                node.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, 1);//hor
            }
        }
        node.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(Width, 0), Colors.Green, 1);
        node.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(0, Height), Colors.Green, 1);
        node.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height), Colors.Green, 1);
        node.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height), Colors.Green, 1);
    }

    public Vector2 GetWorldPosition(int x, int y){
        return new Vector2(x,y) * CELLSIZE + origin;
    }
    private Vector2 normalizeCords(Vector2 worldPostion)
    {
        int x = Mathf.FloorToInt((worldPostion - origin).X / CELLSIZE);
        int y = Mathf.FloorToInt((worldPostion - origin).Y / CELLSIZE);
        return new Vector2(x,y);
    }
*/