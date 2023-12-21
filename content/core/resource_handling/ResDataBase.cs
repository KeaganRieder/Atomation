using Godot;
using System;
using System.Collections.Generic;
using System.IO;

/*
    defines a class which is responsible for loading
    and then caching resources for the game, such as
    the various thing defs
*/
public class ResourceDataBase{
    public const string STATS_FOLDER = FileManger.DEF_FOLDER + "stats/";
    public const string BIOMES_FOLDER = FileManger.DEF_FOLDER + "BIOMES/";
    public const string TERRAIN_FOLDER = FileManger.DEF_FOLDER + "terrain/";

    public Dictionary<string, StatDef> StatDefs;
    public Dictionary<string, TerrainDef> TerrainDefs;
    public Dictionary<string, BiomeDef> BiomeDefs;

    public ResourceDataBase(){
        StatDefs = new(); 
        TerrainDefs = new();
        BiomeDefs = new();
    }

    public void LoadResources(){
        GD.Print("started loading/caching");
        LoadTerrainDefs();
        LoadStatDefs();
        LoadBiomeDefs();
        GD.Print("finished loading/caching");

        
    }
    public void CacheResource<objType>(string name, objType obj, Dictionary<string,objType> cache){
        try
        {
            cache.TryAdd(name,obj);
        }
        catch (Exception Error)
        {
            GD.PrintErr($"Error failed to add key: {Error.Message}");
        }
    }

    //todo figure out a way to do this better

    public void LoadTerrainDefs(){
        string directoryPath = TERRAIN_FOLDER;
        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string filePath in files)
            {
                DefFile<TerrainDef> defFile = JsonReader.ReadJson<DefFile<TerrainDef>>(filePath);

                foreach (TerrainDef def in defFile.defs)
                {              
                    CacheResource(def.defName, def, TerrainDefs);        
                }
            }
        }
        else
        {
            GD.PrintErr($"Error file read failed: {directoryPath} missing");
        }
    }
    public void LoadStatDefs(){
        string directoryPath =  STATS_FOLDER;
        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string filePath in files)
            {
                DefFile<StatDef> defFile = JsonReader.ReadJson<DefFile<StatDef>>(filePath);

                foreach (StatDef def in defFile.defs)
                {              
                    CacheResource(def.defName, def, StatDefs);        
                }
            }
        }
        else
        {
            GD.PrintErr($"Error file read failed: {directoryPath} missing");
        }
    } 
    public void LoadBiomeDefs(){
        string directoryPath =  BIOMES_FOLDER;
        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string filePath in files)
            {
                DefFile<BiomeDef> defFile = JsonReader.ReadJson<DefFile<BiomeDef>>(filePath);

                foreach (BiomeDef def in defFile.defs)
                {              
                    CacheResource(def.defName, def, BiomeDefs);        
                }
            }
        }
        else
        {
            GD.PrintErr($"Error file read failed: {directoryPath} missing");
        }
    } 
    

}