namespace Atomation.PlayerChar;

using Atomation.Map;
using Atomation.Things;

public class SavedPlayer
{
    public string Name;
    public Coordinate Cords;
    public StatSheet StatSheet;

    public SavedPlayer() { }
    public SavedPlayer(Player toSave)
    {
        Name = toSave.Name;
        Cords = toSave.GetCoordinate();
        StatSheet = toSave.GetStatSheet();
    }
}