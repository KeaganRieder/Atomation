using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// store various tables used during generation to place things like tiles and
/// resources 
/// </summary>
public class BiomeOld : IThing {
  
  [JsonProperty("label")]
	public string Label{get; set;}
  [JsonProperty("name")]
  public string Name {get; set;}

  [JsonProperty("avgTempeture")]
  public float AvgTempeture{get;set;}
  [JsonProperty("avgRainfall")]
  public float AvgRainfall{get;set;}
   [JsonProperty]
  private Dictionary<float,string> terrain;
  //add plants later

  public BiomeOld(){

  }
  public BiomeOld(Dictionary<float,string> terrain){
    this.terrain = terrain;
  }

  public Terrain Terrain(float height){
    if (terrain.TryGetValue(height, out var terrainKey ))
    {
      return Defs.CreatTerrain(terrainKey);
    }
    else
    {
      foreach (float groundHeight in terrain.Keys)
      {
        if (height < groundHeight)
        {
          return Defs.CreatTerrain(terrain[height]);
        }
      }
      return default;
    }
  }
  


  
}