using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// defines the generation step for generating the terrian
/// </summary>
public class GenStepTerrain
{
    private float waterLevel = .2f;
    private float mounatinSize = .8f; //no mountains is anything above 1

    Dictionary<Vector2, Tile> generatedTerrain;

    public GenStepTerrain(float waterLevel, float mounatinSize){
        // generatedTerrain = new Dictionary<Vector2, Tile>();
        this.waterLevel = waterLevel;
        this.mounatinSize = mounatinSize;
    }

    public float WaterLevel{
        get=> waterLevel; 
        set{waterLevel = Math.Clamp(value,.2f,.5f);}
    }
    public float MounatinSize{
        get=> mounatinSize; 
        set{mounatinSize = Math.Clamp(value,.7f,1f);}
    }

    public void RunStep(){
        /*out GeneratedChunk chunk data*/

    }

    public void GenerateTerrain(){

    }
    public void GenerateElevation(){
        
    }


}