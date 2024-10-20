namespace Atomation.Resources;

using System;
using System.Collections.Generic;
using System.IO;
using Atomation.GameMap;
using Godot;

/// <summary>
/// class which handles and object graphic. which often 
/// is what gets displayed to the player during run time
/// </summary>
public partial class Graphic : Sprite2D
{
    protected string texturePath;
    protected Vector2I graphicSize;
    protected int variants;

    protected Color defaultColor = Colors.White;
    protected Color currentColor;

    public Graphic()
    {
        currentColor = defaultColor;
    }

    public Graphic(string texturePath, int variants, Vector2I graphicSize, Color defaultColor)
    {
        this.texturePath = texturePath;
        this.variants = variants;
        this.graphicSize = graphicSize;

        this.defaultColor = defaultColor;
        currentColor = defaultColor;
    }

    public Vector2I GraphicSize { get => graphicSize; set => graphicSize = value; } //figure out resize
    public string TexturePath { get => texturePath; set => texturePath = value; }
    public Color CurrentColor
    {
        get => currentColor;
        set
        {
            currentColor = value;
            UpdateColor();
        }
    }
    public Color DefaultColor { get => defaultColor; protected set => defaultColor = value; }
    public int GraphicVariants { get => variants; set => variants = value; }
    public int RenderingLayer
    {
        get => ZIndex;
        set
        {
            ZIndex = value;
        }
    }

    public void SetToDefaultColor()
    {
        currentColor = defaultColor;
        Modulate = currentColor;
    }
    protected void UpdateColor()
    {
        Modulate = currentColor;
    }

    /// <summary>
    /// used to configure that graphic with data from the graphic configs class
    /// </summary>
    public void Configure(Dictionary<string, object> configs)
    {
        ConfigureFromConfigs(configs);
        UpdateTexture();
    }

    /// <summary>
    /// used to set texture to png at given path
    /// </summary>
    public void UpdateTexture(string texturePath = null, int variants = -1)
    {
        Texture2D texture2D;

        if (texturePath == null)
        {
            texturePath = this.texturePath;
        }
        if (variants == -1)
        {
            variants = this.variants;
        }

        texturePath = FilePaths.TEXTURE_FOLDER + texturePath;

        if (variants > 1)
        {
            RandomNumberGenerator rng = new RandomNumberGenerator();
            int variant = rng.RandiRange(1, this.variants);
            texture2D = fileUtil.ReadPngFile(graphicSize, texturePath + "_" + variant);
        }
        else
        {
            if (texturePath == FilePaths.TEXTURE_FOLDER + "defaultTexture")
            {
                texture2D = fileUtil.ReadPngFile(graphicSize, texturePath);
            }
            else
            {
                texture2D = fileUtil.ReadPngFile(graphicSize, texturePath + "_" + 1);
            }
        }
        Texture = texture2D;
    }

    /// <summary>
    /// formats the graphic into a graphic configs
    /// </summary>
    public Dictionary<string, object> FormatGraphicConfigs()
    {
        return new Dictionary<string, object>()
        {
            ["TexturePath"] = texturePath,
            ["GraphicSize"] = graphicSize,
            ["Variants"] = variants,
            ["DefaultColor"] = defaultColor,
        };

    }
    /// <summary>
    /// configures graphic using provided configs
    /// </summary>
    public void ConfigureFromConfigs(Dictionary<string, object> configs)
    {
        if (configs == null)
        {
            texturePath = "defaultTexture";
            graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE);
            variants = 0;
            defaultColor = Colors.Purple;
        }
        else
        {
            texturePath = configs.ContainsKey("TexturePath") ? configs["TexturePath"].ToString() : "defaultTexture";
            graphicSize = configs.ContainsKey("GraphicSize") ? configs["GraphicSize"].ConvertJsonObject<Vector2I>() : new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE);
            variants = configs.ContainsKey("Variants") ? Convert.ToInt32(configs["Variants"]) : 0;
            defaultColor = configs.ContainsKey("DefaultColor") ? configs["DefaultColor"].ConvertJsonObject<Color>() : Colors.Purple;
        }


        CurrentColor = defaultColor;
    }


}