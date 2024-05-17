namespace Atomation.Pawns;

using Godot;
using Atomation.Things;

public class SavedPlayer
{
    public string Name;
    public Vector2 Cords;
    public StatSheet StatSheet;

    public SavedPlayer() { }
    public SavedPlayer(Player toSave)
    {
        Name = toSave.Name;
        Cords = toSave.Position;
        StatSheet = toSave.GetStatSheet();
    }
}