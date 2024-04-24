namespace Atomation.Map;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// configs for the Height Map
/// </summary>
public class HeightMapConfigs
{
    /// <summary>
    /// how many layers are in the noise map
    /// </summary>
    public int Octaves;
    /// <summary>
    /// frequency of the noise, 
    /// Low frequency results in smooth noise while high frequency results in rougher, more granular noise
    /// </summary>
    public float Frequency;
    /// <summary>
    /// Frequency multiplier between subsequent octaves. 
    /// Increasing this value results in higher octaves producing noise with finer details and a rougher appearance.
    /// </summary>
    public float Lacunarity;
    /// <summary>
    /// gain of the noise per layer
    /// </summary>
    public float Gain;
    /// <summary>
    /// the noise type which is used
    /// </summary>
    public FastNoiseLite.NoiseTypeEnum NoiseType;
    /// <summary>
    /// decides the fractal type/method which noise gets layered
    /// </summary>
    public FastNoiseLite.FractalTypeEnum FractalType;

    public float SeaLevel;

    public float MountainSize;
}

/// <summary>
/// configs for the Moisture Map
/// </summary>
public class MoistureMapConfigs
{
    /// <summary>
    /// the temperature at which the air has to be cooled down to 
    /// in order to be saturated with water vapor
    /// </summary>
    public float DewPoint;
    /// <summary>
    /// the insistency of precipitation for the map
    /// </summary>
    public float PrecipitationIntensity;
    /// <summary>
    /// maybe
    /// </summary>
    public float FlatteningVal;
}

/// <summary>
/// configs for the Temperature Map
/// </summary>
public class TemperatureMapConfigs
{
    /// <summary>
    /// highest temperature at the equator
    /// </summary>
    public float BaseTemperature;
    /// <summary>
    /// multiplier decreasing the temperature value with increasing latitude
    /// </summary>
    public float TemperatureMultiplier;
    /// <summary>
    /// is the amount of temperature that should be lost for each height step
    /// </summary>
    public float TemperatureLoss;
    /// <summary>
    /// incremental steps at which temperature should be lost
    /// </summary>
    public float TemperatureHeight;
}