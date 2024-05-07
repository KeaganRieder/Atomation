namespace Atomation.Things;

using Newtonsoft.Json;

public interface IDef
{
    /// <summary>
    /// objects name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// key for cache dictionary, in order to properly sort
    /// and retrieve the item from it
    /// </summary>
    public virtual string GetKey() { return Name; }
}

/// <summary>
/// base class of all simple things like stats and stat modifiers, as well 
/// as def file used to create complex things like terrain and structures 
/// </summary>
public abstract class Def : IDef
{
    [JsonProperty("Name", Order = -1)]
    public string Name { get; set; }

    [JsonProperty("Description", Order = -1)]
    public string Description { get; set; }

    protected Def() { }

    protected Def(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public virtual string GetKey()
    {
        if (Name == "" || Name == null)
        {
            Name = "Undefined_Def";
        }

        return Name;
    }
}