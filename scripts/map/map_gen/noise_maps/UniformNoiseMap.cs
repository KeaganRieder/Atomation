using System;
using Godot;

/// <summary>
/// generates a unfiormed nosieMap
/// </summary>
public class UniformNoiseMap : NoiseObject
{
    public Vector2 MapBounds{get;set;}
    public Vector2 CenterCord{get;set;}
    /// <summary>
    /// the max distance a point can be from the cneratl on the y
    /// </summary>
    public float MaxDistance{get;set;}
    private float[,] noiseMap;

    public UniformNoiseMap(int width, int height){
        noiseMap = new float[width,height];
        MapBounds = new Vector2(width,height);
    }

    public void Generate(int y){
        //sample at current cord
        // for (int Y = 0; Y < length; Y++)
        // {
            
        // }
        float sampleY = y + Offset.Y;
        float noise = sampleY - CenterCord.Y / MaxDistance;

    }
   
    public override float this[int x, int y]{
        get{
            return noiseMap[y,x];            
        }
    }
    public override float this[Vector2 cords]{
        get{
            int x = Mathf.RoundToInt(cords.X);
            int y = Mathf.RoundToInt(cords.Y);
            return this[x, y];
        }
    } 
}
