namespace Atomation.Things;

using Atomation.Resources;

/// <summary>
/// used in creating and formatting a config file design to be read, 
/// and cached at game start and then used in create an instance of 
/// a Structure
/// </summary>
public class StructureDef : CompThingDetails
{
    public StructureDef() { }
    public StructureDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    : base(name, description, statSheet, graphicData) { }
}

