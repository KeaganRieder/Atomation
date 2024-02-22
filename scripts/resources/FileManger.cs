using Godot;
using System;
using System.Collections.Generic;
using Atomation.Thing;

namespace Atomation.Resources
{
	/// <summary>
	/// handles loading/managing file and general resource access.
	/// </summary>
	public partial class FileManger
	{
		public const string DEF_FOLDER = "data/core/defs/";
		public const string TEXTURE_FOLDER = "resources/textures";
		public const string AUDIO_FOLDER = "resources/audio";
		public const string CONFIGS = "data/configs/";

		public FileManger()
		{
			// FormatFiles();
			// FormatFiles();
		}

		/// <summary>
		/// loads files, this function is generally called
		/// during start up
		/// </summary>
		public void LoadFiles()
		{
			GD.Print("Loading Resources");
			DefDatabase.LoadResources();
			GD.Print("Loading Complete");
		}

		// /// <summary>
		// /// used for debugging/initial file creation only
		// /// </summary>
		// public void FormatFiles()
		// {

		// 	DefFileOld<BiomeDef> temperateBiomeDefs = new DefFileOld<BiomeDef>(new BiomeDef[]{
		// 		new BiomeDef("Temperate Plains",0,.2f,0,.2f, new Dictionary<float, string>(){
		// 			{-.5f, "grass"}
		// 		}, new Color())
		// 	});

		// 	JsonWriter.WriteFile(DefDataBase.BIOME_DEFS_PATH, "biomes_temperate", temperateBiomeDefs);
		// }
	}
}
/*
TerrainDef[] natural = new TerrainDef[]{
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
			}; 

			// 	Biome[] temperateBiomes = new Biome[]{
			// 	new Biome("Planes",.5f,.5f,.5f),
			// 	new Biome("tempForest",.5f,.5f,.5f),
			// };

			// DefFile<TerrainDef> naturalDefs = new DefFile<TerrainDef>(natural);
			// DefFile<TerrainDef> waterDefs = new DefFile<TerrainDef>(water);
			// DefFile<TerrainDef> stoneDefs = new DefFile<TerrainDef>(stone);

			// 

			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_natural", naturalDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_water", waterDefs);
			// JsonWriter.WriteFile(DefResources.TERRAIN_DEFS_PATH, "terrain_stone", stoneDefs);
*/