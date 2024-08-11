namespace  Atomation.Resources;

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
    public static void FormatTerrainDefs(){
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
                    texturePath = "terrain/natural/forest_grass",
                    variants = 1,
                    color = Colors.DarkGreen,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Grass",new TerrainDef("Grass", "it's grass",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",1,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",1f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/grass",
                    variants = 1,
                    color = Colors.Green,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Soil",new TerrainDef("Soil", "it's soil",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",1,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.7f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/soil",
                    variants = 1,
                    color = Colors.Brown,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Rich Soil",new TerrainDef("Rich Soil", "it's rich soil, corps grow well here",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",2,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.7f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/soil",
                    variants = 1,
                    color = Colors.RosyBrown,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Sand",new TerrainDef("Sand", "it's rough, corse and gets everywhere",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/sand",
                    variants = 1,
                    color = Colors.Yellow,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Ice",new TerrainDef("Ice", "it's slippery,and cold",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/Ice",
                    variants = 1,
                    color = Colors.White,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Light, SupportReq = SupportType.None}
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
                    texturePath = "terrain/natural/gravel",
                    variants = 1,
                    color = Colors.Gray,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Marble",new TerrainDef("Marble", "it's white rock which looks nice",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/rock_floor",
                    variants = 1,
                    color = Colors.WhiteSmoke,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
            },
            {"Slate",new TerrainDef("Slate", "it's a black rock which looks nice",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/rock_floor",
                    variants = 1,
                    color = Colors.DarkGray,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.Heavy, SupportReq = SupportType.None}
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
                texturePath = "terrain/natural/marsh",
                variants = 1,
                color = Colors.SeaGreen,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Shallow Ocean",new TerrainDef("Shallow Ocean", "it's Ocean which is shallow",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                texturePath = "terrain/natural/ocean",
                variants = 1,
                color = Colors.Blue,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Shallow Water",new TerrainDef("Shallow Water", "it's shallow water",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.5f)}
                }),
                new GraphicData(){
                texturePath = "terrain/natural/water",
                variants = 1,
                color = Colors.LightBlue,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {SupportProvided = SupportType.Medium, SupportReq = SupportType.None}
            },
            {"Deep Ocean",new TerrainDef("Deep Ocean", "it's Ocean which is Deep",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed", 0f)}
                }),
                new GraphicData(){
                texturePath = "terrain/natural/ocean",
                variants = 1,
                color = Colors.DarkBlue,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
             })
                {SupportProvided = SupportType.None, SupportReq = SupportType.None}
            },
            {"Deep Water",new TerrainDef("Deep Water", "it's deep water, which you have to swim through",
                new StatSheet(new Dictionary<string, StatBase>{
                    {"fertility", new StatBase("fertility","The tile's fertility",0,0,2)}
                },
                new Dictionary<string, StatModifierBase>{
                    {"moveSpeed", new FlatModifier("MoveSpeed Modifier","moveSpeed",.2f)}
                }),
                new GraphicData(){
                    texturePath = "terrain/natural/water",
                    variants = 1,
                    color = Colors.RoyalBlue,
                    graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                })
                {SupportProvided = SupportType.None, SupportReq = SupportType.None}
            },
            };
        DefFile<TerrainDef> terrainWaterDefs = new DefFile<TerrainDef>(terrainWater, FilePaths.TERRAIN_FOLDER, "terrain_water");
    }
}