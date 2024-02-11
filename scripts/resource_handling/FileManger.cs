using Godot;
using System;
using System.Collections.Generic;
using Atomation.Thing;

namespace Atomation.ResHandling
{
	/// <summary>
	/// collection of defs meant to be really used for formating files, which 
	/// define 'things'
	/// </summary>
	public struct DefFile<datatype>
	{
		public datatype[] defs;

		public DefFile(datatype[] defs)
		{
			this.defs = defs;
		}
	}

	/*
		the manger of all resources for the game. this serves
		the role of manging the handling of resources and being 
		the base class for all things that relate to reosurces
		for the game
		this is one of th emany class which will run during 
		the games start up
	*/
	public partial class FileManger : Node
	{
		//user:// for later
		public const string DEF_FOLDER = "data/core/defs/";
		public const string SAVE_FOLDER = "saves/";
		public const string TEXTURE_FOLDER = "resources/textures";
		public const string AUDIO_FOLDER = "resources/audio";

		public FileManger()
		{
		}

		public override void _Ready()
		{
			LoadFiles();
		}


		// JsonReader
		public void LoadFiles()
		{
			GD.Print("Loading Respurces");
			DefResources.LoadResources();//todo make this threaded
		}

		/// <summary>
		/// used for debuging/inital file cretaion only
		/// </summary>
		public void FormatFiles()
		{
			// Terrain[] natural = new Terrain[]{
			//     new Terrain("Grass","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",1.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.8f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/grass", new Color())
			//     ),
			//     new Terrain("Soil","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",1.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.8f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/soil", new Color())
			//     ),
			//     new Terrain("Gravel","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",0.5f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.6f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",-1.0f,0,0)}
			//     }, new Graphic("terrain/natural/gravel", new Color())
			//     ),
			//     new Terrain("Sand","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",0.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.7f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/sand", new Color())
			//     )
			// };
			// Terrain[] water = new Terrain[]{
			//     new Terrain("Marsh","", new Dictionary<string, StatBase>{
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.5f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/grass", new Color())
			//     ),
			//     new Terrain("ShallowOcean","", new Dictionary<string, StatBase>{
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.5f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/soil", new Color())
			//     ),
			//     new Terrain("ShallowWater","", new Dictionary<string, StatBase>{
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.5f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",-1.0f,0,0)}
			//     }, new Graphic("terrain/natural/shallow_water", new Color())
			//     ),
			//     new Terrain("ChestDeepOcean","", new Dictionary<string, StatBase>{
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.4f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/chest_deep_ocean", new Color())
			//     ),
			//     new Terrain("DeepOcean","", new Dictionary<string, StatBase>{
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.0f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/deep_ocean", new Color())
			//     )
			// };
			// Terrain[] stone = new Terrain[]{
			//     new Terrain("Slate","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",0.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.7f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/slate", new Color())
			//     ),
			//     new Terrain("marble","", new Dictionary<string, StatBase>{
			//         {"Fertility", new Stat("Fertility", "Objects fertility",0.0f,0,0)},
			//         {"Walkspeed", new Stat("Walkspeed", "Objects Walkspeed ",0.7f,0,0)},
			//         {"Beauty", new Stat("Beauty", "Objects Beauty",0.0f,0,0)}
			//     }, new Graphic("terrain/natural/marble", new Color())
			//     ),
			// };
			Biome[] temperateBiomes = new Biome[]{
			new Biome("Planes",.5f,.5f,.5f),
			new Biome("tempForest",.5f,.5f,.5f),
		};

			// DefFile<Terrain> naturalDefs = new DefFile<Terrain>(natural);
			// DefFile<Terrain> waterDefs = new DefFile<Terrain>(water);
			// DefFile<Terrain> stoneDefs = new DefFile<Terrain>(stone);

			// DefFile<Biome> temperateBiomeDefs = new DefFile<Biome>(temperateBiomes);

			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH,"terrain_natural", naturalDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH,"terrain_water", waterDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH,"terrain_stone", stoneDefs);
			// JsonWriter.WriteFile(DefResources.BIOME_DEFS_PATH,"biomes_temperate", temperateBiomeDefs);
		}

	}
}
