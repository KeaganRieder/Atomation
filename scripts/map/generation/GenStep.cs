namespace Atomation.Map;

using Atomation.Resources;
using Godot;

public abstract class GenStep
{
    protected MapData mapData;
    protected DefDatabase defDatabase;
    protected Vector2 genSize;
    protected Vector2 origin;

    public int Step { get; private set; }

    protected GenStep(int step = -1)
    {
        defDatabase = DefDatabase.GetInstance();
        mapData = MapData.GetData();
        origin = Vector2.Zero;
        Step = step;
        DefaultSize();
    }
    
    /// <summary> sets gen size to chunk size X chunk size (32 X 32)</summary>
    public void DefaultSize()
    {
        genSize = new Vector2(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
    }
    public void SetGenSize(Vector2 size)
    {
        genSize = size;
    }

    public void SetOrigin(Vector2 origin){
        this.origin = origin;
    }

    public virtual void RunStep() { }

    public virtual void UpdateSettings() { }
}