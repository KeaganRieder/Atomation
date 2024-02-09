using Godot;

namespace Atomation.Utility{

/// <summary>
/// utility class that handles defineing fucntion whcih are used
/// to convert/normalize cords based ona varity of factors/
/// values
/// </summary>
public static class CordConversion
{
    /// <summary>
    /// converts cords provided as x and y to be alighned on the cellSzie grid
    /// which is intervals based on the value WorldMap.CELL_SIZE
    /// </summary>
    public static  Vector2 ToCellSizeGrid(int x, int y){
        float xCord = x * WorldMap.CELL_SIZE;
        float yCord = y * WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
    }
    /// <summary>
    /// converts cords provided as a vector2 to be alighned on the 
    /// cellSzie grid which is intervals based on the value WorldMap.CELL_SIZE
    /// </summary>
    public static Vector2 ToCellSizeGrid(Vector2 cords){
      return cords * WorldMap.CELL_SIZE;
    }

    /// <summary>
    /// converts cellSize Cords provided as a vector2 to be alighned on the 
    /// global grid 
    /// </summary>
    public static  Vector2 GlobalFromCellSize(int x, int y){
        float xCord = x / WorldMap.CELL_SIZE;
        float yCord = y / WorldMap.CELL_SIZE;
        return new Vector2(xCord,yCord);
    }
     /// <summary>
    /// converts cellSize Cords provided as a x and y to be alighned on the 
    /// global grid 
    /// </summary>
    public static Vector2 GlobalFromCellSize(Vector2 cords){
      return cords / WorldMap.CELL_SIZE;
    }
    
    /// <summary>
    /// converts cords provided as a x, y to be alighned on the 
    /// chunk grid which is based on Chunk.CHUNK_SIZE 
    /// </summary>
    public static Vector2 ToChunkGrid(int x, int y){
      float xCord = x * Chunk.CHUNK_SIZE;
      float yCord = y * Chunk.CHUNK_SIZE;
      return new Vector2(xCord,yCord);
    }
    /// <summary>
    /// converts cords provided as a vector2 to be alighned on the 
    /// chunk grid which is based on Chunk.CHUNK_SIZE 
    /// </summary>
    public static Vector2 ToChunkGrid(Vector2 cords){
      return  cords * Chunk.CHUNK_SIZE;
    }

    /// <summary>
    /// converts Chunk cords provided as a x, y to be alighned on the 
    /// global grid
    /// </summary>
    public static Vector2 ChunktoGlobalGrid(int x, int y){
      float xCord = x / Chunk.CHUNK_SIZE;
      float yCord = y / Chunk.CHUNK_SIZE;
      return new Vector2(xCord,yCord);
    }
    /// <summary>
    /// converts Chunk cords provided as a vector2  to be alighned on the 
    /// global grid
    /// </summary>
    public static Vector2 ChunktoGlobalGrid(Vector2 cords){
      return  cords / Chunk.CHUNK_SIZE;;
    }
}

}