namespace Atomation.Things;

using Map;
using Resources;
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
            {"Slate",new StructureDef("Slate Wall", "A rock face made of slate",
                new StatSheet(new Dictionary<string, StatBase>{
                    {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",200)}
                },
                new Dictionary<string, StatModifierBase>{
                }),
                new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.SlateGray,
                graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
             })
                {supportReq = SupportType.Heavy}
            },
            {"Marble wall",new StructureDef("Marble wall", "A rock face made of Marble",
                new StatSheet(new Dictionary<string, StatBase>{
                    {StatKeys.MAX_HEALTH, new StatBase(StatKeys.MAX_HEALTH,"The health of the wall",100)}
                },
                new Dictionary<string, StatModifierBase>{
                }),
                new GraphicData(){
                texturePath = "terrain/natural/rock_wall",
                variants = 1,
                color = Colors.GhostWhite,
                graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
             })
                {supportReq = SupportType.Heavy}
            },
        };

        DefFile<StructureDef> NaturalStructure = new DefFile<StructureDef>(naturalStructure, FilePaths.STRUCTURE_FOLDER, "structure_natural");
    }
}
