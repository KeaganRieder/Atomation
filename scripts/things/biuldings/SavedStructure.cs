namespace Atomation.Things;

using Godot;
using Newtonsoft.Json;

public class SavedStructure : SavedThing
{
    [JsonConstructor]
    protected SavedStructure() { }

    public SavedStructure(Structure toSave) : base(toSave.DefName, toSave.GetCoordinate(), toSave.StatSheet)
    {

    }

    public void Load(Structure temp)
    {
        GD.Print("Loading Not Implemented");
    }
}