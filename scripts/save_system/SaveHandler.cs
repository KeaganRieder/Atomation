namespace Atomation.Resources;

using Atomation.Map;
using Atomation.Player;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// class which defines how game saves are handled
/// </summary>
public class SaveHandler
{
    private static SaveHandler instance;

    private SaveHandler()
    {

    }

    public static SaveHandler GetInstance()
    {
        if (instance == null)
        {
            instance = new SaveHandler();
        }
        return instance;
    }

    public void QuickSave()
    {
        GD.Print("Saving game");

        //maybe make this in the save files constructor, which just requires 
        // a string and then does the rest? 
        SaveFile save = new SaveFile();

        save.Name = "Quick Save";
        save.SavedPlayerData = PlayerChar.GetInstance().Save();
        save.SavedMapData = WorldMap.GetInstance().Save();
//new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore,Formatting = Formatting.Indented }
        FileUtility.WriteJsonFile(FilePaths.SAVE_FOLDER, save.Name, save,new JsonSerializerSettings());
        GD.Print("Saved");
    }

    public void QuickLoad()
    {
        GD.Print("Loading");
        SaveFile save = FileUtility.ReadJsonFile<SaveFile>(FilePaths.SAVE_FOLDER, "Quick Save");
        WorldMap.GetInstance().Load(save.SavedMapData);
        PlayerChar.GetInstance().Load(save.SavedPlayerData);
    }

}
