namespace Atomation.Resources;

using GameMap;
using Things;
using StatSystem;
using Godot;
using System.Collections.Generic;

/// <summary>
/// collection of functions which can be used to write a def file contain
/// defined terrain defs
/// </summary>
public static class TerrainDefs
{
    public static void FormatTerrainDefs()
    {
        FormatNaturalTerrainDefs();
        FormatRockyTerrainDefs();
        FormatWaterTerrainDefs();
    }
    public static void FormatNaturalTerrainDefs()
    {
        Dictionary<string, TerrainDef> terrainNatural = new Dictionary<string, TerrainDef>{
            {"NaturalTerrainBase", new TerrainDef(){DefName = "NaturalTerrainBase", Description = "Todo"}},
            {"Forest Grass",new TerrainDef("Forest Grass", "it's grass within a forest",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",1,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.7f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/forest_grass",
                    Variants = 1,
                    Color = Colors.DarkGreen,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Grass",new TerrainDef("Grass", "it's grass",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",1,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",1f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/grass",
                    Variants = 1,
                    Color = Colors.Green,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Dry Grass",new TerrainDef("Dry Grass", "it's Dry Grass",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0.5f,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/Ice",
                    Variants = 1,
                    Color = new Color(0.9f, 0.9f, 0.2f),
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Light, SupportReq = SupportType.None}
            },
            {"Soil",new TerrainDef("Soil", "it's soil",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",1,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.7f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/soil",
                    Variants = 1,
                    Color = Colors.Brown,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Rich Soil",new TerrainDef("Rich Soil", "it's rich soil, corps grow well here",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",2,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.7f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/soil",
                    Variants = 1,
                    Color = Colors.RosyBrown,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Sand",new TerrainDef("Sand", "it's rough, corse and gets everywhere",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/sand",
                    Variants = 1,
                    Color = Colors.Yellow,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain,  SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Ice",new TerrainDef("Ice", "it's slippery,and cold",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/Ice",
                    Variants = 1,
                    Color = Colors.White,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain,  SupportProvided = SupportType.Light, SupportReq = SupportType.None}
            },
            {"Taiga Soil",new TerrainDef("Taiga Soil", "it's lacks nutrients",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0.5f,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/Ice",
                    Variants = 1,
                    Color = new Color(0.3f, 0.4f, 0.3f),
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain,  SupportProvided = SupportType.Light, SupportReq = SupportType.None}
            }
        };
        DefFile<TerrainDef> terrainNaturalDefs = new DefFile<TerrainDef>(terrainNatural, FilePaths.TERRAIN_FOLDER, "terrain_natural");
    }

    public static void FormatRockyTerrainDefs()
    {
        Dictionary<string, TerrainDef> terrainRocky = new Dictionary<string, TerrainDef>{
            {"Gravel",new TerrainDef("Gravel", "it's made of smaller rocks",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",.5f,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/gravel",
                    Variants = 1,
                    Color = Colors.Gray,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Marble",new TerrainDef("Marble", "it's white rock which looks nice",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/rock_floor",
                    Variants = 1,
                    Color = Colors.WhiteSmoke,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Slate",new TerrainDef("Slate", "it's a black rock which looks nice",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/rock_floor",
                    Variants = 1,
                    Color = Colors.DarkGray,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            };
        DefFile<TerrainDef> terrainStoneDefs = new DefFile<TerrainDef>(terrainRocky, FilePaths.TERRAIN_FOLDER, "terrain_stone");
    }

    public static void FormatWaterTerrainDefs()
    {
        Dictionary<string, TerrainDef> terrainWater = new Dictionary<string, TerrainDef>{
            {"Marsh",new TerrainDef("Marsh", "it's Marshy",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0f,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.4f)}
                }),
                new GraphicData(){
                TexturePath = "terrain/natural/marsh",
                Variants = 1,
                Color = Colors.SeaGreen,
                GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Shallow Ocean",new TerrainDef("Shallow Ocean", "it's Ocean which is shallow",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                TexturePath = "terrain/natural/ocean",
                Variants = 1,
                Color = Colors.Blue,
                GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Shallow Water",new TerrainDef("Shallow Water", "it's shallow water",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                TexturePath = "terrain/natural/water",
                Variants = 1,
                Color = Colors.LightBlue,
                GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Deep Ocean",new TerrainDef("Deep Ocean", "it's Ocean which is Deep",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed", 0f)}
                }),
                new GraphicData(){
                TexturePath = "terrain/natural/ocean",
                Variants = 1,
                Color = Colors.DarkBlue,
                GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.None, SupportReq = SupportType.None}
            },
            {"Deep Water",new TerrainDef("Deep Water", "it's deep water, which you have to swim through",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.2f)}
                }),
                new GraphicData(){
                    TexturePath = "terrain/natural/water",
                    Variants = 1,
                    Color = Colors.RoyalBlue,
                    GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {GridLayer = GameLayers.Terrain, SupportProvided = SupportType.None, SupportReq = SupportType.None}
            },
            };
        DefFile<TerrainDef> terrainWaterDefs = new DefFile<TerrainDef>(terrainWater, FilePaths.TERRAIN_FOLDER, "terrain_water");
    }
}