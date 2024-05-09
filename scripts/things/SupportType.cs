namespace Atomation.Things;

/// <summary>
/// decide what a terrain can support or what is 
/// require to support a thing
/// </summary>
public enum SupportType
{
    Undefined = -1,
    None = 0,
    Light = 1,
    Medium = 2,
    Heavy = 3,
}