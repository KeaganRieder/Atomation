using Godot;
using Newtonsoft.Json;
using System.IO;
/*
	defines a class which handles the reading of 
    json files. theres only one type of this class
    so it's static
*/
public static class JsonReader
{
    public static DataType ReadJson<DataType>(string filePath){
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            DataType convertedData = JsonConvert.DeserializeObject<DataType>(jsonData);
            return convertedData;
        }
        else
        {
            GD.PrintErr($"Error reading file: {filePath} doesn't exsit");
            return default;
        }
    }

    public static DataType ReadDefFile<DataType>(string filepath, string fileName){
        string file =  FileManger.DEF_FOLDER + filepath + fileName;
        return ReadJson <DataType>(file);
    }  

}
