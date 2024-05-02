namespace Atomation.Resources;

using Godot;
using Newtonsoft.Json;

public enum GraphicType{

}

/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a graphic
/// </summary>
public class GraphicData
{
    [JsonProperty("texturePath", Order = 1)]
    public string TexturePath { get; set; }

    [JsonProperty("Texture Variants", Order = 2)]
    public int Variants;

    [JsonProperty("color", Order = 3)]
    public Color Color { get; set; }

    [JsonProperty("Graphic Size", Order = 4)]
    public Vector2I GraphicSize { get; set; }
}
