using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{
    /// <summary>
    /// base class for all objects texture/graphical repression 
    /// in game world
    /// </summary>
    public abstract class Graphic
    {
        protected string texturePath;
        protected Vector2 graphicSize;
        protected Color color;

        public ColorRect ObjGraphic { get; protected set; }
        public Color Color { get => color; set { color = value; } }

        /// <summary>
		/// sets the graphics configuration
		/// </summary>
        public virtual void ConfigureGraphic(GraphicData graphicConfig) { }
    }
}
