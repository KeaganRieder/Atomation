// namespace Atomation.UnitTests;

// using System.IO;
// using Atomation.Resources;
// using Atomation.Things;
// using GdUnit4;
// using Godot;
// using static GdUnit4.Assertions;

// [TestSuite]
// public class ThingTest
// {
//     [TestCase]
//     public void TestTerrainDefFilesExist()
//     {
//         string naturalTerrain = FilePaths.TERRAIN_FOLDER + "terrain_natural.json";
//         string terrainWater = FilePaths.TERRAIN_FOLDER + "terrain_natural.json";
//         string terrainStone = FilePaths.TERRAIN_FOLDER + "terrain_natural.json";

//         AssertBool(File.Exists(naturalTerrain)).IsTrue();
//         AssertBool(File.Exists(terrainWater)).IsTrue();
//         AssertBool(File.Exists(terrainStone)).IsTrue();
//     }

//     [TestCase]
//     public void TestStructureDefFilesExist()
//     {
//         string naturalStructure = FilePaths.STRUCTURE_FOLDER + "structure_natural.json";

//         AssertBool(File.Exists(naturalStructure)).IsTrue();
//     }

//     [TestCase]
//     public void TestBiomeFilesExist()
//     {
//         string cold = FilePaths.BIOME_FOLDER + "biome_cold.json";
//         string hot = FilePaths.BIOME_FOLDER + "biome_hot.json";
//         string temp = FilePaths.BIOME_FOLDER + "biome_temperate.json";

//         AssertBool(File.Exists(hot)).IsTrue();
//         AssertBool(File.Exists(cold)).IsTrue();
//         AssertBool(File.Exists(temp)).IsTrue();
//     }

//     [TestCase]
//     public void TestLoadingDefs()
//     {
//         DefDatabase defs = DefDatabase.GetInstance();

//         TerrainDef grassDef = defs.GetTerrainDef("Grass");
        
//         AssertThat(grassDef.Name).IsEqual("Grass");
//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).Name).IsEqual(StatKeys.FERTILITY);
//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).Description).IsEqual("The tile's fertility");
//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).BaseValue).IsEqual(1);
//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).MaxValue).IsEqual(1);
//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).CurrentValue).IsEqual(1);

//         AssertThat(grassDef.StatSheet.GetStat(StatKeys.FERTILITY).Type).IsEqual(StatType.Undefined); 

//         AssertThat(grassDef.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Name).IsEqual("MoveSpeed Modifier");
//         AssertThat(grassDef.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Description).IsEqual("Tiles effect on Move Speed");
//         AssertThat(grassDef.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).TargetStat).IsEqual("Move Speed");
//         AssertThat(grassDef.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Value).IsEqual(1.0f);
//         AssertThat(grassDef.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Type).IsEqual(ModifierType.Flat); 

//         Terrain grass = AutoFree(new Terrain(new Map.Coordinate(Vector2.Zero)));
//         grass.ReadConfigs(grassDef);

//         AssertThat(grass.Name).IsEqual("Grass" + new Map.Coordinate(Vector2.Zero).ToString());
//         AssertThat(grass.StatSheet.GetStat(StatKeys.FERTILITY).Name).IsEqual(StatKeys.FERTILITY);
//         AssertThat(grass.StatSheet.GetStat(StatKeys.FERTILITY).Description).IsEqual("The tile's fertility");
//         AssertThat(grass.StatSheet.GetStat(StatKeys.FERTILITY).BaseValue).IsEqual(1);
//         AssertThat(grass.StatSheet.GetStat(StatKeys.FERTILITY).Type).IsEqual(StatType.Undefined); 

//         AssertThat(grass.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Name).IsEqual("MoveSpeed Modifier");
//         AssertThat(grass.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Description).IsEqual("Tiles effect on Move Speed");
//         AssertThat(grass.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).TargetStat).IsEqual("Move Speed");
//         AssertThat(grass.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Value).IsEqual(1.0f);
//         AssertThat(grass.StatSheet.GetStatModifier(StatKeys.MOVE_SPEED).Type).IsEqual(ModifierType.Flat);
//     }

  
// }