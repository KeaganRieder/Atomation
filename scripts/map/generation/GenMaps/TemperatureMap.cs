namespace Atomation.Map;

using Atomation.Things;
using Godot;

/// <summary>
/// generates the height/elevation using simplex noise
/// </summary>
public class TemperatureMap : GenMaps
{
    private float equatorHeight;
    private float baseTemperature;
    private float temperatureMultiplier;
    private float temperatureLoss;
    private float temperatureHeight;

    public TemperatureMap(GenSettings mapGenSettings, int width, int height)
    {
        MapSize = new Vector2I(width, height);
        TotalMapSize = mapGenSettings.WorldSize;
        //0 is dead center due to map generating both in negative and positive directions
        equatorHeight = 0;
        UpdateConfigs(mapGenSettings);
    }

    public override void UpdateConfigs(GenSettings mapGenSettings)
    {
        TotalMapSize = mapGenSettings.WorldSize;
        equatorHeight = 0;
        baseTemperature = mapGenSettings.TemperatureMapConfigs.BaseTemperature;
        temperatureMultiplier = mapGenSettings.TemperatureMapConfigs.TemperatureMultiplier;
        temperatureLoss = mapGenSettings.TemperatureMapConfigs.TemperatureLoss;
        temperatureHeight = mapGenSettings.TemperatureMapConfigs.TemperatureHeight;

        ValidateValues();
    }

    /// <summary>
    /// calculates a given points temperature 
    /// </summary>
    public void CalculateHeat(int y, Terrain terrain)
    {
        // find the distance given point is from equator
        float latitude = Mathf.Abs(y + Offset.Y - equatorHeight);

        // equator heat is the distance given point is from equator
        //and how close it is to bounds
        float equatorHeat = latitude / TotalMapSize.Y;

        float avgTemperature = (equatorHeat * -1 * temperatureMultiplier) - ((terrain.HeightValue / temperatureHeight) * temperatureLoss) + baseTemperature;

        if (avgTemperature < minValue)
        {
            minValue = avgTemperature;
        }
        if (avgTemperature > maxValue)
        {
            maxValue = avgTemperature;
        }

        terrain.HeatValue = avgTemperature;
    }

}