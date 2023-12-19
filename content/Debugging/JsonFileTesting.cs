using Godot;
using System;
using System.Collections.Generic;

public partial class JsonFileTesting : Node
{
	public DefFile<FloorDef> terrain_natural = new DefFile<FloorDef>();

	public override void _Ready()
	{
		string filePath = "terrain_defs/";
		string fileName = "terrain_natural.json";
		terrain_natural.defs = new FloorDef[2]{
			new FloorDef{
				defName = "FloorBase",
				description = "Default Floor",
				texturePath = "Default Path",
				supports = "DefaultSupport",
				statBases = null,
			},
			new FloorDef{
				defName = "Grass",
				description = "it's grassy",
				texturePath = "something grass/todo",
				supports = "Heavy",
				// statBases= new Dictionary<string, float>{
				// 	{"Flammability", 1.5f},
				// 	{"WorkToBuild", 0f},
				// 	{"WalkSpeed", .5f},
				// 	{"Beauty", .5f},
				// }
			},
		};
		
		JsonWriter.WriteDefFile(filePath, fileName, terrain_natural);
		base._Ready();
	}

}
