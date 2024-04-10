namespace Atomation.TestCases;

using System;
using Atomation.Asserts;
using Godot;

public abstract class UnitTest
{
    protected string testName = "Undefined";

    protected int totalTests = 0;
    protected int failedTests = 0;
    protected int PassedTests = 0;

    protected UnitTest(string name){
        testName = name;
    }

    public virtual void RunTest() {  GD.Print($"\n--- Running {testName} test ---");   }
    public virtual void TestResults(){
        GD.Print("\nResults");
        GD.Print($"Ran: {totalTests}");
        GD.Print($"Passed: {PassedTests}");
        GD.Print($"Failed: {failedTests}");



    }
}

