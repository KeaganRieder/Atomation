using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public class DefDatabase<defType> where defType : IThing  {
  
    public Dictionary<string,defType> Contents = new Dictionary<string, defType>();

    public defType this[string key]{
        get{
            if (Contents.ContainsKey(key))
            {
                return Contents[key];
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
            }
        }
    }

    public DefDatabase(string folderPath){
        if (Directory.Exists(folderPath))
        {            
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                DefFile<defType> defFile = JsonReader.ReadJson<DefFile<defType>>(filePath);
                foreach (defType def in defFile.defs)
                {    
                    CacheFileData(def.Label, def);        
                }
            }
        }
        else
        {
            throw new FileNotFoundException($"Error file read failed: {folderPath} missing");
        }
    }

    public void CacheFileData(string name, defType obj){
        try
        {
           Contents.Add(name, obj);
        }
        catch (Exception  Error)
        {
            GD.PrintErr($"Error failed to add key: {Error.Message}");
        }
    }
}