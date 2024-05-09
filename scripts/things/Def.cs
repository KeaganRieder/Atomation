namespace Atomation.Things;

using Newtonsoft.Json;

public interface IDef
{
    /// <summary>
    /// objects name
    /// </summary>
    public string defName { get; set; }
    /// <summary>
    /// key for cache dictionary, in order to properly sort
    /// and retrieve the item from it
    /// </summary>
    public abstract string GetKey();
}

/// <summary>
/// base class of all simple things like stats and stat modifiers, as well 
/// as def file used to create complex things like terrain and structures 
/// </summary>
public abstract class Def : IDef
{
    [JsonProperty(Order = -1)]
    public string defName { get; set; }

    [JsonProperty(Order = -1)]
    public string description { get; set; }

    [JsonConstructor]
    protected Def() { }

    protected Def(string name, string description)
    {
        defName = name;
        this.description = description;
    }

    public virtual string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "Undefined_Def";
        }

        return defName;
    }
}