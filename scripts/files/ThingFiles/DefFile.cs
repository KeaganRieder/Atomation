namespace Atomation.Resources;

using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Atomation.Things;
using Newtonsoft.Json;

public class DefFile<defType> where defType : IDef
{
    [JsonProperty("Defs")]
    protected Dictionary<string, defType> defs;

    public DefFile() { }

    /// <summary>
    /// constructor which is used primarily for formatting new def file
    /// </summary>
    public DefFile(Dictionary<string, defType> contents, string path, string fileName)
    {
        defs = contents;
        FileUtility.WriteJsonFile(path, fileName, this);
    }

    /// <summary>
    /// constructor when called reads in def files from given
    /// folder path
    /// </summary>
    public DefFile(string folderPath)
    {
        defs = new Dictionary<string, defType>();

        Initialize(folderPath);
    }

    protected virtual void Initialize(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                DefFile<defType> defFile = FileUtility.ReadJsonFile<DefFile<defType>>(filePath);

                foreach (var def in defFile.defs)
                {
                    CacheFileData(def.Value.GetKey(), def.Value);
                }
            }
        }
        else
        {
            throw new FileNotFoundException($"file read failed: {folderPath} missing");
        }
    }

    protected virtual void CacheFileData(string key, defType obj)
    {
        try
        {
            defs.Add(key, obj);
        }
        catch (Exception Error)
        {
            GD.PushError($"Error failed to add key: {Error.Message}");
        }
    }

    [JsonIgnore]
    public Dictionary<string, defType> FileContents
    {
        get => defs;
    }

    public defType this[string key]
    {
        get
        {
            if (defs.ContainsKey(key))
            {
                return defs[key];
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in the dictionary.");
            }
        }
    }
}