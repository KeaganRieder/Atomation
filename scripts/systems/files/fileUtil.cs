namespace Atomation.Resources;

using Godot;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// defines utility functions meant to aid in formatting, writing and reading files
/// </summary>
public static class fileUtil
{
    /// <summary>
    /// checks if the provided file path exists, if it doesn't creates it
    /// </summary>
    private static void CheckFilePath(string filePath)
    {
        filePath = ProjectSettings.GlobalizePath(filePath);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }

    /// <summary>
    /// formats the given object to a json file which is then written to the provide
    /// path
    /// </summary>
    public static void WriteJsonFile<ObjType>(this ObjType obj, string filePath, string fileName, JsonSerializerSettings settings = null)
    {
        string jsonData;

        if (settings == null)
        {
            settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };
        }
        jsonData = JsonConvert.SerializeObject(obj, settings);

        CheckFilePath(filePath);

        filePath += fileName + ".json";
        try
        {
            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception error)
        {
            GD.PushError("Error writing file: " + error.Message);
        }

    }

    /// <summary>
    /// reads in json file at provided location, and deserializes to the provided object type
    /// </summary>
    public static ObjType ReadJsonFile<ObjType>(string filePath, string fileName = null)
    {
        if (fileName != null)
        {
            filePath += fileName + ".json";
        }
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            ObjType convertedData = JsonConvert.DeserializeObject<ObjType>(jsonData);
            return convertedData;
        }
        else
        {
            GD.PushError($"File: {filePath} doesn't exist");
            return default;
        }
    }

    /// <summary>
    /// reads in png file at provided location, and deserializes to the provided object type
    /// </summary>
    public static Texture2D ReadPngFile(Vector2I graphicSize, string filePath, string fileName = null)
    {
        Image image;

        if (fileName != null)
        {
            filePath += fileName;
        }
        filePath += ".png";
        if (File.Exists(filePath))
        {
            image = Image.LoadFromFile(filePath);
            if (image.GetSize() != graphicSize)
            {
                image.Resize(graphicSize.X, graphicSize.Y, Image.Interpolation.Bilinear);
            }
        }
        else
        {
            // GD.PushError($"File: {filePath} doesn't exist, assigning default");
            return ReadPngFile(graphicSize, FilePaths.TEXTURE_FOLDER, "defaultTexture");
        }
        return ImageTexture.CreateFromImage(image);
    }

    /// <summary>
    /// converts the json object to the chosen type or returns null
    /// </summary>
    public static objectType ConvertJsonObject<objectType>(this JObject obj){
        return obj != null ? obj.ToObject<objectType>() : default;
    }
     /// <summary>
    /// converts the json object to the chosen type or returns null
    /// </summary>
    public static objectType ConvertJsonObject<objectType>(this object obj){
        if (obj is JObject)
        {
            JObject jObject = (JObject)obj;
            return jObject.ToObject<objectType>();
        }
        GD.PushError($"provided object is {obj.GetType()} not JObject it's");
        return default;
    }
}