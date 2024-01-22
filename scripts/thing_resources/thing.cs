using Godot;
using Newtonsoft.Json;

/// <summary>
/// Thing defines the foundations fo all obejct that appear in the game world
/// conatin values all of these obejct have like a name or description
/// </summary>
public abstract partial class ThingNew : Resource, IThingNew
{
    //maybe make inherit from resource/make this a custom resources?
    [JsonProperty("name")]
    protected string name = "";
    [JsonProperty("description")]
    protected string description;
    
    [JsonIgnore]
    public virtual string Name{get => name; set{name = value;}}
    [JsonIgnore]
    public virtual string Description{get => description; set{description = value;}}
}

//interface for things
public interface  IThingNew
{
    public string Name{get; set;}
}