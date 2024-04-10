namespace Atomation.Asserts;

using System;

public static class AssertFailures
{

    // public static string IsTrue =>; todo
    // public static string IsFalse =>; todo

    public static string IsEqual(object current, object expected) => 
    $"Expecting to equal: {expected} but is {current}";

    public static string IsNotEqual(object current, object expected) => 
    $"Expecting to not equal: {expected} but is {current}";

    public static string IsNull(object current) => 
    $"Expecting be <Null> but is {current}";

    public static string IsNotNull(object current) => 
    $"Expecting be not <Null> but is {current}";

    public static string IsEmpty()=> 
    $"Expecting empty but is not";

    public static string IsNotEmpty()=> 
    $"Expecting not empty but is empty";

    //add more messages when needed
}