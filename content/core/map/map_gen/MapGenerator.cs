using System;
using System.Collections.Generic;
using Godot;

 

/// <summary>
/// MapGenerator manges the various steps for generating the
/// games map, executing each genStep in it's correct order
/// </summary>
public class MapGenerator{
    private SimplexNoise elevation;
    // private SimplexNoise moisture;


    public MapGenerator(){
        elevation = new SimplexNoise(0,5,0.01f,2,0.5f);
    }

    public void GenerateMap(){
        for (int x = 0; x < Map.mapData.Width; x++)
        {
            for (int yOffset = 0; yOffset < Map.mapData.Height; yOffset++)
            {
                
            }
        }
    }

    public void GenerateChunk(){

        for (int x = 0; x < Data.ChunkSize; x++)
        {
            for (int y = 0; y < Data.ChunkSize; y++)
            {
                GD.Print($"Noise {x},{y}: {elevation.GetNoise(x,y)}");
            }
        }
    }

}