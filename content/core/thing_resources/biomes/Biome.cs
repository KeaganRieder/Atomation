using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// biomes are used to determine the types of terrain based on
/// the provide elevation, mositure and tempeture
/// </summary>
public class Biome : IThingNew{
    [JsonProperty("name")]
    private string name = "";
    [JsonProperty("height")]
    private float heightValue;
    [JsonProperty("heat")]
    private float heatValue;
    [JsonProperty("moisture")]
    private float moistureValue;
    [JsonProperty("terrain types")]
    private Dictionary<float, string> terrain;

    public Biome(string name, float heightValue, float heatValue, float moistureValue){
        this.name = name;
        this.heightValue = heightValue;  
        this.heatValue = heatValue;  
        this.moistureValue = moistureValue;  
    }

    public string Name{get => name; set{name = value;}}
    public float HeightReq{get => heightValue; set{heightValue = value;}}
    public float HeatReq{get => heatValue; set{heatValue = value;}}
    public float MoistureReq{get => moistureValue; set{moistureValue = value;}}

    public string Terrain(float heightVal){
        if (terrain.TryGetValue(heightVal, out string terrainId))
        {
            return terrainId;
        }
        else
        {
            throw new KeyNotFoundException($"terrain with id:{heightVal} isn't presnt in {this.name}");
        }
    }
}