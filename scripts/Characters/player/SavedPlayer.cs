namespace Atomation.Player;

using Atomation.Map;
using Atomation.Things;

public class SavedPlayer
{
    public string Name;
    public Coordinate Cords;
    public StatSheet StatSheet;

    public SavedPlayer() { }
    public SavedPlayer(PlayerChar toSave)
    {
        Name = toSave.Name;
        Cords = toSave.GetCoordinate();
        StatSheet = toSave.GetStatSheet();
    }
}