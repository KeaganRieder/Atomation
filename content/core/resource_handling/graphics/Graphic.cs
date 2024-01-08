using Godot;
using Newtonsoft.Json;
using System;

public enum EdgeType{
    Default,
    Rough, 
    Smooth,
}
/// <summary>
/// manges manipluating and handling a thinsg texture/graphic
/// applying any mansk or other modifcation to it
/// </summary>
public partial class Graphic {
 
    [JsonProperty]
    private string texturePath;
    [JsonProperty]
    private Vector2 graphicSize;
    [JsonProperty]
    private Color color {get; set;}

    public Graphic(string texturePath,Color color){
        this.texturePath = texturePath;
        this.color = color;
    }

    public void ReadTexture(){
        //todo
    }
    public void ReadMasks(){
        //todo
    }

    public ColorRect GetTexture(){
        Vector2 size = new Vector2(WorldMap.CELL_SIZE,WorldMap.CELL_SIZE);
        return new ColorRect(){
            Color = color,
            Size = size,
        }; 
    }



}