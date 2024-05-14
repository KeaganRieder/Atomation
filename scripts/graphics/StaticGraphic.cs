namespace Atomation.Resources; //maybe make this into a graphics name space?

using Atomation.Map;
using Godot;

/// <summary>
/// a static graphic which stays the same after setting
/// </summary>
public partial class StaticGraphic : Sprite2D
{
    private Vector2I graphicSize;
    private int variants;

    public string TexturePath { get; private set; }
    public Color DefaultColor { get; set; }

    public StaticGraphic()
    {
        TexturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png";

        graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE);
        Position = graphicSize / 2;
        SetTexture();
    }

    public StaticGraphic(string texturePath, int variants, Vector2I graphicSize, Color defaultColor)
    {
        TexturePath = FilePaths.TEXTURE_FOLDER + texturePath;
        this.variants = variants;
        this.graphicSize = graphicSize;
        Position = graphicSize / 2;

        DefaultColor = defaultColor;
        SetTexture();
    }

    public void Configure(GraphicData configs)
    {
        DefaultColor = configs.color;
        TexturePath = FilePaths.TEXTURE_FOLDER + configs.texturePath;

        graphicSize = configs.graphicSize;
        variants = configs.variants;

        SetTexture();
    }

    private void SetTexture()
    {
        Texture2D texture;

        if (variants > 1)
        {
            Texture2D[] textureArray = FileUtility.ReadTextureGroup(TexturePath, graphicSize, variants);
            RandomNumberGenerator rng = new RandomNumberGenerator();
            texture = textureArray[rng.RandiRange(0, variants)];
        }
        else
        {
            texture = FileUtility.ReadTexture(TexturePath, graphicSize);
        }
        Texture = texture;

        SetDefaultColor();
    }

    public void SetSize(Vector2I size){
        graphicSize = size;
        SetTexture();
    }
    public void SetScale(Vector2 scale){
        Scale = scale;
    }

    public void SetDefaultColor()
    {
        Modulate = DefaultColor;
    }

}