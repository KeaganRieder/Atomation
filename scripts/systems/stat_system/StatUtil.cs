namespace Atomation.StatSystem;

/// <summary>
/// collection of utilities used by atomation stat system
/// </summary>
public static class StatUtil
{
    public static int CompareModifierOrder(StatModifierBase a, StatModifierBase b){

        if (a.Order < b.Order)
        {
            return -1;
        }
        if (a.Order > b.Order)
        {
            return 1;
        }

        return 0;
    }

    
}