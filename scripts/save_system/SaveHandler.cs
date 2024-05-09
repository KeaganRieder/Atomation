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

        SaveFile save = new SaveFile("Quick Save");
        // new JsonSerializerSettings()
        FileUtility.WriteJsonFile(FilePaths.SAVE_FOLDER, save.GetName(), save,null);
        GD.Print("Saved");
    }

    public void QuickLoad()
    {
        GD.Print("Loading");
        SaveFile save = FileUtility.ReadJsonFile<SaveFile>(FilePaths.SAVE_FOLDER, "Quick Save");
        save.Load();  
    }
}
