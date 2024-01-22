// using Godot;
// using System;
// using System.Collections.Generic;
// using Newtonsoft.Json;

// public partial class JsonFileTesting : Node
// {
// 	public DefFile<Terrain> fileFormatting = new DefFile<Terrain>();
// 	// public Defs defsTest = new Defs();
// 	public override void _Ready()
// 	{
// 		// test = new ResourceManger();
		
// 		// fileFormatting.defs = new BiomeDef[2]{
// 		// 	new BiomeDef{
// 		// 		defName = "default",
// 		// 		description= "default biome"
// 		// 	},
// 		// 	new BiomeDef{
// 		// 		defName = "Plains",
// 		// 		description= "a plains, found normally in neutral areas",
// 		// 		genReqs = new Dictionary<string, float>{
// 		// 			{"maxTemperature", .7f},
// 		// 			{"minTemperature", .3f},
// 		// 			{"maxMoisture", .7f},
// 		// 			{"minMoisture", .3f},
// 		// 			{"height", .5f},
// 		// 		},
// 		// 		tileTypes = new Dictionary<string, float>{
// 		// 			{"water", .1f},
// 		// 			{"sand", .3f},
// 		// 			{"grass", .5f},
// 		// 			{"rock", .7f},
// 		// 			{"mountain", .7f},
// 		// 		}
// 		// 	},
// 		// };
		
// 		// JsonWriter.WriteDefFile(filePath, fileName, new Terrain{
// 		// 	Name = "default",
// 		// 	Description = "default",
// 		// 	stats = new Dictionary<string, StatBase>{
// 		// 		{"defaultStat" , new StatBase()}
// 		// 	},
// 		// 	graphic = new Graphic(),
// 		// });
		
// 		// DefFile<Terrain>test = new();
// 	 	// test = JsonReader.ReadDefFile<DefFile<Terrain>>(filePath, fileName);
// 		// Defs.LoadDefs();
// 		// GD.Print( "cached terrain \n" +
// 		//  	JsonConvert.SerializeObject(Defs.TerrainDefs.Contents, Formatting.Indented, new JsonSerializerSettings{
// 		//  	NullValueHandling = NullValueHandling.Ignore,
// 		//  	})
// 		// );
// 		base._Ready();
// 		InitalizeFile();
// 	}

// 	//todo make test for reading

// 	public void InitalizeFile(){
// 		string filePath = "data/settings/";
// 		string fileName = "key_bindings.json";
// 		// Dictionary<string,StatBase> temp =  new Dictionary<string, StatBase>{
// 		// 		{"Fertility", new StatBase("Fertility","Objects Fetrility",1)},
// 		// 		{"WalkSpeed", new StatBase("WalkSpeed","effect on WalkSpeed ",.8f)},
// 		// 		{"Beauty", new StatBase("Fertility","Objects Beauty",0)}};
// 		// Graphic tmepg = new Graphic("terrain/natural/grass",new Color());

// 		// fileFormatting.defs = new Terrain[1]{
// 		// 	new("Grass","",temp,tmepg),
// 		// };
// 		Controls controls = new Controls();
		
// 		JsonWriter.WriteDefFile(filePath, fileName, controls);
// 	}
// }
