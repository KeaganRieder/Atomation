namespace Atomation.Resources;

using Atomation.Map;
using Atomation.Resources;
using Atomation.PlayerChar;
using Atomation.Things;
using Godot;
using System.Collections.Generic;

public static class FileFormatting
{
    public static void FormatTerrain()
    {
        Dictionary<string, TerrainDef> terrainNatural = new Dictionary<string, TerrainDef>{
            {"Forest Grass",new TerrainDef("Forest Grass", "it's grass within a forest",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",1)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.7f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/forest_grass",
                Variants = 1,
                Color = Colors.DarkGreen,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Grass",new TerrainDef("Grass", "it's grass",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",1)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,1f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/grass",
                Variants = 1,
                Color = Colors.Green,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
            {"Soil",new TerrainDef("Soil", "it's soil",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",1)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.7f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/soil",
                Variants = 1,
                Color = Colors.Brown,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Rich Soil",new TerrainDef("Rich Soil", "it's rich soil, corps grow well here",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",2)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.7f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/soil",
                Variants = 1,
                Color = Colors.RosyBrown,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Sand",new TerrainDef("Sand", "it's rough, corse and gets everywhere",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/sand",
                Variants = 1,
                Color = Colors.Yellow,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Ice",new TerrainDef("Ice", "it's slippery,and cold",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/Ice",
                Variants = 1,
                Color = Colors.White,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) }
        };

        //
        // rocky terrain
        //

        Dictionary<string, TerrainDef> terrainRocky = new Dictionary<string, TerrainDef>{
            {"Gravel",new TerrainDef("Gravel", "it's made of smaller rocks",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",.5f)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/gravel",
                Variants = 1,
                Color = Colors.Gray,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Marble",new TerrainDef("Marble", "it's white rock which looks nice",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },

             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/rock_floor",
                Variants = 1,
                Color = Colors.WhiteSmoke,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Slate",new TerrainDef("Slate", "it's a black rock which looks nice",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },

             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/rock_floor",
                Variants = 1,
                Color = Colors.DarkGray,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             };

        //
        // Water terrain
        //

        Dictionary<string, TerrainDef> terrainWater = new Dictionary<string, TerrainDef>{
            {"Marsh",new TerrainDef("Marsh", "it's Marshy",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0f)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.4f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/marsh",
                Variants = 1,
                Color = Colors.SeaGreen,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Shallow Ocean",new TerrainDef("Shallow Ocean", "it's Ocean which is shallow",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/ocean",
                Variants = 1,
                Color = Colors.Blue,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Shallow Water",new TerrainDef("Shallow Water", "it's shallow water",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.5f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/water",
                Variants = 1,
                Color = Colors.LightBlue,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Deep Ocean",new TerrainDef("Deep Ocean", "it's Ocean which is Deep",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED, 0f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/ocean",
                Variants = 1,
                Color = Colors.DarkBlue,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
              {"Deep Water",new TerrainDef("Deep Water", "it's deep water, which you have to swim through",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.FERTILITY, new StatBase(StatKeys.FERTILITY,"The tile's fertility",0)}
             },
             new Dictionary<string, StatModifierBase>{
                {StatKeys.MOVE_SPEED, new FlatStatModifier("MoveSpeed Modifier","Tiles effect on Move Speed",StatKeys.MOVE_SPEED,.2f)}
             }),new GraphicData(){
                TexturePath = "terrain/natural/water",
                Variants = 1,
                Color = Colors.RoyalBlue,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             };

        DefFile<TerrainDef> terrainNaturalDefs = new DefFile<TerrainDef>(terrainNatural, FilePaths.TERRAIN_FOLDER, "terrain_natural");
        DefFile<TerrainDef> terrainWaterDefs = new DefFile<TerrainDef>(terrainWater, FilePaths.TERRAIN_FOLDER, "terrain_water");
        DefFile<TerrainDef> terrainStoneDefs = new DefFile<TerrainDef>(terrainRocky, FilePaths.TERRAIN_FOLDER, "terrain_stone");
    }

    public static void FormatStructure()
    {
        Dictionary<string, StructureDef> naturalStructure = new Dictionary<string, StructureDef>{
            {"Slate",new StructureDef("Slate Wall", "A rock face made of slate",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",200)}
             },
             new Dictionary<string, StatModifierBase>{
             }),
             new GraphicData(){
                TexturePath = "terrain/natural/rock_wall",
                Variants = 1,
                Color = Colors.SlateGray,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
             {"Marble wall",new StructureDef("Marble wall", "A rock face made of Marble",
             new StatSheet(new Dictionary<string, StatBase>{
                {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",100)}
             },
             new Dictionary<string, StatModifierBase>{
             }),
             new GraphicData(){
                TexturePath = "terrain/natural/rock_wall",
                Variants = 1,
                Color = Colors.GhostWhite,
                GraphicSize = new Vector2I(WorldMap.CELL_SIZE, WorldMap.CELL_SIZE)
             }) },
        };

        DefFile<StructureDef> NaturalStructure = new DefFile<StructureDef>(naturalStructure, FilePaths.STRUCTURE_FOLDER, "structure_natural");
    }
}