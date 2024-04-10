namespace Atomation.TestCases;

using Godot;

/// <summary>
/// runs unit test
/// </summary>
public partial class UnitTestRunner : Node
{
	CoordinateTest coordinateTest;

	public override void _Ready()
	{
		GD.Print("--- Running Unit Tests ---");
		coordinateTest = new CoordinateTest();
		GD.Print("\n--- Unit Tests Complete ---");
	}

}
