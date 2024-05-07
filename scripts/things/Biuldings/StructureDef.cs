namespace Atomation.Things;

using System.Collections.Generic;
using Atomation.Map;
using Atomation.Resources;
using Godot;


/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a Structure
/// </summary>
public class StructureDef : ThingDef
{
    public StructureDef() { }
    public StructureDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }

    public static StructureDef Undefined(){
        return new StructureDef("Undefine Structure", "",
             new StatSheet(new Dictionary<string, StatBase>{},
             new Dictionary<string, StatModifierBase>{
             }),
             new GraphicData(){
                TexturePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png",
                Variants = 1,
                Color = Colors.Purple,
                GraphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE)
             }); 
    }
}

