using Newtonsoft.Json;

/// <summary>
/// base definition for all stats
/// </summary>
public abstract partial class StatBase : Thing
{
    [JsonProperty]
    protected float baseValue;
    [JsonIgnore]
    public virtual float Value{get => baseValue; set{baseValue = value;}}
}