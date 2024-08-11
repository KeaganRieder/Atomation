namespace Atomation.Resources;

using Godot;
using Newtonsoft.Json;

public enum GraphicType
{
//todo
}

/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a graphic
/// </summary>
public class GraphicData
{
    public string texturePath;
    public int variants = 0;
    public Color color;
    public Vector2I graphicSize;
}
