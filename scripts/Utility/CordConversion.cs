using Godot;

/// <summary>
/// utility class that handles defineing fucntion whcih are used
/// to convert/normalize cords based ona varity of factors/
/// values
/// </summary>
public static class CordConversionUtility
{
    /// <summary>
    /// normilize given x,y cords, based on WorldMap.CELL_SIZE
    /// </summary>
    public static  Vector2 CellSizeCords(int x, int y){
        float xCord = x * WorldMap.CELL_SIZE;
        float yCord = y * WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
    }
    /// <summary>
    /// normilize the given vector represnting the cords, based on WorldMap.CELL_SIZE
    /// </summary>
    public static Vector2 CellSizeCords(Vector2 cords){
      return CellSizeCords(Mathf.RoundToInt(cords.X),Mathf.RoundToInt(cords.Y));
    }
    // todo chunksize
}