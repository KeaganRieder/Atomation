namespace Atomation.Asserts;
using Godot;

public class VectorAssertions : AssertBase<Vector2>
{
    public VectorAssertions(Vector2 current) : base(current) {}

    public override void IsEqual(Vector2 expected)
    {
        if (Current != expected)
        {
            ThrowTestFailureReport(AssertFailures.IsEqual(Current, expected), Current, expected);
        }
        return;

    }

    public override void IsNotEqual(Vector2 expected)
    {
        if (Current == expected)
        {
            ThrowTestFailureReport(AssertFailures.IsNotEqual(Current, expected), Current, expected);
        }

        return;
    }

}