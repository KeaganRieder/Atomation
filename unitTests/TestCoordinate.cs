namespace Atomation.UnitTests;

using Atomation.Map;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class TestCoordinate
{
    [TestCase]
    public void TestCoordinateWorld()
    {
        Coordinate cord = new Coordinate(new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(16, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(16, 0));

        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(513, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(513, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(528, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(528, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(528, 528));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(528, 528));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 1));
    }
    [TestCase]
    public void TestCoordinateNegWorld()
    {
        Coordinate cord = new Coordinate(new Vector2(-16, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-16, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(31, 0));
    }

    [TestCase]
    public void TestCoordinateXY()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(1, 0, new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(16, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(0, 0, new Vector2(32, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(0, 0, new Vector2(-32, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(1, 0, new Vector2(32, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(528, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));
    }

    [TestCase]
    public void TestCoordinateNegXY()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(-1, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(1, 0, new Vector2(-1, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-496, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(31, 0, new Vector2(-1, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-16, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(31, 0));
    }

    [TestCase]
    public void TestChunkWorld()
    {
        ChunkCoordinate cord = new ChunkCoordinate(new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(496, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new ChunkCoordinate(new Vector2(512, 512));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 1));

        cord = new ChunkCoordinate(new Vector2(1024, 512));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(1024, 512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(2, 1));
    }

    [TestCase]
    public void TestChunkNegWorld()
    {
        ChunkCoordinate cord = new ChunkCoordinate(new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(-512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(-1, 0));

        cord = new ChunkCoordinate(new Vector2(-528, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-1024, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(-2, 0));

        cord = new ChunkCoordinate(new Vector2(-512, -512));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(-1, -1));
    }

    [TestCase]
    public void TestChunkXY()
    {
        ChunkCoordinate cord = new ChunkCoordinate(0, 0);
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(1, 0);
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new ChunkCoordinate(1, 1);
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 1));

        cord = new ChunkCoordinate(2, 1);
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(1024, 512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(2, 1));
    }

    [TestCase]
    public void TestChunkBounds()
    {
        Coordinate cord = new Coordinate(new Vector2(512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(496, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(496, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(31, 0));

        cord = new ChunkCoordinate(new Vector2(496, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(0, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(0, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(-512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        cord = new ChunkCoordinate(new Vector2(-512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(-1, 0));
    }

    [TestCase]
    public void TestCordConversion()
    {
        Coordinate cord = new Coordinate(new Vector2(512, 0));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        ChunkCoordinate chunkCord = cord.ToChunkCords();
        AssertThat(chunkCord.GetWorldPosition()).IsEqual(new Vector2(512, 0));
        AssertThat(chunkCord.GetXYPosition()).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(1024, 512));
        AssertThat(cord.GetWorldPosition()).IsEqual(new Vector2(1024, 512));
        AssertThat(cord.GetXYPosition()).IsEqual(new Vector2I(0, 0));

        chunkCord = cord.ToChunkCords();
        AssertThat(chunkCord.GetWorldPosition()).IsEqual(new Vector2(1024, 512));
        AssertThat(chunkCord.GetXYPosition()).IsEqual(new Vector2I(2, 1));
    }

}
