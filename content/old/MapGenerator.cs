// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Godot;

// /// <summary>
// /// generated data for a ch
// /// </summary>
// public struct GeneratedChunkDataOld{
//     public Node2D ChunkNode{get;}
//     public Dictionary<Vector2, Terrain> Terrain{get;}

//     public GeneratedChunkDataOld(Dictionary<Vector2, Terrain> terrain, Node2D chunkNode){
//         Terrain = terrain;
//         ChunkNode = chunkNode;
//     }
// }

// /// <summary>
// /// manges world generation, by primarly define how the genSteps are carried out.
// /// generates the map preveiw before finalizlation, which then it generates new
// /// chunks when the chunk hanlder regensters a new one is laoded
// /// </summary>
// public class MapGeneratorOld
// {
//     public const int ELEVATION_MAP_IDX = 0;
//     public const int MOISTURE_MAP_IDX = 1;
//     public const int TEMPETURE_MAP_IDX = 2;

//     //generation configs
//     private Vector2 MapSize;
//     private NoiseMap[] noiseMaps;
//     private int seed;
//     private int octaves;
//     private float zoomLevel;
//     private float frequency;
//     private float lacunarity;
//     private float persistence;
//     private Vector2 center;

   
//     public MapGeneratorOld(int width, int height){
//         MapSize = new Vector2(width,height);
//         noiseMaps = new NoiseMap[1]{new(),};
//         center = new Vector2(0,0);
//     }
    

//     public int Seed{
//         get => seed;
//         set{
//             seed = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.Seed = value;
//             }
//         } 
//     }
//     public int Octaves{
//         get => octaves;
//         set{
//             octaves = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.Octaves = value;
//             }
//         } 
//     }
//     public float ZoomLevel{
//         get => zoomLevel;
//         set{
//             zoomLevel = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.ZoomLevel = value;
//             }
//         } 
//     }
//     public float Frequency{
//         get => frequency;
//         set{
//             frequency = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.Frequency = value;
//             }
//         } 
//     }
//     public float Lacunarity{
//         get => lacunarity;
//         set{
//             lacunarity = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.Lacunarity = value;
//             }
//         } 
//     }
//     public float Persistence{
//         get => persistence;
//         set{
//             persistence = value;
//             foreach (NoiseMap noiseObject in noiseMaps)
//             {
//                 noiseObject.Persistence = value;
//             }
//         } 
//     }
    
//     //generation functions

//     /// <summary>
//     /// performs and final stemps to generation/ like spawning the player 
//     /// </summary>
//     public void FinalzieGenartion(){
//         MapSize = default;
//     }

//     /// <summary>
//     /// generates the map based on noise maps, after each update
//     /// during the intial generation preveiw
//     /// </summary>
//     public void GenerateMap(WorldMap map){
//         int tileID = 0; 
//         for (int x = 0; x < MapSize.X; x++)
//         {
//             for (int y = 0; y < MapSize.X; y++)
//             {
//                 // GD.Print("test");
//                 string ID = $"Tile{tileID}"; 
//                 float elevation = noiseMaps[ELEVATION_MAP_IDX][x,y];
//                 Color color = GetColor(elevation);//(elevation,elevation,elevation)
//                 Graphic graphic = new Graphic("",color);
                
//                 Terrain terrain = new Terrain(ID, "", null, graphic){
//                     Position = new Vector2(x*WorldMap.CELL_SIZE, y*WorldMap.CELL_SIZE),
//                 };
//                 terrain.AddChild(graphic.GetTexture());
//                 map.AddChild(terrain);
//                 tileID++;
//             }
//         }        
//     }

//     public Color GetColor(float noiseVal){
//         // 
//         // if (noiseVal <= 0.25)
//         // {
//         //     return new Color(225,0,0);
//         // }
//         // else if(noiseVal <= 0.5)
//         // {
//         //     return new Color(100,0,50);
//         // }
//         // else if(noiseVal <= 0.75)
//         // {
//         //     return new Color(0,100,0);
//         // }
//         // else if(noiseVal <= 1)
//         // {
//         //     return new Color(0,0,225);
//         // }
//         // else{
//         //     return new Color(noiseVal,noiseVal,noiseVal);
//         // }
//         return new Color(noiseVal,noiseVal,noiseVal);
//     }

//     /// <summary>
//     /// called by chunkhandler to generate a new chunk, if it finds
//     /// an area loaded that needs a new chunk
//     /// </summary>
//     public GeneratedChunkDataOld GenerateChunk(Vector2 chunkCord, Node2D map){
//         int tileID = 0; 
//         // float dist
//         Dictionary<Vector2, Terrain> generatedTerrain = new();
//         Node2D ChunkNode = new Node2D(){
//             Name = $"Chunk {chunkCord}",
//             Position = chunkCord*WorldMap.CELL_SIZE
//         };
//         map.AddChild(ChunkNode);
//         UniformNoiseMap test = 
//         new UniformNoiseMap(Chunk.CHUNK_SIZE,Chunk.CHUNK_SIZE,new Vector2(Mathf.RoundToInt(chunkCord.X), Mathf.RoundToInt(chunkCord.Y)),
//         center,MapSize.Y);

//         // NoiseCombiner heatMap = new();
//         // heatMap.AddSource(test);
//         // noiseMaps[ELEVATION_MAP_IDX].Offset =new Vector2(Mathf.RoundToInt(chunkCord.X), Mathf.RoundToInt(chunkCord.Y));
//         // heatMap.AddSource(noiseMaps[ELEVATION_MAP_IDX]);
        
//         for (int x = 0; x < Chunk.CHUNK_SIZE; x++)
//         {
//             for (int y = 0; y < Chunk.CHUNK_SIZE; y++)
//             {
//                 float xCord = x * WorldMap.CELL_SIZE;
//                 float yCord = y * WorldMap.CELL_SIZE;
//                 Vector2 cords = new Vector2(xCord,yCord);

//                 //figure out how to normalize/make seemless trasnistion between
//                 //chunks
//                 // float elevation = noiseMaps[ELEVATION_MAP_IDX]
//                 //     [Mathf.RoundToInt(chunkCord.X+x), Mathf.RoundToInt(chunkCord.Y+y)];

//                 // float elevation = heatMap[x,y];
//                 // float elevation = noiseMaps[ELEVATION_MAP_IDX][x,y];

//                 float elevation = test[x, y];

//                 Color color = GetColor(elevation);//(elevation,elevation,elevation);
//                 Graphic graphic = new Graphic("",color);
//                 string ID = $"Tile{tileID}"; 
                
//                 Terrain terrain = new Terrain(ID, "", null, graphic){
//                     Position = cords,
//                 };
               
//                 tileID++;
            
//                 ChunkNode.AddChild(terrain);
//                 generatedTerrain.Add(cords,terrain);
//             }
//         }
       
//         return new GeneratedChunkDataOld(generatedTerrain,ChunkNode);
//     }

    
// }