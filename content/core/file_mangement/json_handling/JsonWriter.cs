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
    //user:// for later
    public const string DATA_PATH = "res://data/";
    public const string DEF_PATH = "core/defs/";
    public static void WriteJson(string filePath, object obj){
        string jsonData = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings{
            NullValueHandling = NullValueHandling.Ignore,
        });
        // GD.Print(filePath);
        try
        {
            File.WriteAllText(filePath,jsonData);
        }
        catch (Exception error)
        {
            GD.PrintErr("Error writing file: " + error.Message);
        }
    }

    public static void WriteDefFile(string filePath, string fileName, object obj ){
        string directory = ProjectSettings.GlobalizePath(DATA_PATH+DEF_PATH+filePath);
        if (!Directory.Exists(directory))
        {
            GD.Print("Created: " + directory);
            Directory.CreateDirectory(directory);
        }
        WriteJson(directory+fileName, obj);
    }

    
}