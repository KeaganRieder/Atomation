namespace Atomation.Things;

using Newtonsoft.Json;

/// <summary>
/// base of all basic things inside the game. these things are often used
/// with other basic things to cerate more complex things
/// </summary>
public abstract class Thing : IThing
{
	[JsonProperty("Name", Order = -2)]
	public virtual string Name { get; set; }

	[JsonIgnore]
	public virtual string Key { get => Name; }

	[JsonProperty("Description", Order = -1)]
	public virtual string Description { get; set; }

	protected Thing(){}

	protected Thing(string name, string description){
		Name = name;
		Description = description;
	}

}
