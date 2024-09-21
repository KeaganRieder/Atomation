namespace Atomation.GameMap;

using Godot;

public class GenStep : Generator<object>
{
    /// <summary>
    /// used to determine the order  gen steps run 
    /// </summary>
    public int Step{get; set;}

    public virtual bool Validate()
    {
        GD.PushWarning("genStep  validation hasn't be implemented");

        return false;
    }
    public virtual void RunStep(GeneratedMapData generatedMapData){
        GD.PushWarning("genStep hasn't be implemented");
    }
}