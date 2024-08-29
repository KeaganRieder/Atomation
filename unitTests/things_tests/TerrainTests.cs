namespace Atomation.UnitTests;

using Atomation.Things;
using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using Atomation.GameMap;

[TestSuite]
public class TerrainTests
{
    private Terrain testTerrain;

    [TestCase]
    public void TestTerrainCreation()
    {
        Vector2 testCord = new Vector2(0, 0);
        testTerrain = new Terrain(testCord);

        AssertThat(testTerrain.Position).IsEqual(testCord);

        AutoFree(testTerrain.CollisionBox);
        AutoFree(testTerrain.Graphic);

        testCord = new Vector2(1, 1);
        testTerrain = new Terrain(testCord);

        AssertThat(testTerrain.Position).IsEqual(testCord * Map.CELL_SIZE);

        AutoFree(testTerrain.CollisionBox);
        AutoFree(testTerrain.Graphic);
    }

    [TestCase]
    public void TestTerrainConfiguration()
    {
        Vector2 testCord = new Vector2(0, 0);
        testTerrain = new Terrain(testCord);

        testTerrain.Configure(TerrainDef.Undefined);
        AssertThat(testTerrain.Position).IsEqual(testCord);
        AssertThat(testTerrain.Name).IsEqual("Undefined Terrain");
        AssertThat(testTerrain.Description).IsEqual(" ");

        AutoFree(testTerrain.CollisionBox);
        AutoFree(testTerrain.Graphic);
    }
}