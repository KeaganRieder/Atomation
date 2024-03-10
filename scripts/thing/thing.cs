using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
	/// <summary>
	/// Thing defines the foundations fo all object that appear in the game world
	/// contain values all of these object have like a name or description
	/// </summary>
	public abstract class Thing : IThing
	{
		//maybe make inherit from resource/make this a custom resources?
		[JsonProperty("name", Order = 1)]
		protected string name = "";
		[JsonProperty("description", Order = 2)]
		protected string description;

		[JsonIgnore]
		public virtual string Name { get => name; set { name = value; } }
		[JsonIgnore]
		public virtual string Label {get => name;}
		
		[JsonIgnore]
		public virtual string Description { get => description; set { description = value; } }

		//todo make something that formats the description of things
		//so it reads out like ${name} is .... or it reads through a description
		//and places an object name at every location which has {name} though this
		//may want a just be handled inside the actual things
	}

	//interface for things
	public interface IThing
	{
		/// <summary>
		/// objects name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Label used as key for cache dictionary, in order to properly sort
		/// and retrieve the item from it
		/// </summary>
		public string Label { get;}
	}
}
