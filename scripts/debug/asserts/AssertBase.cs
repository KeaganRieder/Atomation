namespace Atomation.Asserts;

using System;
using Atomation.Exceptions;
using Godot;

public abstract class AssertBase<TValue> 
{
    protected TValue Current { get; private set; }

    protected string CustomFailureMessage { get; set; }
    protected string CurrentFailureMessage { get; set; } = "";

    public AssertBase(TValue current) => Current = current;
       
    public virtual void IsEqual(TValue expected){ }

    public virtual void IsNotEqual(TValue expected){}

    public void IsNull()
    {
        if (Current != null)
        {
            ThrowTestFailureReport(AssertFailures.IsNull(Current), Current, null);
        }

        return;
    }

    public void IsNotNull()
    {
        if (Current == null)
        {
            ThrowTestFailureReport(AssertFailures.IsNotNull(Current), Current, null);
        }

        return;
    }
    protected void ThrowTestFailureReport(string message, object current, object expected)
    {
        CurrentFailureMessage = CustomFailureMessage ?? message;

        throw new TestFailedException(CurrentFailureMessage);
    }


}