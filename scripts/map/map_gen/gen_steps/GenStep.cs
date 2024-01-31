using Godot;

/// <summary>
/// bsae cabstarct class for all gensteps
/// </summary>
public abstract class GenStep
{
    //maybe?   
    public virtual Vector2 NormalizeCords(int x, int y){
        float xCord = x * WorldMap.CELL_SIZE;
        float yCord = y * WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
    }
    public virtual void RunStep(){}
}