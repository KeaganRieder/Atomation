namespace Atomation.GameMap;

using Atomation.Things;
using Godot;

/// <summary>
/// this generation step handles teh generation of plants, resources and 
/// other things (tbd)
/// </summary>
public class GenStepPlants : GenStep
{
    private float[,] elevationMap;
    private float[,] treeDensityMap;
    
    private float waterLevel;
    private float mountainSize;

    private float treeDensity;


    public GenStepPlants(GeneratedNoiseMaps noiseMaps, WorldSettings configs)
    {
        Step = 2;
        
        treeDensityMap = noiseMaps.TreeDensityMap;
        elevationMap = noiseMaps.ElevationMap;
        waterLevel = configs.WaterLevel;
        mountainSize = configs.MountainSize;
        treeDensity = configs.TreeDensity;
    }

    public override bool Validate()
    {
        if (treeDensityMap == default)
        {
            GD.PushError("No tree density map given");
            return false;
        }

        return true;
    }

    public override void RunStep(GeneratedMapData generatedMapData)
    {
        if (!Validate())
        {
            generatedMapData.GeneratedFoliage = default;
            return;
        }


        SetSize(generatedMapData.GenSize);

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Plant plant = null;
                if (elevationMap[x,y] > waterLevel && elevationMap[x,y] < mountainSize-.1)
                {
                    if (treeDensityMap[x,y] > 1 - treeDensity)
                    {
                        plant = new Plant(new Vector2(x,y));
                        plant.Configure("Pine");
                    }
                } 

                generatedMapData.GeneratedFoliage[x,y] = plant;
            }
        }
    }

    private void TryToSpawnTree(){

    }

}