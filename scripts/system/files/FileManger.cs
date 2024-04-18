namespace Atomation.Resources;

using Godot;
using System;
using System.IO;
using Newtonsoft.Json;


/// <summary>
/// Handles files, defining methods allowing,
/// formatting, reading/writing and the process they are read
/// upon game start
/// </summary>
public class FileManger
{
	public FileManger()
	{
	}

	/// <summary>
	/// loads files, this function is generally called
	/// during start up
	/// </summary>
	public void LoadFiles()
	{
		GD.Print("Loading Resources");
		DefDatabase.LoadResources();
		GD.Print("Loading Complete");
	}

	/// <summary>
	/// formats object using default formatting and then writes it to json
	/// at specified location
	/// </summary>
	public static void WriteJsonFile<ObjType>(string filePath, string fileName, ObjType obj)
	{
		JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			Formatting = Formatting.Indented,
			// NullValueHandling = NullValueHandling.Ignore,
		};

		WriteJsonFile(filePath, fileName, obj, settings);
	}
	/// <summary>
	/// formats object using custom formatting and then writes it to json
	/// at specified location
	/// </summary>
	public static void WriteJsonFile<ObjType>(string filePath, string fileName, ObjType obj, JsonSerializerSettings settings)
	{
		filePath = ProjectSettings.GlobalizePath(filePath);

		if (!Directory.Exists(filePath))
		{
			Directory.CreateDirectory(filePath);
		}

		filePath += fileName + ".json";
		string jsonData = JsonConvert.SerializeObject(obj, settings);

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
	/// reads .json file from location and formats it to correct object'
	/// which it then returns
	/// </summary>
	public static ObjType ReadJsonFile<ObjType>(string filePath, string fileName)
	{

		filePath += fileName + ".json";
		return ReadJsonFile<ObjType>(filePath);
	}

	public static ObjType ReadJsonFile<ObjType>(string filePath)
	{
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
	/// reads in .png files, converting it to a texture2d to be used in the game. if
	/// texture at given location doesn't exist returns default texture
	/// </summary>
	public static Texture2D ReadTexture(string filePath)
	{
		Texture2D texture = default;

		if (File.Exists(filePath))
		{
			Image image = Image.LoadFromFile(filePath);
			texture = ImageTexture.CreateFromImage(image);
		}
		else
		{			
			// GD.PushError($"Texture: {filePath} doesn't exist");
			filePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png";
			Image image = Image.LoadFromFile(filePath);
			texture = ImageTexture.CreateFromImage(image);
		}

		return texture;
	}

	/// <summary>
	/// reads in a collection of .png files, converting them to be a texture2d which is used as objects graphics
	/// </summary>
	public static Texture2D[] ReadTextureGroup(string filePath, int variants){

		Texture2D[] textureArray = new Texture2D[variants];

		for (int i = 0; i < variants; i++)
		{
			string path = filePath + "_" +i;
			
			textureArray[i] = ReadTexture(path);
		}

		return default;
	}

	
}
