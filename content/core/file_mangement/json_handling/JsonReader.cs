using Godot;
using System;
using System.IO;
using System.Text.Json;
/*
	defines a class which handles the reading of 
    json files. theres only one type of this class
    so it's static
*/
public static class JsonReader
{
    public const string DATA_PATH = "data/";
    public const string DEF_PATH = "core/defs/";

    public static DataType ReadJson<DataType>(string filePath){
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GD.Print(jsonData);
            DataType convertedData = JsonSerializer.Deserialize<DataType>(jsonData);
            return convertedData;
        }
        else
        {
            GD.PrintErr($"Error reading file: {filePath} doesn't exsit");
            return default;
        }
    }

    public static DataType ReadDefFile<DataType>(string filepath, string fileName){
        string file = DATA_PATH + DEF_PATH + filepath + fileName;
        GD.Print(file);
        return ReadJson <DataType>(file);
    }

    

}