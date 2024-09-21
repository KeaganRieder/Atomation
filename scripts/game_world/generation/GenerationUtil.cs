namespace Atomation.GameMap;

using Godot;

/// <summary>
/// A bunch of utility functions meant to be use during world 
/// generation
/// </summary>
public static class GenerationUtil
{
    public static float[,] GenerateTemperatureMap(Vector2I size, float[,] heatMap, float[,] heightMap, WorldSettings settings)
    {
        float[,] temperatureMap = new float[size.X, size.Y];

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                float temperature = heatMap[x, y] * -1;
                temperature -= heightMap[x, y] * heightMap[x, y];

                temperatureMap[x, y] = temperature + settings.BaseTemperature;
            }
        }

        return temperatureMap;
    }

}