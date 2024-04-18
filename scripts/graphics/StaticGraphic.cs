namespace Atomation.Resources;

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

    public StaticGraphic() { }

    public StaticGraphic(string texturePath, int variants, Vector2I graphicSize, Color defaultColor)
    {
        TexturePath = FilePaths.TEXTURE_FOLDER + texturePath;
        this.variants = variants;
        this.graphicSize = graphicSize;
        DefaultColor = defaultColor;
        SetTexture();
    }

    public void Configure(GraphicData configs)
    {
        DefaultColor = configs.Color;
        TexturePath = FilePaths.TEXTURE_FOLDER + configs.TexturePath;

        graphicSize = configs.GraphicSize;
        variants = configs.Variants;

        SetTexture();
    }

    private void SetTexture()
    {
        Texture2D texture;

        if (variants > 1)
        {
            Texture2D[] textureArray = FileManger.ReadTextureGroup(TexturePath, variants);
            RandomNumberGenerator rng = new RandomNumberGenerator();
            texture = textureArray[rng.RandiRange(0, variants)];
        }
        else
        {
            texture = FileManger.ReadTexture(TexturePath);
        }

        if (texture.GetImage().GetSize() != graphicSize)
        {
            // GD.PushWarning($"Texture is size is incorrect scaling from {texture.GetImage().GetSize()} to {graphicSize}");
            texture.GetImage().Resize(graphicSize.X, graphicSize.Y, Image.Interpolation.Bilinear);
        }

        Texture = texture;
        SetDefaultColor();
    }

    public void SetDefaultColor()
    {
        Modulate = DefaultColor;
    }

}