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
public static class FileUtility
{
	/// <summary>
	/// formats object using custom formatting and then writes it to json
	/// at specified location
	/// </summary>
	public static void WriteJsonFile<ObjType>(string filePath, string fileName, ObjType obj, JsonSerializerSettings settings = null)
	{
		if (settings == null)
		{
			settings = new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented,
			};
		}

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
}