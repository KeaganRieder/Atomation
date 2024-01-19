using Newtonsoft.Json;

/// <summary>
/// base definition for all stats
/// </summary>
public abstract partial class StatBase : ThingNew
{
    [JsonProperty]
    protected float baseValue;

    public virtual float Value{get => baseValue; set{baseValue = value;}}
}