namespace Atomation.Resources;

using System;
using System.Collections.Generic;
using System.IO;
using Atomation.Things;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// a collection of data used in configuring instances of different objects 
/// </summary>
public class DefFile
{
    private Dictionary<string, Dictionary<string, object>> defs;

    [JsonConstructor]
    public DefFile() { }

    public DefFile(string folderPath)
    {
        defs = new Dictionary<string, Dictionary<string, object>>();

        ReadDefGroup(folderPath);
    }

    public DefFile(List<Thing> ThingDefs, string filePath, string fileName)
    {
        FormatThingDefFile(ThingDefs, filePath, fileName);
    }

    [JsonProperty]
    public Dictionary<string, Dictionary<string, object>> Defs { get => defs; set => defs = value; }

    /// <summary>
    /// formats a def file to be a collection of thing defs
    /// </summary>
    public void FormatThingDefFile(List<Thing> ThingDefs, string filePath, string fileName)
    {
        defs = new Dictionary<string, Dictionary<string, object>>();

        foreach (var def in ThingDefs)
        {
            defs.Add(def.ID, def.FormatThingDef());
        }
        this.WriteJsonFile(filePath, fileName);
    }

    protected void ReadDefGroup(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                DefFile defFile = fileUtil.ReadJsonFile<DefFile>(filePath);

                foreach (var def in defFile.Defs)
                {
                    CacheFileData(def.Key, def.Value);
                }
            }
        }
        else
        {
            throw new FileNotFoundException($"file read failed: {folderPath} missing");
        }
    }

    protected virtual void CacheFileData(string key, Dictionary<string, object> obj)
    {
        try
        {
            Defs.Add(key, obj);
        }
        catch (Exception Error)
        {
            GD.PushError($"Error failed to add key: {Error.Message}");
        }
    }

    public Dictionary<string, object>  this[string key]
    {
        get
        {
            if (Defs.ContainsKey(key))
            {
                return Defs[key];
            }
            else
            {
                return default;
               
            }
        }
    }
}