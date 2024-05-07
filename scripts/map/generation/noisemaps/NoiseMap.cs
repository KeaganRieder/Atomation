namespace Atomation.Map;

using Godot;

public abstract class NoiseMap
{
    protected MapData mapData;
    protected Vector2 offset;

    protected float min = 1000;
    protected float max = -1000;

    protected NoiseMap()
    {
        mapData = MapData.GetData();
        offset = Vector2.Zero;
    }

    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }

    public virtual void UpdateConfigs(){ }

    public virtual void PrintMinMax(){
        GD.Print($"Min {min}, Max {max}");
    }

    public virtual float CalculateNoise(int x, int y)
    {
        return 0.0f;
    }
    public virtual float CalculateNoise(int x, int y,float height)
    {
        return 0.0f;
    }
     public virtual float CalculateNoise(int x, int y,float height,float heat)
    {
        return 0.0f;
    }
}
