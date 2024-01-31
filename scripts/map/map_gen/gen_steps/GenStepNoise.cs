
/// <summary>
/// defiens the generation step for generating noise maps, 
/// which are pass to generation steps following, and help determine
/// asspects in the game world
/// </summary>
public class GenStepNoise : GenStep
{
    private GenerationData generationData;

    private float [,] elevationMap;
    private float [,] moistureMap;
    private float [,] heatMap;

    // private NoiseMap elevationMap;
    // private NoiseMap moistureMap;
    // private NoiseMap heatMap;

    //GenerationData
    public override void RunStep(){

    }

    private void GenerateElevationMap(){
        
    }

    private void GenarateHeatMap(){
        
    }

    
}