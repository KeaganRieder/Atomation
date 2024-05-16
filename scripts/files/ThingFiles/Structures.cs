namespace Atomation.Resources;

using Things;
using Map;
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
                defName = "Slate Wall",
                description = "A rock face made of slate",
                statSheet = new StatSheet(new Dictionary<string, StatBase>{
                    {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",200)},
                },
                new Dictionary<string, StatModifierBase>{
                }),
                graphicData = new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.SlateGray,
                graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
                },
                supportReq = SupportType.Heavy,
                buildCost = new Dictionary<string, int>{
                    {"Stone",5}
                }
            }},
            {"Marble wall", new StructureDef{
                defName = "Marble wall",
                description = "A rock face made of Marble",
                statSheet = new StatSheet(new Dictionary<string, StatBase>{
                    {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",100)},
                },
                new Dictionary<string, StatModifierBase>{
                }),
                graphicData = new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.SlateGray,
                graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
                },
                supportReq = SupportType.Heavy,
                buildCost = new Dictionary<string, int>{
                    {"Stone",5}
                }
            }},
        };

        DefFile<StructureDef> NaturalStructure = new DefFile<StructureDef>(naturalStructure, FilePaths.STRUCTURE_FOLDER, "structure_natural");
    }
}
