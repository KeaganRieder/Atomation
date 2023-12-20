using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class JsonFileTesting : Node
{
	public DefFile<TerrainDef> terrain_natural = new DefFile<TerrainDef>();

	public override void _Ready()
	{
		string filePath = "terrain_defs/";
		string fileName = "terrain_natural.json";
		terrain_natural.defs = new TerrainDef[2]{
			new TerrainDef{
				defName = "FloorBase", //not writing/serializing
				description = "Default Floor", //not writing/serializing
				texturePath = "Default Path",
				supports = "DefaultSupport",
				
			},
			new TerrainDef{
				defName = "Grass",//not writing/serializing
				description = "it's grassy",//not writing/serializing
				texturePath = "something grass/todo",
				supports = "Heavy",
				statBases = new Dictionary<string, float>{
					{"Flammability", 1.5f},
					{"WorkToBuild", 0f},
					{"WalkSpeed", .5f},
					{"Beauty", .5f},
				}
			},
		};
		
		// JsonWriter.WriteDefFile(filePath, fileName, terrain_natural);
		
		DefFile<TerrainDef>test = new();
	 	test = JsonReader.ReadDefFile<DefFile<TerrainDef>>(filePath, fileName);
		GD.Print( "test \n" +
			JsonConvert.SerializeObject(test, Formatting.Indented, new JsonSerializerSettings{
			NullValueHandling = NullValueHandling.Ignore,
			})
		);
		base._Ready();
	}

	//todo make test for reading
}
