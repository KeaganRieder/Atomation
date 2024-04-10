using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{
   /// <summary>
	/// used in creating and formatting a config file design to be read, 
	/// and cached at game start and then used in create an instance of 
	/// a graphic
	/// </summary>
    public class GraphicData
    {
        [JsonProperty("texturePath", Order = 3)]
        public string TexturePath { get; set; }
        [JsonProperty("color", Order = 3)]
        public Color Color { get; set; }
    }
}