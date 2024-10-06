namespace Atomation.GameMap;

using Godot;

public class GradientMapGenerator : NoiseGenerators
{
    private float center;

    private bool configured;

    public GradientMapGenerator()
    {
        configured = false;
    }

    public GradientMapGenerator(Vector2 offset = default, Vector2I size = default, Vector2I totalSize = default, bool trueCenter = false)
    {
        Configure(offset, size, totalSize, trueCenter);
    }

    public override void SetSize(Vector2I size = default)
    {
        base.SetSize(size);
        noiseMap = new float[size.X, size.Y];
    }

    public void Configure(Vector2 offset = default, Vector2I size = default, Vector2I totalSize = default, bool trueCenter = false)
    {
        SetSize(size);
        SetTotalSize(totalSize);
        SetOffset(offset);
        center = trueCenter ? this.totalSize.Y / 2 : 1;

        configured = true;

    }

    public float[,] Generate()
    {
        if (!configured)
        {
            GD.PushError("Can't generate gradient noise sense it's not configured");
            return default;
        }

        float[,] noiseMap = new float[genSize.X, genSize.Y];

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float val = DistanceFromCenter(y) / totalSize.Y;
                noiseMap[x, y] = val;
            }
        }

        return noiseMap;
    }

    /// <summary>
    /// gets equator heat at given y position
    /// </summary>
    public float GetGradientNoiseValue(int y)
    {
        float temperature = DistanceFromCenter(y) / totalSize.Y;
        return temperature;
    }

    /// <summary>
    /// calculates the given cell is from the equator
    /// </summary>
    private float DistanceFromCenter(int y)
    {
        float distance = Mathf.Abs(y + offset.Y - center);

        return distance;
    }
}