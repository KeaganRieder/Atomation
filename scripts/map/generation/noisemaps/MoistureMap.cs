namespace Atomation.Map;

using Godot;

public class MoistureMap : NoiseMap
{
    private float dewPoint;

    public MoistureMap() : base()
    {
    }

    public override float CalculateNoise(int x, int y, float elevation, float temperature)
    {
        dewPoint = mapData.DewPoint;
        float intensity = mapData.PrecipitationIntensity;
        float flatteningThreshold = mapData.FlatteningVal;

        float celsius = temperature * 100;

        float saturatedVaporPressure = (458.25f * celsius) / (237.3f + celsius);
        float actualVaporPressure = (458.25f * dewPoint) / (237.3f + dewPoint);

        float humidity = (saturatedVaporPressure == 0) ? (actualVaporPressure) : ((actualVaporPressure / saturatedVaporPressure)) ;

        if (humidity > 50f)
        {
            humidity = 50f;
        }

        if (elevation >= flatteningThreshold)
        {
            humidity = 100 - (humidity * 2);
        }
        else
        {
            humidity = humidity * 2;

        }

        float precipitation = CalculatePrecipitation(humidity, elevation, temperature, intensity, y) / 100;


        if (precipitation > max)
        {
            max = precipitation;
        }
        if (precipitation < min)
        {
            min = precipitation;
        }

        return precipitation;
    }

    private float EstimateBasePrecipitation(int y, float elevation)
    {
        float latitude = Mathf.Abs(y + offset.Y - mapData.EquatorHeight) * 0.5f + 0.5f;
        float value = -1 * Mathf.Cos(latitude * 3f * (Mathf.Pi * 2)) * 0.5f + 0.5f;

        return value;
    }

    private float CalculatePrecipitation(float humidity, float elevation, float temperature, float intensity, int y)
    {

        float estimated = EstimateBasePrecipitation(y, elevation);
        float simulated = 2.0f * temperature * humidity;
        return intensity * (estimated + simulated);
    }
}