namespace Atomation.UnitTests;

using Atomation.Map;
using Atomation.Resources;
using Atomation.Things;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class TestChunk
{
    [TestCase]
    public void TestChunkCreation()
    {
        Chunk chunk = AutoFree(new Chunk(-1, -1));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(-1, -1));

        chunk = AutoFree(new Chunk(new Vector2(-512, -512)));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(-1, -1));

        chunk = AutoFree(new Chunk(0, -1));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, -512));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, -1));

        chunk = AutoFree(new Chunk(-1, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(-1, 0));

        chunk = AutoFree(new Chunk(0, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, 0));
    }

    [TestCase]
    public void TestTerrainGrid()
    {
        Chunk chunk = AutoFree(new Chunk(0, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        var terrainGrid = chunk.TerrainGrid;
        AssertThat(terrainGrid).IsNotNull();

        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));

        TerrainDef terrainDef = TerrainDef.Undefined();
        Terrain terrain = AutoFree(new Terrain(cord));
        terrain.ReadConfigs(terrainDef);
        terrainGrid.SetObject(0, 0, terrain);

        AssertThat(terrainGrid.GetObject(0, 0)).IsNotNull();
        AssertThat(terrainGrid.GetObject(new Vector2(0, 0))).IsNotNull();
        AssertThat(terrainGrid.GetObject(new Vector2(1, 0))).IsNotNull();
        AssertThat(terrainGrid.GetObject(cord)).IsNotNull();

        cord = new Coordinate(1, 0, new Vector2(0, 0));
        AssertThat(terrainGrid.GetObject(1, 0)).IsNull();
        AssertThat(terrainGrid.GetObject(new Vector2(16, 0))).IsNull();
        AssertThat(terrainGrid.GetObject(cord)).IsNull();

        cord = new Coordinate(16, 0, new Vector2(0, 0));
        terrain = AutoFree(new Terrain(cord));
        terrain.ReadConfigs(terrainDef);

        terrainGrid.SetObject(cord, terrain);
        AssertThat(terrainGrid.GetObject(16, 0)).IsNotNull();
        AssertThat(terrainGrid.GetObject(new Vector2(256, 0))).IsNotNull();
        AssertThat(terrainGrid.GetObject(cord)).IsNotNull();
    }

    [TestCase]
    public void TestStructureGrid()
    {
        Chunk chunk = AutoFree(new Chunk(0, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        var structureGrid = chunk.StructureGrid;
        AssertThat(structureGrid).IsNotNull();

        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));

        StructureDef structureDef = StructureDef.Undefined();
        Structure structure = AutoFree(new Structure(cord));
        structure.ReadConfigs(structureDef);

        structureGrid.SetObject(0,0,structure);

        AssertThat(structureGrid.GetObject(0, 0)).IsNotNull();
        AssertThat(structureGrid.GetObject(new Vector2(0, 0))).IsNotNull();
        AssertThat(structureGrid.GetObject(new Vector2(1, 0))).IsNotNull();
        AssertThat(structureGrid.GetObject(cord)).IsNotNull();

        cord = new Coordinate(1, 0, new Vector2(0, 0));
        AssertThat(structureGrid.GetObject(1, 0)).IsNull();
        AssertThat(structureGrid.GetObject(new Vector2(16, 0))).IsNull();
        AssertThat(structureGrid.GetObject(cord)).IsNull();

        cord = new Coordinate(16, 0, new Vector2(0, 0));
        structure = AutoFree(new Structure(cord));
        structure.ReadConfigs(structureDef);

        structureGrid.SetObject(cord, structure);
        AssertThat(structureGrid.GetObject(16, 0)).IsNotNull();
        AssertThat(structureGrid.GetObject(new Vector2(256, 0))).IsNotNull();
        AssertThat(structureGrid.GetObject(cord)).IsNotNull();

    }

    [TestCase]
    public void TestChunkDistanceCalc()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        Chunk chunk = AutoFree(new Chunk(-1, -1));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(chunk.Coordinate.DistanceTo(cord)).IsEqual(724);

        chunk = AutoFree(new Chunk(0, -1));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, -512));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, -1));

        chunk = AutoFree(new Chunk(-1, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(-1, 0));

        chunk = AutoFree(new Chunk(0, 0));
        AssertThat(chunk.Coordinate.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(chunk.Coordinate.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        AssertFloat(chunk.Coordinate.DistanceTo(cord)).IsEqual(0);

        cord = new Coordinate(5, 0, new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(80, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(5, 0));


        AssertFloat(chunk.Coordinate.DistanceTo(cord)).IsEqual(80);

    }

    [TestCase]
    public void TestRenderDistance(){
        Chunk chunk = AutoFree(new Chunk(0,0));
        Coordinate cord = new Coordinate(new Vector2(0,0));

        AssertThat( MapData.GetData().GetTileRenderDistance()).IsEqual(512);
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(0);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsTrue();

        cord = new Coordinate(new Vector2(512,0));
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(23);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsTrue();

        cord = new Coordinate(new Vector2(512,512)); //1,1 
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(27);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsTrue();

        chunk = AutoFree(new Chunk(1,1));
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(0);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsTrue();

        chunk = AutoFree(new Chunk(-1,-1));
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(38);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsFalse();

        chunk = AutoFree(new Chunk(0,-1));
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(34);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsFalse();

        chunk = AutoFree(new Chunk(-1,0));
        AssertThat(chunk.Coordinate.squaredDistanceTo(cord)).IsEqual(34);
        chunk.UpdateVisibility(cord);
        AssertBool(chunk.CheckVisibility()).IsFalse();

    }
}