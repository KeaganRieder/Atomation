namespace Atomation.UnitTests;

using Atomation.Map;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class TestMap
{
    [TestCase]
    public void TestMapInstance()
    {
        WorldMap map1 = AutoFree(WorldMap.GetInstance());
        WorldMap map2 = AutoFree(WorldMap.GetInstance());

        AssertThat(map1).IsEqual(map2);
    }

    [TestCase]
    public void TestMapGenInstance()
    {
        WorldGenerator map1 = WorldGenerator.GetInstance();
        WorldGenerator map2 = WorldGenerator.GetInstance();

        AssertThat(map1).IsEqual(map2);
    }
}

