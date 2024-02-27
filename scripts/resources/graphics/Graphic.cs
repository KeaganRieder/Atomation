using Godot;
using Newtonsoft.Json;

namespace Atomation.Resources
{      
    /// <summary>
    /// abstract class which defines the base of all graphics in Atomation
    /// </summary>
    public abstract class Graphic
    {
        protected string texturePath;

        protected Vector2 graphicSize;
        protected Color color;
        

        public virtual Color Color{get => color;}
    }
}
