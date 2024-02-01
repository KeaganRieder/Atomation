using System;
using Godot;

/// <summary>
/// defiens the generation step for generating noise maps, 
/// which are pass to generation steps following, and help determine
/// asspects in the game world
/// </summary>
public class GenStepNoise : GenStep
{
    //general info
    private int worldMaxWidth;
    private int worldMaxHeight; 
    private float seaLevel; //no sea = 
    private float mountainSize; // no mounatins = 1

    //noise map info
    private NoiseMap elevationMap;
    private NoiseMap moistureMap;
    private NoiseMap heatMap;

   
    public GenStepNoise(GenConfigs genConfig){
        
        worldMaxWidth = genConfig.worldBounds.X;
        worldMaxHeight = genConfig.worldBounds.Y;
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
    public GenerationData RunStep(Vector2 origin){
        elevationMap.Offset = origin;
        heatMap.Offset = origin;
        moistureMap.Offset = origin;
        GenerationData generationData = new(){
            elevationMap =  GenarateHeatMap(origin, 0),
            //GenerateElevationMap(origin),
            // heatMap = GenarateHeatMap(origin),
        };
       
        return generationData;
    }

    /// <summary>
    /// generateing elevation map which outlines the primary parts of the terrain
    /// </summary>
    private float[,] GenerateElevationMap(Vector2 origin){
        float[,] elevation = new float[Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE];
        
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {                
                //set all value sto represnt the sea level
                if (elevationMap[x,y] < seaLevel)
                {
                    elevation[x,y] = 0;
                }
                //set all value sto represnt a mountain
                else if (elevationMap[x,y] >= mountainSize)
                {
                    elevation[x,y] = 1;
                }
                //set all value sto represnt the ground
                else{
                    elevation[x,y] = 0.5f;
                }
            }
        }

        return elevation;
    }

    //kinda works 
    private float[,] GenerateWorldHeat(Vector2 origin, int center){
        float[,] noiseMap = new float[Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE];

        // dicide the tempeture based on distnace from central point/equator
        for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
        {
            float sampleY = y + origin.Y;

            //calaculate noise value based on it's distnace
            // well also ensuring that it's within the bounds
            float noise = Math.Abs(sampleY - center) / (64*2);//need a figure out this

            for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
            {
                //apply the noise vallue for all points at this Row
                noiseMap[x,y] =noise;
            }
        }
        return noiseMap;
        //4.2cos ((x - 1)π/6) + 13.7
    } 

    private float[,] GenarateHeatMap(Vector2 origin,int center){
        float[,] worldHeat = new float[Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE];
        float[,] equatorHeat = GenerateWorldHeat(origin,center);

        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                float heat = heatMap[x,y] * equatorHeat[x,y];
                heat += elevationMap[x,y]; //make this base on curve at least the second height
                // GD.Print());
                worldHeat[x,y] = heat;
            }
        }
        return worldHeat;
    }

    
}