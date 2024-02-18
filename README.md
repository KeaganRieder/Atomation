# Atomation

## Table of contents 
1. [ About](#about)
2. [ Feature Overview](#game-world)

## About
Atomation is planned to be a mixture between colony manger (ex Rimworld) and factory builder games (ex Factorio). It’s being developed in c# using the Godot game engine and currently has no planned release date, sense it’s currently being used as a nice way I can learn more about the Godot game engine as well as practice techniques I learned in school utilizing a different language.

## feature overview 
This section describes the many features which are present in the game, which are:

- [World](#game-world)
- [Entities from Files](#Entities-from-Files)

### Game World 
 Game works  or the map is the space in which the game exists, this section defines the many aspect which make of the map.

#### Map structure 
Map structure describes how the world is divided into standard units: [Tiles](#Tile) and [Chunks](#Chunk)

#### Tile
A tile is a square which is the smallest possible piece of the game world being roughly 32 x 32 pixels in size and are used to determine the size of entities. With entities being measure in units of tile x tile.

#### Chunk
A chunk is unit of the map which contains a collection of 32 x 32 tiles (1024 tiles in total)
Chunks are used for:
* Map generation when a player moves (more methods of loading chunks planned for later) the map generates a new chunk 
* chunks are “switch off” or “unloaded” in order to save cpu cycles from needing to render object which have nothing important happening in or by them.

## Map generation
Describes the procedure in which the map is generated. There exists a number of settings in which can be changed to modify how the world gets generated, which in some cases can lead to unique landscapes that offer unique experiences. 

### Generation Steps 
The map and chunk are generated in steps:

#### GenStepNoise
Step one in the process of generating the map. This step handles the creation of noise map, which are a grid of float number depending on the noise map range from 0 - 1 or -1 – 1. The methods to which the maps are generated from either Simplex noise, uniform noise generation or a combination of the two maps.

![Example](/docs/images/MapExample.png)

The Map representations above are:
* Elevation Map (Top Left)
* Heat Map (Top Right)
* Moisture Map (Bottom Left)
* Biome Map (Bottom Right)

##### Elevation Map
The height map uses simplex noise generated using functions defined in Godot’s FastNoiseLite class. The map generated from this contains float values which range from -1 (lowest value representing water) to 1 (highest value representing mountains). This map is used to:
* Generate terrain in later steps
* Help to determine the heat map, by making lower terrain or water generally warmer the higher terrain like mountains.
* Help determine moisture levels in the moisture maps, water should have 100% moisture well higher elevation tends to have less 

##### Heat Map
The Heat map uses a combination of simplex noise (generated during elevation map) and uniform noise. The uniform noise map is used in creating the equator heat map, which represents the points gear based on distance from a central point. This is meant to simulate how planets generally get colder the further you get from the centre and is represented using floats that range from 0 (warmest) to 1 (coldest).

![Example](/docs/images/EquatiorHeatMap.png ("Eg. Equator Map"))

The equator map is then layer onto the elevation map by:

```C#
private float GetHeatValue(Vector2 origin, int x, int y, float[,] equatorHeat)
{
    float sampleX = x + origin.X;
    float sampleY = y + origin.Y;

    float height = MathF.Abs(elevationMap[sampleX, sampleY]);

    float heat = equatorHeat[x, y] * Mathf.Abs(heatMap[sampleX, sampleY] * 10);
    heat += Mathf.Sin(height) * height;

    return Mathf.Clamp(MathF.Abs(heat), 0, 1f);
}
```

This results in the heat map being now not only based on distance from centre but also height in terrain. Which is also why temperature is represented by 0 being the coldest and 1 being the warmest, sense this allows for higher elevations (closer to 1) to be colder compared to lower elevations (closer to 0)

The map generated from this is used:
* By the terrain generation step to pick biomes which then determine specific terrain types
 
##### Moisture Map
The moisture uses primarily simplex noise to determine moisture values. As such the value range for this map is from -1 (wettest) to 1(dryest). The process to which the moisture map is generated is determined by a couple factors.
-	Terrain elevation which is gotten from the elevation map (-1 in height or water should have highest moisture)
-	Tiles distance from a body of water (river,ocean,lake ect) current tile is, closer mean more moisture, well further means less moisture. – This is still a work in progress and not currently implemented 
-	Simplex noise map which is used to generate a semi random rainfall map

#### GenStepTerrain
Work in Progress 

### Entities from Files
Todo 

### Planned features 
Here are planned features in which haven’t been currently implemented and are bound to change:

* save/load system
* inventory system, crafting system, items
* World interaction, placing object, breaking/mining object
