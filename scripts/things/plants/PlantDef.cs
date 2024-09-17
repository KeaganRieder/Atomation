namespace Atomation.Things;

using System.Collections.Generic;
using Newtonsoft.Json;

public class PlantDef : ThingDef
{
    private float growthTime;

    private Dictionary<string, int> buildCost;

    private int temperatureReq;
    private int moistureReq;
    private float fertilityEffect;

    public PlantDef(){

    }
     
    [JsonProperty]
    public float GrowthTime { get => growthTime; set => growthTime = value; }
    [JsonProperty]
    public Dictionary<string, int> BuildCost { get => buildCost; set => buildCost = value; }
    [JsonProperty]
    public int TemperatureReq { get => temperatureReq; set => temperatureReq = value; }
    [JsonProperty]
    public int MoistureReq { get => moistureReq; set => moistureReq = value; }
    [JsonProperty]
    public float FertilityEffect { get => fertilityEffect; set => fertilityEffect = value; }
}