
using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public class DefDatabase<defType> where defType : IThing {
    
    public Dictionary<string,defType> contents = new Dictionary<string, defType>();

    public DefDatabase(string folderPath){
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                DefFile<defType> defFile = JsonReader.ReadJson<DefFile<defType>>(filePath);

                foreach (defType def in defFile.defs)
                {    
                    CacheFileData(def.Name, def);        
                }
            }
        }
        else
        {
            GD.PrintErr($"Error file read failed: {folderPath} missing");
        }
    }

    public void CacheFileData(string name, defType obj){
        try
        {
           contents.Add(name, obj);
        }
        catch (Exception  Error)
        {
            GD.PrintErr($"Error failed to add key: {Error.Message}");
        }
    }
}