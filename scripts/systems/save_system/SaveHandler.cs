namespace Atomation.Systems;

using Atomation.Resources;
using Godot;

/// <summary>
/// class which defines how game saves are handled
/// </summary>
public class SaveHandler
{
    private static SaveHandler instance;

    public static SaveHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveHandler();
            }
            return instance;
        }
    }

    private SaveHandler() { }

    public void QuickSave()
    {
        GD.Print("Saving game");

        SaveFile save = new SaveFile("Quick Save");
        FileUtility.WriteJsonFile(FilePaths.SAVE_FOLDER, save.GetName(), save, null);
        GD.Print("Saved");
    }

    public void QuickLoad()
    {
        GD.Print("Loading");
        SaveFile save = FileUtility.ReadJsonFile<SaveFile>(FilePaths.SAVE_FOLDER, "Quick Save");
        save.Load();
    }
}
