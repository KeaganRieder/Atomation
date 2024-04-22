namespace Atomation.UnitTests;

using Atomation.Map;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class TestCoordinate
{
    [TestCase]
    public void TestWorldPosition()
    {
        Coordinate cord = new Coordinate(new Vector2(0, 0));
        AssertThat(cord.WorldPosition).IsEqual(Vector2.Zero);
        AssertThat(cord.ChunkGridPosition).IsEqual(Vector2.Zero);
        AssertThat(cord.ChunkWorldPos).IsEqual(Vector2.Zero);
        AssertThat(cord.XYPosition).IsEqual(Vector2I.Zero);

        cord = new Coordinate(new Vector2(16, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(16, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(0, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(0, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(512, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(512, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(513, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(513, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(528, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(528, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(new Vector2(560, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(560, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(3, 0));

        cord = new Coordinate(new Vector2(-512, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(-1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(new Vector2(-512,-512));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(-512, -512));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(-1, -1));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(-512, -512));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

    }

    [TestCase]
    public void TestXYPosition()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));
        AssertThat(cord.WorldPosition).IsEqual(Vector2.Zero);
        AssertThat(cord.ChunkGridPosition).IsEqual(Vector2.Zero);
        AssertThat(cord.ChunkWorldPos).IsEqual(Vector2.Zero);
        AssertThat(cord.XYPosition).IsEqual(Vector2I.Zero);

        cord = new Coordinate(1, 0, new Vector2(0, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(16, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(0, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(0, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(1, 0));

        cord = new Coordinate(0, 0, new Vector2(32, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(512, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(0, 0, new Vector2(-32, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(-1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(-512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(0, 0));

        cord = new Coordinate(1, 0, new Vector2(32, 0));
        AssertThat(cord.WorldPosition).IsEqual(new Vector2(528, 0));
        AssertThat(cord.ChunkGridPosition).IsEqual(new Vector2(1, 0));
        AssertThat(cord.ChunkWorldPos).IsEqual(new Vector2(512, 0));
        AssertThat(cord.XYPosition).IsEqual(new Vector2I(1, 0));
    }

    [TestCase]
    public void TestWorldDistance()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));
        Coordinate cord2 = new Coordinate(1, 0, new Vector2(0, 0));
        AssertFloat(cord.Distance(cord2)).IsEqual(16);

        cord = new Coordinate(0, 0, new Vector2(0, 0));
        cord2 = new Coordinate(1, 1, new Vector2(0, 0));
        AssertFloat(cord.Distance(cord2)).IsEqual(23);

        cord = new Coordinate(new Vector2(-512,-512));
        cord2 = new Coordinate(new Vector2(56,56));
        AssertFloat(cord.Distance(cord2)).IsEqual(803);

        cord = new Coordinate(new Vector2(-512,-512));
        cord2 = new Coordinate(new Vector2(512,512));
        AssertFloat(cord.Distance(cord2)).IsEqual(1448);
    }

    [TestCase]
    public void TestChunkDistance()
    {
        Coordinate cord = new Coordinate(0, 0, new Vector2(0, 0));
        Coordinate cord2 = new Coordinate(1, 0, new Vector2(0, 0));
        AssertFloat(cord.ChunkDistance(cord2)).IsEqual(1);

        cord = new Coordinate(0, 0, new Vector2(0, 0));
        cord2 = new Coordinate(1, 1, new Vector2(0, 0));
        AssertFloat(cord.ChunkDistance(cord2)).IsEqual(1);

        cord = new Coordinate(new Vector2(-512,-512));
        cord2 = new Coordinate(new Vector2(56,56));
        AssertFloat(cord.ChunkDistance(cord2)).IsEqual(50);

        cord = new Coordinate(new Vector2(-512,-512));
        cord2 = new Coordinate(new Vector2(512,512));
        AssertFloat(cord.ChunkDistance(cord2)).IsEqual(91);

        cord = new Coordinate(new Vector2(0,512));
        cord2 = new Coordinate(new Vector2(0,514));
        AssertFloat(cord.ChunkDistance(cord2)).IsEqual(0);
    }
}
