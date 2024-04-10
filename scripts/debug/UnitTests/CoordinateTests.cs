namespace Atomation.TestCases;

using Atomation.Asserts;
using System;
using Atomation.Map;
using Godot;

/// <summary>
/// defines Coordinate test
/// </summary>
public class CoordinateTest : UnitTest
{
    private Coordinate coord;

    public CoordinateTest() : base("CoordinateTest")
    {
        RunTest();
    }

    public override void RunTest()
    {
        base.RunTest();

        TryCoordPosition(new Vector2(0, 0), new Vector2(0, 0), new Vector2I(0, 0), new Vector2(0, 0), new Vector2(0, 0));
        TryCoordPosition(0, 0, new Vector2(0, 0), new Vector2(0, 0), new Vector2I(0, 0), new Vector2(0, 0), new Vector2(0, 0));

        TryCoordPosition(new Vector2(16, 0), new Vector2(16, 0), new Vector2I(1, 0), new Vector2(0, 0), new Vector2(0, 0));
        TryCoordPosition(1, 0, new Vector2(0, 0), new Vector2(16, 0), new Vector2I(1, 0), new Vector2(0, 0), new Vector2(0, 0));

        TryCoordPosition(new Vector2(32, 0), new Vector2(32, 0), new Vector2I(2, 0), new Vector2(0, 0), new Vector2(0, 0));
        TryCoordPosition(2, 0, new Vector2(0, 0), new Vector2(32, 0), new Vector2I(2, 0), new Vector2(0, 0), new Vector2(0, 0));

        TryCoordPosition(new Vector2(-512, 0), new Vector2(-512, 0), new Vector2I(0, 0), new Vector2(-512, 0),new Vector2(-1, 0));
        TryCoordPosition(0, 0, new Vector2(-32, 0), new Vector2(-512, 0), new Vector2I(0, 0), new Vector2(-512, 0),new Vector2(-1, 0));

        //starts in top left of chunk and moves towards worlds center at (0,0)
        TryCoordPosition(new Vector2(-496, 0), new Vector2(-496, 0), new Vector2I(1, 0), new Vector2(-512, 0),new Vector2(-1, 0));
        TryCoordPosition(1, 0, new Vector2(-32, 0), new Vector2(-496, 0), new Vector2I(1, 0), new Vector2(-512, 0),new Vector2(-1, 0));

        base.TestResults();
    }

    private void TryCoordPosition(Vector2 worldPosition, Vector2 expectedPosition, Vector2I ExpectedXY, Vector2 ExpectedChunk,
    Vector2 ExpectedChunkWorld)
    {
        coord = new Coordinate(worldPosition);
        TryWorldPosition(expectedPosition);
        TryXYPosition(ExpectedXY);
        TryChunkWorldPosition(ExpectedChunk);
        TryChunkPosition(ExpectedChunkWorld);
    }

    private void TryCoordPosition(int x, int y, Vector2 ChunkPos, Vector2 expectedPosition, Vector2I ExpectedXY, Vector2 ExpectedChunk,
    Vector2 ExpectedChunkWorld)
    {

        coord = new Coordinate(x, y, ChunkPos);
        TryWorldPosition(expectedPosition);
        TryXYPosition(ExpectedXY);
        TryChunkWorldPosition(ExpectedChunk);
        TryChunkPosition(ExpectedChunkWorld);
    }

    private void TryWorldPosition(Vector2 expected)
    {
        totalTests++;
        VectorAssertions assert = new VectorAssertions(coord.WorldPosition);
        try
        {
            assert.IsEqual(expected);
        }
        catch (Exception errMsg)
        {
            failedTests++;
            GD.PrintErr($"World Pos: {errMsg.Message}");
            return;
        }
        PassedTests++;
    }

    private void TryXYPosition(Vector2I expected)
    {
        totalTests++;
        VectorAssertions assert = new VectorAssertions(coord.XYPosition);
        try
        {
            assert.IsEqual(expected);
        }
        catch (Exception errMsg)
        {
            failedTests++;
            GD.PrintErr($"XY Pos: {errMsg.Message}");
            return;
        }
        PassedTests++;
    }

    private void TryChunkWorldPosition(Vector2 expected)
    {
        totalTests++;
        VectorAssertions assert = new VectorAssertions(coord.ChunkWorldPos);
        try
        {
            assert.IsEqual(expected);
        }
        catch (Exception errMsg)
        {
            failedTests++;
            GD.PrintErr($"ChunkPos :{errMsg.Message}");
            return;
        }

        assert = new VectorAssertions(coord.ChunkWorldPos);
        try
        {
            assert.IsEqual(expected);
        }
        catch (Exception errMsg)
        {
            failedTests++;
            GD.PrintErr($"ChunkPos :{errMsg.Message}");
            return;
        }
        PassedTests++;
    }
    private void TryChunkPosition(Vector2 expected)
    {
        totalTests++;
        VectorAssertions assert = new VectorAssertions(coord.ChunkPosition);
        try
        {
            assert.IsEqual(expected);
        }
        catch (Exception errMsg)
        {
            failedTests++;
            GD.PrintErr($"ChunkPos :{errMsg.Message}");
            return;
        }
        PassedTests++;
    }
}
