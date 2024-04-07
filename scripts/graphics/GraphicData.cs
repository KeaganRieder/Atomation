using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{
    /// <summary>
    /// graphic configuration file
    /// </summary>
    public class GraphicData
    {
        [JsonProperty("texturePath", Order = 3)]
        public string TexturePath { get; set; }
        [JsonProperty("color", Order = 3)]
        public Color Color { get; set; }
    }
}