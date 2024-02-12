using Godot;
using System;
using System.Collections.Generic;
using Atomation.Thing;

namespace Atomation.Resources
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
			// FormatFiles();
		}

		public override void _Ready()
		{
			LoadFiles();
		}


		// JsonReader
		public void LoadFiles()
		{
			GD.Print("Loading Resources");
			DefResources.LoadResources();//todo make this threaded
		}

		/// <summary>
		/// used for debugging/initial file creation only
		/// </summary>
		public void FormatFiles()
		{
			/* TerrainDef[] natural = new TerrainDef[]{
				new TerrainDef("Grass","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",1.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.8f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0),
				}, new GraphicConfig("terrain/natural/grass", new Color())),

				new TerrainDef("Soil","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",1.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.8f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/soil", new Color())),

				new TerrainDef("Soil","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",1.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.8f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/soil", new Color())),

				new TerrainDef("Soil","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",1.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.8f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/soil", new Color())),

				new TerrainDef("Gravel","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",0.5f,0,0),
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.6f,0,0),
					new StatDef("Beauty", "Objects Beauty",-1.0f,0,0)
				}, new GraphicConfig("terrain/natural/gravel", new Color())),

				new TerrainDef("Sand","", new StatDef[]{
					new StatDef("Fertility", "Objects fertility",0.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.7f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/sand", new Color())),
			};
			TerrainDef[] water = new TerrainDef[]{
				new TerrainDef("Marsh","", new StatDef[]{
					new StatDef("Walkspeed", "Objects Walkspeed ",0.5f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/grass", new Color())),

				new TerrainDef("ShallowOcean","", new StatDef[]{
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.5f,0,0),
					 new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/soil", new Color())),

				new TerrainDef("ShallowWater","", new StatDef[]{
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.5f,0,0),
					 new StatDef("Beauty", "Objects Beauty",-1.0f,0,0)
				}, new GraphicConfig("terrain/natural/shallow_water", new Color())),

				new TerrainDef("ChestDeepOcean","", new StatDef[]{
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.4f,0,0),
					 new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/chest_deep_ocean", new Color())),

				new TerrainDef("DeepOcean","", new StatDef[]{
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.0f,0,0),
					 new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/deep_ocean", new Color())),
			};

			TerrainDef[] stone = new TerrainDef[]{
				new TerrainDef("Slate","",new StatDef[] {
					 new StatDef("Fertility", "Objects fertility",0.0f,0,0),
					 new StatDef("Walkspeed", "Objects Walkspeed ",0.7f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/slate", new Color())
				),
				new TerrainDef("marble","",new StatDef[] {
					new StatDef("Fertility", "Objects fertility",0.0f,0,0),
					new StatDef("Walkspeed", "Objects Walkspeed ",0.7f,0,0),
					new StatDef("Beauty", "Objects Beauty",0.0f,0,0)
				}, new GraphicConfig("terrain/natural/marble", new Color())
				),
			}; */

			// 	Biome[] temperateBiomes = new Biome[]{
			// 	new Biome("Planes",.5f,.5f,.5f),
			// 	new Biome("tempForest",.5f,.5f,.5f),
			// };

			// DefFile<TerrainDef> naturalDefs = new DefFile<TerrainDef>(natural);
			// DefFile<TerrainDef> waterDefs = new DefFile<TerrainDef>(water);
			// DefFile<TerrainDef> stoneDefs = new DefFile<TerrainDef>(stone);

			// DefFile<Biome> temperateBiomeDefs = new DefFile<Biome>(temperateBiomes);

			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_natural", naturalDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_water", waterDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_stone", stoneDefs);
			// JsonWriter.WriteFile(DefResources.BIOME_DEFS_PATH, "biomes_temperate", temperateBiomeDefs);
		}

	}
}
