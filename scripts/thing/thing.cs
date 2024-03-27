using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
	/// <summary>
	/// definition file used in creating new instances of things 
	/// </summary>
	public abstract class ThingDef : IThing
    {        
        //maybe make inherit from resource/make this a custom resources?
        [JsonProperty("name",Order = -2)]
        public string Name { get; set; }
        [JsonIgnore]
		public virtual string Key {get => Name;}
        [JsonProperty("description", Order = -2)]
        public string Description { get; set; }
    }

	/// <summary>
	/// base class for all things in game
	/// </summary>
	public abstract class Thing : IThing 
	{
		protected string name = "";
		protected string description;

		public virtual string Name { get => name; set { name = value; } }
		public virtual string Key {get => name;}
		public virtual string Description { get => description; set { description = value; } }
	}

	public interface IThing
	{
		/// <summary>
		/// objects name
		/// </summary>
        [JsonProperty("name",Order = -2)]
		public string Name { get; set; }
		/// <summary>
		/// key for cache dictionary, in order to properly sort
		/// and retrieve the item from it
		/// </summary>
        [JsonProperty("name",Order = -2)]
		public string Key { get;}
	}
}
