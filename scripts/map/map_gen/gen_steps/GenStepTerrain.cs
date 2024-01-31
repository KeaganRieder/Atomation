using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// defines the generation step for generating the terrian
/// </summary>
public class GenStepTerrain : GenStep
{
    private  GeneratedChunk generatedChunk;
    private GenData genData;
    private Dictionary<Vector2, Terrain> generatedTerrain;

    public GenStepTerrain(GenData genData){
        this.genData = genData;
        generatedTerrain = new Dictionary<Vector2, Terrain>();    
    }

    public GeneratedChunk RunStep(GeneratedChunk generatedChunk){
        this.generatedChunk = generatedChunk;
        GenerateTerrain();
        // GenerateElevation();

        return this.generatedChunk;
    }

    public void GenerateTerrain(){
        for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
            {
                float elevation = genData.ElevationMap[x,y];
                //get tile based on data 
                Vector2 cords = NormalizeCords(x,y);
                Terrain terrainTile = DefResources.Terrain(GetTerrain(elevation));
                generatedTerrain.Add(cords,terrainTile);
            }
        }
        // generatedChunk.Terrain = generatedTerrain;
    }
    public void GenerateElevation(){
        
    }

    //tempory function tell biomes are working
    private string GetTerrain(float elevation){
        return "";
    }


}