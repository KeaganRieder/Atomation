using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// defiens the generation step for generating noise maps, 
/// which are pass to generation steps following, and help determine
/// asspects in the game world
/// </summary>
public class GenStepNoise : GenStep
{
    //general info
    private int worldMaxWidth; //maybe?
    private int worldMaxHeight;  //maybe?
    private float seaLevel; //no sea = 
    private float mountainSize; // no mounatins = 1
    private int equatorHeight;
    private int maxFromEquator; 

    //noise map info
    private NoiseMap elevationMap;
    private NoiseMap moistureMap;
    private NoiseMap heatMap;

    private Dictionary<Vector2, Terrain> terrainTiles;

    public GenStepNoise(GenConfigs genConfig){
       
        worldMaxWidth = genConfig.worldBounds.X;
        worldMaxHeight = genConfig.worldBounds.Y;
        equatorHeight = 0;
        maxFromEquator = worldMaxHeight;
        seaLevel = genConfig.seaLevel;
        mountainSize = genConfig.mounatinSize;
        elevationMap = new NoiseMap(genConfig.elevationMapConfigs);
        moistureMap = new NoiseMap(genConfig.moistureMapConfigs);
        heatMap = new NoiseMap(genConfig.heatMapConfigs); 
    }

    /// <summary>
    /// used to get the elevation noise map inorder to change
    /// indvidual conifg Data
    /// </summary>
    public NoiseMap GetElevationMap(){
        return elevationMap;
    }
    /// <summary>
    /// used to get the moisture noise map inorder to change
    /// indvidual conifg Data
    /// </summary>
    public NoiseMap GetMoistureMap(){
        return moistureMap;
    }
    /// <summary>
    /// used to get the heat noise map inorder to change
    /// indvidual conifg Data
    /// </summary>
    public NoiseMap GetHeatMap(){
        return heatMap;
    }

    //GenerationData
    public Dictionary<Vector2, Terrain> RunStep(Vector2 origin, int width, int height){
        // origin *= WorldMap.CELL_SIZE;
        elevationMap.Offset = origin;
        heatMap.Offset = origin;
        moistureMap.Offset = origin;

        terrainTiles = new Dictionary<Vector2, Terrain>();

        float[,] elevation = GenerateElevationMap(origin, width, height);
        float[,] worldHeat = GenarateHeatMap(origin, width, height);

        //applying generated maps to tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // GD.Print(elevation[x,y]);
                Vector2 cords = new Vector2(x,y);
                if (terrainTiles.ContainsKey(cords))
                {
                    //do nothing sense this is already created
                    // terrainTiles[cords].HeatValue = worldHeat[x,y];
                    // terrainTiles[cords].HeightValue = elevation[x,y];
                    // terrainTiles[cords].MoistureValue = 0;
                }
                else
                {
                    Terrain terrain = new Terrain(cords)
                    {
                        HeatValue = worldHeat[x,y],
                        HeightValue = elevation[x,y],
                        
                        MoistureValue = 0
                    };
                    terrainTiles.Add(new Vector2(x,y), terrain);
                }                
            }
        }
       
        return terrainTiles;
    }

    /// <summary>
    /// generateing elevation map which outlines the primary parts of the terrain
    /// </summary>
    private float[,] GenerateElevationMap(Vector2 origin, int width, int height){
        float[,] elevation = new float[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {         
                elevation[x,y] = Mathf.Clamp(Mathf.Abs(elevationMap[x,y]),0,1);  
            }
        }

        return elevation;
    }

    //this works
    private float[,] GenerateWorldHeat(Vector2 origin, float maxDistance,int width, int height){
        float[,] noiseMap = new float[width, height];

        // dicide the tempeture based on distnace from central point/equator
        for (int y = 0; y < height; y++)
        {
            float sampleY = y + origin.Y;

            //calaculate noise value based on it's distnace
            // well also ensuring that it's within the bounds
            float noise = Math.Abs(sampleY - equatorHeight) / maxDistance;//need a figure out this

            for (int x = 0; x < width; x++)
            {
                //apply the noise vallue for all points at this Row
                noiseMap[x,y] = Mathf.Clamp(noise, 0,1);
            }
        }
        return noiseMap;
    } 

    private float[,] GenarateHeatMap(Vector2 origin, int width, int height){
        float[,] worldHeat = new float[width, height];
        float[,] equatorHeat = GenerateWorldHeat(origin,64, width ,height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {   
                //need to figure out how to properly apply this
                float heat =  equatorHeat[x,y]*heatMap[x,y];// + Mathf.Sin(elevationMap[x,y]);//*heatMap[x,y];
                // GD.Print(heat);
                // float heightCurve = ;
                // heat += heightCurve;// * elevationMap[x,y];
                worldHeat[x,y] = MathF.Abs(heat);
            }
        }
        return worldHeat;
    }

    private void EvaluateOnCurve(float time, float value){

    }
}
//  function (float time, float startValue, float change, float duration) {
//      time /= duration / 2;
//      if (time < 1)  {
//           return change / 2 * time * time + startValue;
//      }

//      time--;
//      return -change / 2 * (time * (time - 2) - 1) + startValue;
//  };