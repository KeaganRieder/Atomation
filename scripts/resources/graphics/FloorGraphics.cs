using Godot;

namespace Atomation.Resources
{
    /// <summary>
    /// decides how to blend seams between two different terrain
    /// and ground types
    /// </summary>
    public enum EdgeType{
        Default,
        Rough, 
        Smooth,
    }

    /// <summary>
    /// decides wether to show the sprite,
    /// height,heat or moisture map value
    /// manly used for debugging map generation
    /// </summary>
    public enum GroundVisualizationMode
    {
        Default = 0,
        Height = 1,
        Heat = 2,
        Moisture = 3,
    }   


    /// <summary>
    /// abstract class which defines the base of all graphics in Atomation
    /// </summary>
    public class FloorGraphics : Graphic
    {
        public FloorGraphics(GraphicConfig graphicConfig){
            color = new Color();
            texturePath = graphicConfig.TexturePath;
            color = graphicConfig.Color;
            color.A = 1;
        }

        // public Color Color{get => color;}
        
    }   
}
