namespace Atomation.Player;

using Atomation.Things;

public class SavedPlayer : SavedThing
{
    public SavedPlayer(){}
    public SavedPlayer(PlayerChar toSave)
    : base(toSave.Name, toSave.GetCoordinate(), toSave.StatSheet)
    {

    }
}