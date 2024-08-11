namespace Atomation.Resources;

using Atomation.GameMap;
using Godot;

/// <summary>
/// a static graphic which stays the same after setting
/// </summary>
public partial class StaticGraphic : Sprite2D
{
    private Vector2I graphicSize;
    private int variants;

    private string texturePath;
    private Color defaultColor=Colors.White;
    private Color currentColor;

    public StaticGraphic()
    {
        texturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png";

        graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE);
        Position = graphicSize / 2;
        // SetTexture();
    }

    public StaticGraphic(string texturePath, int variants, Vector2I graphicSize, Color defaultColor, Node2D parent)
    {
        this.texturePath = FilePaths.TEXTURE_FOLDER + texturePath;
        this.variants = variants;
        this.graphicSize = graphicSize;
        Position = graphicSize / 2;

        this.defaultColor = defaultColor;

        SetTexture();
        parent.AddChild(this);
    }

    public void Configure(GraphicData configs)
    {
        defaultColor = configs.color;
        texturePath = FilePaths.TEXTURE_FOLDER + configs.texturePath;

        graphicSize = configs.graphicSize;
        variants = configs.variants;

        SetTexture();
    }
    public Vector2I GraphicSize { get => graphicSize; private set => graphicSize = value; }
    public int Variants { get => variants; private set => variants = value; }
    public string TexturePath { get => texturePath; private set => texturePath = value; }
    public Color DefaultColor { get => defaultColor; set => defaultColor = value; }
    public Color CurrentColor
    {
        get => currentColor; set
        {
            currentColor = value;
            UpdateColor();
        }
    }

    public void SetTexture()
    {
        Texture2D texture;

        if (variants > 1)
        {
            Texture2D[] textureArray = FileUtility.ReadTextureGroup(texturePath, graphicSize, variants);
            RandomNumberGenerator rng = new RandomNumberGenerator();
            texture = textureArray[rng.RandiRange(0, variants)];
        }
        else
        {
            texture = FileUtility.ReadTexture(texturePath, graphicSize);
        }
        Texture = texture;

        CurrentColor = DefaultColor;
    }

    public void UpdateColor()
    {
        Modulate = currentColor;
    }

    // public void SetSize(Vector2I size){
    //     graphicSize = size;
    //     SetTexture();
    // }
    // public void SetScale(Vector2 scale){
    //     Scale = scale;
    // }

    // public void SetDefaultColor()
    // {
    //     Modulate = defaultColor;
    // }

}