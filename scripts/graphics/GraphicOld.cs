using Godot;
using Newtonsoft.Json;
using System;
using Atomation.Map;
public enum EdgeType{
    Default,
    Rough, 
    Smooth,
}

/// <summary>
/// class mainly used to store and transfer configurtaion data
/// used to create a new graphic
/// </summary>
public class GraphicData
{
    public string texturePath{get; set;}
    public Color color {get; set;}
}

/// <summary>
/// manages manipluating and handling a thinsg texture/graphic
/// applying any mansk or other modifcation to it
/// TODO: clean up and make other varients
/// </summary>
public partial class Graphic {
 
    [JsonProperty]
    private string texturePath;
    [JsonProperty]
    private Vector2 graphicSize;
    [JsonProperty]
    private Color color {get; set;}

    private ColorRect colorRect;

    public Graphic(string texturePath, Color color, Node2D parent){
        this.texturePath = texturePath;
        this.color = color;

        colorRect = new ColorRect(){
           Size = new Vector2(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE),
           Color = color,
        };
        parent.AddChild(colorRect);
    }
    public Graphic(GraphicData configs,Node2D parent){
        texturePath = configs.texturePath;
        color = configs.color;

        colorRect = new ColorRect(){
           Size = new Vector2(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE),
           Color = color,
        };
        parent.AddChild(colorRect);
    }  


}