using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class JsonFileTesting : Node
{
	public DefFile<BiomeDef> fileFormatting = new DefFile<BiomeDef>();

	public override void _Ready()
	{
		string filePath = "biomes/";
		string fileName = "biomesTest.json";
		fileFormatting.defs = new BiomeDef[2]{
			new BiomeDef{
				defName = "default",
				description= "default biome"
			},
			new BiomeDef{
				defName = "Plains",
				description= "a plains, found normally in neutral areas",
				genReqs = new Dictionary<string, float>{
					{"maxTemperature", .7f},
					{"minTemperature", .3f},
					{"maxMoisture", .7f},
					{"minMoisture", .3f},
					{"height", .5f},
				},
				tileTypes = new Dictionary<string, float>{
					{"water", .1f},
					{"sand", .3f},
					{"grass", .5f},
					{"rock", .7f},
					{"mountain", .7f},
				}
			},
		};
		
		JsonWriter.WriteDefFile(filePath, fileName, fileFormatting);
		
		// DefFile<TerrainDef>test = new();
	 	// test = JsonReader.ReadDefFile<DefFile<TerrainDef>>(filePath, fileName);
		// GD.Print( "test \n" +
		// 	JsonConvert.SerializeObject(test, Formatting.Indented, new JsonSerializerSettings{
		// 	NullValueHandling = NullValueHandling.Ignore,
		// 	})
		// );
		base._Ready();
	}

	//todo make test for reading
}
