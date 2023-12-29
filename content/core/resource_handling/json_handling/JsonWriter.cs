using Godot;
using System;
using System.IO;
using Newtonsoft.Json;
/*
	defines a class which handles the writing of 
    json files. theres only one type of this class
    so it's static
*/
public static class JsonWriter
{
    public static void WriteJson(string filePath, object obj, JsonSerializerSettings settings){
        string jsonData = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        try
        {
            File.WriteAllText(filePath,jsonData);
        }
        catch (Exception error)
        {
            GD.PrintErr("Error writing file: " + error.Message);
        }
    }
    public static void WriteDefFile(string filePath, string fileName, object obj){
        string directory = ProjectSettings.GlobalizePath(FileManger.DEF_FOLDER+filePath);
        JsonSerializerSettings settings = new JsonSerializerSettings{};

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
       
        WriteJson(directory+fileName, obj, settings);
    }

    
}
