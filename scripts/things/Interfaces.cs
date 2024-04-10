using Atomation.Map;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Things
{
    public interface ICompThing
	{
		public Node2D Node { get; }
		public Coordinate Coordinate { get; }
	}

    public interface IThing
	{
		/// <summary>
		/// objects name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// key for cache dictionary, in order to properly sort
		/// and retrieve the item from it
		/// </summary>
		public string Key { get;}
	}
}