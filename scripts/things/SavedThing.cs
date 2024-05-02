namespace Atomation.Things;

using Atomation.Map;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// decide the required information to be saved from any given complex thing
/// </summary>
public abstract class SavedThing
{
    public string Name;
    public Coordinate Cords;
    public StatSheet StatSheet;

    protected SavedThing() { }
    protected SavedThing(string name, Coordinate cords, StatSheet statSheet)
    {
        Name = name;
        Cords = cords;
        StatSheet = statSheet;
    }

}