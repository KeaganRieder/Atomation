namespace Atomation.Player;

using Godot;
using Atomation.Things;
using StatSystem;

public class SavedPlayer
{
    public string Name;
    public Vector2 Cords;
    public StatSheet StatSheet;

    public SavedPlayer() { }
    // public SavedPlayer(Player toSave)
    // {
    //     Name = toSave.Name;
    //     Cords = toSave.Position;
    //     StatSheet = toSave.GetStatSheet();
    // }
}