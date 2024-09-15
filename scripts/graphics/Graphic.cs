namespace Atomation.Resources;

using System.IO;
using Godot;

/// <summary>
/// class which handles and object graphic. which often 
/// is what gets displayed to the player during run time
/// </summary>
public partial class Graphic : Sprite2D
{
    private string texturePath;
    protected Vector2I graphicSize;
    protected int graphicVariants;

    protected Color defaultColor = Colors.White;
    protected Color currentColor;

    public Graphic()
    {
        currentColor = defaultColor;
    }
    public Graphic(string texturePath, int variants, Vector2I graphicSize, Color defaultColor, Node2D parent)
    {
        this.texturePath = FilePaths.TEXTURE_FOLDER + texturePath;
        this.graphicVariants = variants;
        this.graphicSize = graphicSize;

        this.defaultColor = defaultColor;
        currentColor = defaultColor;

        SetGraphicTexture();
        parent.AddChild(this);
    }

    public Vector2I GraphicSize { get => graphicSize; set => graphicSize = value; }
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
    public int GraphicVariants { get => graphicVariants; set => graphicVariants = value; }
    public int RenderingLayer
    {
        get => ZIndex;
        set
        {
            ZIndex = value;
        }
    }

    protected void UpdateColor()
    {
        Modulate = currentColor;
    }

    /// <summary>
    /// used to configure that graphic with data from the graphic configs class
    /// </summary>
    public void Configure(GraphicData configs)
    {
        defaultColor = configs.Color;
        currentColor = defaultColor;

        texturePath = FilePaths.TEXTURE_FOLDER + configs.TexturePath;
        graphicSize = configs.GraphicSize - Vector2I.One; // minus 1 is fro debugging
        graphicVariants = configs.Variants;

        SetGraphicTexture();
    }

    /// <summary>
    /// sets the texture of the graphic, based on whats read from a file
    /// </summary>
    protected void SetGraphicTexture()
    {
        Texture2D texture2D;
        if (graphicVariants > 1)
        {
            Texture2D[] textureArray = ReadTextureFiles();
            RandomNumberGenerator rng = new RandomNumberGenerator();
            texture2D = textureArray[rng.RandiRange(0, graphicVariants - 1)];
        }
        else
        {
            texture2D = ReadTextureFile(texturePath + "_" + 1);
            UpdateColor();
        }

        Texture = texture2D;
    }

    /// <summary>
	/// reads in .png files, and converts it to a texture2d object at the given size. if 
	/// the path doesn't contain a image then set texture to default Undefined one
	/// </summary>
    protected Texture2D ReadTextureFile(string filePath)
    {
        Image image;
        filePath += ".png";
        if (File.Exists(filePath))
        {
            image = Image.LoadFromFile(filePath);
            if (image.GetSize() != graphicSize)
            {
                image.Resize(graphicSize.X, graphicSize.Y, Image.Interpolation.Bilinear);
            }
        }
        else
        {
            // GD.PushWarning($"texture path {filePath} doesn't exist, setting to default texture");
            filePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture";

            return ReadTextureFile(filePath);
        }

        return ImageTexture.CreateFromImage(image);
    }

    /// <summary>
    /// reads in a collection of .png files, converting them
    /// to be a texture2d which is used as objects graphics
    /// </summary>
    protected Texture2D[] ReadTextureFiles()
    {
        Texture2D[] textureArray = new Texture2D[graphicVariants];

        for (int i = 0; i < graphicVariants; i++)
        {
            string filePath = texturePath + "_" + (i + 1);
            textureArray[i] = ReadTextureFile(filePath);
        }

        return textureArray;
    }
}