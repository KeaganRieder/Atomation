namespace Atomation.GameMap;

using Godot;

public class GenStepPlants : Generator<object>
{
     private float[,] treeNoiseMap;
    private float[,] temperature;
    private float[,] moisture;

    public GenStepPlants(float[,] treeNoiseMap = default, float[,] temperatureMap = default, float[,] moistureMap = default)
    {
        configure(treeNoiseMap, temperatureMap, moistureMap);
    }

    public void configure(float[,] treeNoiseMap = default, float[,] temperature = default, float[,] moisture = default)
    {
        this.treeNoiseMap = treeNoiseMap;
        this.temperature = temperature;
        this.moisture = moisture;
    }

    protected bool Validate()
    {
        if (treeNoiseMap == null || temperature == null || moisture == null)
        {
            GD.PushError("Can't Generate trees for required layers haven't been set");
            return false;
        }
        return true;
    }

    public void GenerateVegetation(){
        GD.Print("GenStep currently not implemented");
    }
}