namespace Atomation.Resources;

using Godot;
using Newtonsoft.Json;

public enum GraphicType
{

}

/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a graphic
/// </summary>
public class GraphicData
{
    public string texturePath { get; set; }
    public int variants;
    public Color color { get; set; }
    public Vector2I graphicSize { get; set; }
}
