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

        // public GraphicData(string texturePath,Color color){
        //     TexturePath = texturePath;
        //     Color = color;            
        // }
    }
    /// <summary>
    /// abstract class which defines the base of all graphics in Atomation
    /// </summary>
    public abstract class Graphic
    {
        protected string texturePath;

        protected Vector2 graphicSize;
        protected Color color;

        public virtual Color Color { get => color; set { color = value; } }

        /// <summary>
		/// sets the graphics configuration
		/// </summary>
        public virtual void ConfigureGraphic(GraphicData graphicConfig)
        {

        }
    }
}
