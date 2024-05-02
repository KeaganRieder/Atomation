namespace Atomation.Map;

using Godot;

public class TemperatureMap : NoiseMap
{
    public TemperatureMap() : base() { }

    public override float CalculateNoise(int x, int y, float elevation)
    {
        float latitude = Mathf.Abs(y + offset.Y - mapData.EquatorHeight);

        float equatorHeat = latitude / mapData.MapSize.Y;

        float avgTemperature = (equatorHeat * -1 * mapData.TemperatureMultiplier) -
        (elevation / mapData.TemperatureHeightLoss * mapData.TemperatureLoss) + mapData.BaseTemperature;

        return avgTemperature;
    }
}