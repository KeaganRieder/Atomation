namespace Atomation.Resources;

using Things;
using GameMap;
using StatSystem;
using Godot;
using System.Collections.Generic;

/// <summary>
/// collection of functions which can be used to write a def file contain
/// defined terrain defs
/// </summary>
public static class StructureDefs
{
    public static void FormatNaturalStructureDefs()
    {
        Dictionary<string, StructureDef> naturalStructure = new Dictionary<string, StructureDef>{
            {"Slate Wall", new StructureDef{
                DefName = "Slate Wall",
                Description = "A rock face made of slate",
                GridLayer = 2,
                StatSheet = new StatSheet(new Dictionary<string, StatBase>{
                    {"health", new StatBase("health","The health of the wall",200,0,200)},
                },
                new Dictionary<string, StatModifierBase>{
                }),
                GraphicData = new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.SlateGray,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                },
                SupportReq = SupportType.Heavy,
                BuildCost = new Dictionary<string, int>{
                    {"Stone",5}
                }
            }},
            {"Marble wall", new StructureDef{
                DefName = "Marble wall",
                Description = "A rock face made of Marble",
                GridLayer = 2,
                StatSheet = new StatSheet(new Dictionary<string, StatBase>{
                    {"health", new StatBase("health","The health of the wall",100,0,100)},
                },
                new Dictionary<string, StatModifierBase>{
                }),
                GraphicData = new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.SlateGray,
                graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE)
                },
                SupportReq = SupportType.Heavy,
                BuildCost = new Dictionary<string, int>{
                    {"Stone",5}
                }
            }},
        };

        DefFile<StructureDef> NaturalStructure = new DefFile<StructureDef>(naturalStructure, FilePaths.STRUCTURE_FOLDER, "structure_natural");
    }
}
