using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Newtonsoft.Json;

public class DefDatabase<defType> where defType : IThingNew  {
  
    public Dictionary<string,string> Contents;

    public DefDatabase(string folderPath){
        Contents = new Dictionary<string, string>();
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
            throw new FileNotFoundException($"file read failed: {folderPath} missing");
        }
    }
    
    public void CacheFileData(string name, defType obj){
        try
        {
           Contents.Add(name, JsonConvert.SerializeObject(obj));
        }
        catch (Exception  Error)
        {
            GD.PrintErr($"Error failed to add key: {Error.Message}");
        }
    }

    public defType this[string key]{
        get{
            if (Contents.ContainsKey(key))
            {
                return JsonConvert.DeserializeObject<defType>(Contents[key]);
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
            }
        }
    }
}