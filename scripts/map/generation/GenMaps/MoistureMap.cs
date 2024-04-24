namespace Atomation.Map;

using Atomation.Things;
using Godot;

public class MoistureMap : GenMaps
{
    private float scale;
    private float dewPoint;
    private float moistureIntensity;
    private float flatteningThreshold;
    private float equatorHeight;

    public MoistureMap(GenSettings mapGenSettings, int width, int height)
    {
        MapSize = new Vector2I(width, height);
        UpdateConfigs(mapGenSettings);
    }

    public override void UpdateConfigs(GenSettings mapGenSettings)
    {
        TotalMapSize = mapGenSettings.WorldSize;
        scale = mapGenSettings.Scale;
        equatorHeight = 0;

        dewPoint = mapGenSettings.MoistureMapConfigs.DewPoint;
        moistureIntensity = mapGenSettings.MoistureMapConfigs.PrecipitationIntensity;
        flatteningThreshold = mapGenSettings.MoistureMapConfigs.FlatteningVal;
        ValidateValues();
    }
    public void CalculateMoisture(float x, float y, Terrain terrain)
    {
        float celsius = terrain.HeatValue * 100;

        float saturatedVapor = 6.11f * 10 * (7.5f * celsius / 237 + celsius);
        float actualVapor = 6.11f * 10 * (7.5f * dewPoint / 237 + dewPoint);

        float humidity = (saturatedVapor == 0) ? (actualVapor) : (actualVapor / saturatedVapor);

        if (humidity > 50)
        {
            humidity = 50;
        }

        humidity = (terrain.HeightValue >= flatteningThreshold) ? (humidity * 2) : 100 - (humidity * 2);

        float precipitation = CalculatePrecipitation(humidity, terrain.HeatValue, y);
        precipitation /= 100;

        if (precipitation < minValue)
        {
            minValue = precipitation;
        }
        if (precipitation > maxValue)
        {
            maxValue = precipitation;
        }
        terrain.MoistureValue = precipitation;

    }

    private float CalculatePrecipitation(float humidity, float temperature, float y)
    {
        float latitude = Mathf.Abs((y + Offset.Y - equatorHeight) / scale) * 0.5f + 0.5f;
        float estimatePrecipitation = -1 * Mathf.Cos(latitude * 3 * (Mathf.Pi * 2)) * 0.5f + 0.5f;

        float simulated = 2.0f * temperature * humidity;

        return moistureIntensity * (simulated + estimatePrecipitation);
    }

}