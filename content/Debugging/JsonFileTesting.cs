using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class JsonFileTesting : Node
{
	public DefFile<BiomeDef> fileFormatting = new DefFile<BiomeDef>();
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
		
		//JsonWriter.WriteDefFile(filePath, fileName, new TerrainDef{
		//	defName = "default",
		//	description = "default",
		//	statBases = new Dictionary<string, float>{
		//		{"default" , 0}
		//	},
		//	graphicData = new GraphicData{
		//		graphicSize = new Vector2(0,0),
		//		color = new(0,0,0,0),
		//		edgeType = default,
		//	}
		//});
		
		// DefFile<TerrainDef>test = new();
	 	//test = JsonReader.ReadDefFile<DefFile<TerrainDef>>(filePath, fileName);
		GD.Print( "cached terrain \n" +
		 	JsonConvert.SerializeObject(test.TerrainDefs.contents, Formatting.Indented, new JsonSerializerSettings{
		 	NullValueHandling = NullValueHandling.Ignore,
		 	})
		);
		base._Ready();
	}

	//todo make test for reading
}
