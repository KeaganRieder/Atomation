namespace Atomation.GameMap;

using Godot;
using System.Collections.Generic;

/// <summary>
/// A collection of values used to configure the world generation
/// </summary>
public class WorldConfigs
{
    public Dictionary<string, object> Values { get; set; }

    public NoiseConfigs ElevationNoise { get; set; }
    public NoiseConfigs MoistureNoise { get; set; }
    public NoiseConfigs TemperatureNoise { get; set; }
    public NoiseConfigs VegetationNoise { get; set; }

    public WorldConfigs(){
        Values = new Dictionary<string, object>();
        ElevationNoise = new NoiseConfigs("Elevation");
        MoistureNoise = new NoiseConfigs("Moisture");
        TemperatureNoise = new NoiseConfigs("Temperature");
        VegetationNoise = new NoiseConfigs("Vegetation");
    }
}

/// <summary>
/// A collection of values used to configure the noise map generators
/// </summary>
public class NoiseConfigs
{
    /// <summary>
    /// The name of of the noise that will get generated
    /// </summary>
    public string Name{get; set;}

    /// <summary>
    /// the noise object used to get values from
    /// </summary>
    public FastNoiseLite Noise{get; set;}

    public NoiseConfigs(string name){
        if (name == null)
        {
            GD.PushError("noise config name can't be null");
        }
        Noise = new FastNoiseLite();
    }
}
