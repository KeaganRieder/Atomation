using Godot;
using System;
using System.Collections.Generic;
using System.IO;

/*
    defines a class which is responsible for loading
    and then caching resources for the game, such as
    the various thing defs
*/
public class ResourceManger{
    public const string STATS_FOLDER = FileManger.DEF_FOLDER + "stats/";
    public const string BIOMES_FOLDER = FileManger.DEF_FOLDER + "biomes/";
    public const string TERRAIN_FOLDER = FileManger.DEF_FOLDER + "terrain/";

    public DefDatabase<StatDef> StatDataBase;
    public DefDatabase<TerrainDef> TerrainDefs;
    public DefDatabase<BiomeDef> BiomeDefs;

    public ResourceManger(){

        // StatDataBase = new DefDatabase<StatDef>(STATS_FOLDER);
        TerrainDefs = new DefDatabase<TerrainDef>(TERRAIN_FOLDER);
        BiomeDefs = new DefDatabase<BiomeDef>(BIOMES_FOLDER);
    }

}