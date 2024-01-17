using System;
using Godot;

/// <summary>
/// generates a unfiormed nosieMap
/// </summary>
public class UniformNoiseMap : NoiseObject
{
    private int width;
    private int height;
    private Vector2 centerCord;
    private float maxDistance;
    private float[,] noiseMap;


    public UniformNoiseMap(int width, int height, Vector2 offset, Vector2 centerCord, float maxDistance){
        noiseMap = new float[width,height];
        Offset = offset;
        this.width = width;
        this.height = height;
        this.centerCord = centerCord;
        this.maxDistance = maxDistance; 
        Generate();
    }

    //generates a gradient on the y axis form a central point
    public void Generate(){
        //todo firgure out how to make it so 1 is warm and 0 is coldest
        for (int y = 0; y < height; y++)
        {
            float samepleY = y + Offset.Y;
            float noise = Mathf.Abs(samepleY - centerCord.Y) / maxDistance;
            for (int x = 0; x < width; x++)
            {
                noiseMap[y,x] = noise;
            } 
        }
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
