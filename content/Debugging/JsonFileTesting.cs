using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class JsonFileTesting : Node
{
	public DefFile<Terrain> fileFormatting = new DefFile<Terrain>();
	public ResourceManger test = new ResourceManger();
	public override void _Ready()
	{
		test = new ResourceManger();
		// string filePath = "terrain/";
		// string fileName = "newtype.json";
		// fileFormatting.defs = new BiomeDef[2]{
		// 	new BiomeDef{
		// 		defName = "default",
		// 		description= "default biome"
		// 	},
		// 	new BiomeDef{
		// 		defName = "Plains",
		// 		description= "a plains, found normally in neutral areas",
		// 		genReqs = new Dictionary<string, float>{
		// 			{"maxTemperature", .7f},
		// 			{"minTemperature", .3f},
		// 			{"maxMoisture", .7f},
		// 			{"minMoisture", .3f},
		// 			{"height", .5f},
		// 		},
		// 		tileTypes = new Dictionary<string, float>{
		// 			{"water", .1f},
		// 			{"sand", .3f},
		// 			{"grass", .5f},
		// 			{"rock", .7f},
		// 			{"mountain", .7f},
		// 		}
		// 	},
		// };
		
		// JsonWriter.WriteDefFile(filePath, fileName, new Terrain{
		// 	Name = "default",
		// 	Description = "default",
		// 	stats = new Dictionary<string, StatBase>{
		// 		{"defaultStat" , new StatBase()}
		// 	},
		// 	graphic = new Graphic(),
		// });
		
		// DefFile<TerrainDef>test = new();
	 	// test = JsonReader.ReadDefFile<DefFile<TerrainDef>>(filePath, fileName);
		GD.Print( "cached terrain \n" +
		 	JsonConvert.SerializeObject(test.TerrainDefs.contents, Formatting.Indented, new JsonSerializerSettings{
		 	NullValueHandling = NullValueHandling.Ignore,
		 	})
		);
		base._Ready();
	}

	//todo make test for reading
}
