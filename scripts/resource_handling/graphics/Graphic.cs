using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{    
    /// <summary>
    /// graphic configuration file
    /// </summary>
    public class GraphicConfig
    {
        public string TexturePath{get; set;}
        public Color Color {get; set;}

        public GraphicConfig(string texturePath,Color color){
            TexturePath = texturePath;
            Color = color;
            
        }
    }

    /// <summary>
    /// abstarct class which defines the base of all graphics in Atomation
    /// </summary>
    public abstract class Graphic
    {
        protected string texturePath;

        protected Vector2 graphicSize;

        protected Color color;

    }
}
