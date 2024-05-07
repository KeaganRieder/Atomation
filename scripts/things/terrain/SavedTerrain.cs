namespace Atomation.Things;

using Godot;
using Newtonsoft.Json;

public class SavedTerrain : SavedThing
{
    public float Elevation;
    public float Temperature;
    public float Moisture;

    [JsonConstructor]
    protected SavedTerrain() { }

    public SavedTerrain(Terrain toSave) : base(toSave.DefName, toSave.GetCoordinate(), toSave.StatSheet)
    {
        Elevation = toSave.Elevation;
        Temperature = toSave.Temperature;
        Moisture = toSave.Moisture;
    }

}