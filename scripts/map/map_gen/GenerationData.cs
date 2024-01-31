/// <summary>
/// class is meant to allow an easy way to transfer generated data in a generstep
/// between ofther gen steps this class is manly temproy tell another
/// better way  is thought of
/// </summary>
public class GenerationData
{    
    //noise maps
    public float [,] elevationMap;
    public float [,] moistureMap;
    public float [,] heatMap; 
}