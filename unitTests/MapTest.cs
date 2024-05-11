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
        WorldMap map1 = AutoFree(WorldMap.Instance);
        WorldMap map2 = AutoFree(WorldMap.Instance);

        AssertThat(map1).IsEqual(map2);
    }

    [TestCase]
    public void TestMapGenInstance()
    {
        WorldGenerator map1 = WorldGenerator.Instance;
        WorldGenerator map2 = WorldGenerator.Instance;

        AssertThat(map1).IsEqual(map2);
    }
}

