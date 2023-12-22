using Godot;
using System;

/*
    class which handles an object graphic 
*/
public partial class Graphic : Resource{
    
    private string texturePath;
    private Vector2 graphicSize;
    private Color color {get; set;}

    public Graphic(GraphicData data, string texturePath){
        this.texturePath = texturePath;
        color = data.color;
    }

    public void ReadTexture(){

    }
    public void ReadMasks(){

    }



}