using Godot;
using Newtonsoft.Json;


/// <summary>
/// Thing defines the foundations fo all obejct that appear in the game world
/// conatin values all of these obejct have like a name or description
/// </summary>
public abstract partial class Thing : Resource, IThing
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

   //todo make something that formats the description of things
//so it reads out like ${name} is .... or it reads through a descirption
//and places an object name at every location which has {name} though this
//may want a just be handled inside the actual things
}

//interface for things
public interface  IThing
{
    public string Name{get; set;}
}