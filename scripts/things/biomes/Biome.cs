namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

public struct BiomeLabel
{
    public float minMoisture;
    public float maxMoisture;
    public float minTemperature;
    public float maxTemperature;

}

/// <summary>
/// biomes are used to determine the types of terrain based on
/// the provide elevation, moisture and temperature
/// </summary>
public class Biome : IDef
{
    //     [JsonProperty(Order = 1)]
    private string defName;
    private float minMoisture;
    private float maxMoisture;
    private float minTemperature;
    private float maxTemperature;

    [JsonProperty("BiomeTerrain")]
    private Dictionary<float, string> terrain = null;

    [JsonProperty("BiomeColor")]
    public Color Color;

    public string DefName { get => defName; set => defName = value; }
    public float MinMoisture { get => minMoisture; set => minMoisture = value; }
    public float MaxMoisture { get => maxMoisture; set => maxMoisture = value; }
    public float MinTemperature { get => minTemperature; set => minTemperature = value; }
    public float MaxTemperature { get => maxTemperature; set => maxTemperature = value; }


    public virtual string GetKey()
    {
        BiomeLabel biomeLabel = new BiomeLabel()
        {
            minMoisture = MinMoisture,
            maxMoisture = MaxMoisture,
            minTemperature = MinTemperature,
            maxTemperature = MaxTemperature,
        };
        return JsonConvert.SerializeObject(biomeLabel);
    }

    public Biome(float maxTemperature = 0)
    {
        this.maxTemperature = maxTemperature;

    }

    /// <summary>
    /// returns the key for the terrain which is present in the biome
    /// and corresponds to the
    /// </summary>
    public TerrainDef GetTerrain(float elevation)
    {
        //try and get a terrain
        foreach (float terrainHeight in terrain.Keys)
        {
            if (elevation < terrainHeight)
            {
                return ThingDatabase.Instance.GetTerrainDef(terrain[terrainHeight]);
            }
        }

        // if no terrain meeting requirement then pick closest
        return null;
    }


}
