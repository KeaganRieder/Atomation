# Atomation

## Table of Contents
1. [Project Overview](#Project-Overview)
2. [Story Overview](#Story-Overview)
3. [Feature Overview](#Feature-Overview)
4. [Planned Features ](#Planned-Features)

## Project Overview
Atomation is being developed in the Godot game Engine using C#, and is a post apocalyptic game, set in the future on an world
colonized but eventually abandon and forgotten by humanity. It's planned to be a mixture of colony simulation/management and
factory building games, however there is currently no planned releases date.

For now Atomation is manly serving as a fun way for me to learn the Godot game engine, C# and practice utilizing concepts
I learn during school. and also as a way of showing much skills.

### Story Overview
NOTE: this is still a work in progress, and as such the stroy/plot is 
very rough.

Atomation is set on a distant planet originally colonized by humanity as a industrial colony, with the sole,
purpose of produced goods for the greater human civilization. However it eventually was forgotten, leading to
supply shortages, wars and a collapse of the plants civilization. The player eventually finds themselves crashing
onto a planet which unknowingly once a human colony, but is now a radiative industrial wasteland. the player must 
now survive, looking for survives an build a colony that balances industries with restoration efforts...

## Feature Overview

1. [Game World](#game-world)
2. [Entities from Files](#Entities-from-Files)

### Game World
This section describes the many aspect that are apart of the [game world](scripts/map "Classes which define the game world") and go into
generating it.

#### Map Structure
This section outlines how the world is structured and dived into Standard units:
[Tiles](#Tile) and [Chunks](#Chunk)

##### Tile
A tile is the smallest possible unit in the game being around 16 X 16 pixels in size. these are used to determine
the size of entities, which are measure in units of X by X tiles.

##### Chunks
Chunks are units of the map which contain a collection of 32 X 32 tiles (1024 tiles in total).
Chunks allow for the map to be divided into more manageable sections and are used for:
* chunks are "switched off" or unloaded if they are a certain distance from the player. that distance
being determined in the [ChunkHandler class](scripts/map/ChunkHandler.cs) by:
``` C#
    public const float MAX_LOAD_DIST = 64; // 64 tiles away from player current position

    // making it so load distance is based on chunks not tiles. Chunk.CHUNK_SIZE = 32 tiles, 
    // so visibleChunks = 2 chunks away from player are loaded
    visibleChunks = Mathf.RoundToInt(MAX_LOAD_DIST / Chunk.CHUNK_SIZE); 
```
This allows for chunks which don't aren't near the player to not be unloaded saving cpu cycles
from needing to render object which aren't important.

* [Map generation](#Map-Generation) utilizes chunks in order to generate new parts of the map when a player moves into 
chunks which haven't been loaded before

#### Map Generation
Describes the process in which the map is generated at and during playtime. Generation is divided into different
steps which build on each other and handle certain task that relate to creating the world:

1. [GenStepNoise](#GenStepNoise)
2. [GenStepTerrain](#GenStepTerrain)

##### GenStepNoise
[GenStepNoise](scripts/map/map_gen/gen_steps/GenStepNoise.cs) is the first step in generating the world and handles the initial creation of 
[Noise Maps](# "A Noise Map is a 2d array of floating number which are in the range of 0 - 1") which are used and modified by later steps.

![Example](https://github.com/KeaganRieder/Atomation/blob/main/docs/MapExample.png)

The Map representations above are:
* Elevation Map (Top Left)
* Heat Map (Top Right)
* Moisture Map (Bottom Left)
* Biome Map (Bottom Right)

###### Elevation Map
The Elevation map is generated using a form of fractal noise called [Simplex noise](# "Defined by Godot's FastNoiseLite Class").
The map generated using Simplex noise, contains a range of float values between -1 and 1. which are used to determine
how high or low a given point is, with -1 one being the lowest well 1 is the highest.

###### Heat Map
The Heat map is generated form combing the [Elevation Map](#Elevation-Map) and the equator heat map. The Map generated during 
this step has float values which range between -1 and 1. And is used to determine how [hot](# "Closer to -1") or [cold](# "closer to 1") a given point is. 

```C#
private float[,] GenerateEquatorHeat(Vector2 origin, int width, int height)
{
    float[,] noiseMap = new float[width, height];

    // decide the temperature based on distance from central point/equator
    for (int y = 0; y < height; y++)
    {
        float sampleY = y + origin.Y;
        //calculate noise value based on it's distance
        // well also ensuring that it's within the bounds
        float noise = Math.Abs(sampleY - equatorHeight) / worldMaxHeight;//need a figure out this

        for (int x = 0; x < width; x++)
        {
            //apply the noise value for all points at this Row
            noiseMap[x, y] = Mathf.Clamp(noise, 0, 1);
        }
    }
    return noiseMap;
}
```
The equator heat map is a form of uniform noise, which is a form of noise that result in an map of values with a predictable outcome, or uniform 
change. As such the map created by this is meant to simulate a planets equator where the further you get from the center the colder it gets. 
The following is an example of the resulting map

![Example](https://github.com/KeaganRieder/Atomation/blob/main/docs/EquatiorHeatMap.png "Eg. Equator Map")

This map is then layer onto the Elevation map in the function:
```C#
private float GetHeatValue(int x, int y, float[,] equatorHeat)
{    
    float height = elevationMap[x, y];
    float heat = equatorHeat[x, y] * heatMap[x, y] * 10;
        
    heat += Mathf.Sin(Mathf.Abs(height)) * height;
            
    return Mathf.Clamp(heat, -1, 1f);
}
```
Allowing for the heat to be based on distance form center and height of the terrain. And is why the closer to 1 (higher up) you get the colder you get,
and closer to -1 the warmer you get

###### Moisture Map
The moisture map is initially generated during this step but however is also updated later during [GenStepTerrain](#GenStepTerrain). This map differs 
from the others with the result array of floats only being in a range of 0 - 1. Which allow for a point moistness to be defined as more dry closer to 0 and more moist the closer to 1 it is.

The method to which this map is generated is by applying th Elevation map on top of another simplex noise map: 
```c#
 private float GetMoistureValue(float x, float y, float elevationVal)
{
    float height = Mathf.Abs(elevationMap[x,y]);
    float moisture = Mathf.Abs(moistureMap[x,y]);

    moisture += Mathf.Sin(Mathf.Abs(height)); //* height; 

    moisture = Mathf.Clamp(moisture, 0, 1);
    //any place with water is 100% moist
    if (elevationVal < waterLevel)
    {
        return 1;
    }

    return moisture;
}
```

This is mainly the base in which the next GenStep uses to also make a tiles moisture based on it's distance form water, as well as making
water tiles  have a 100% moisture 

##### GenStepTerrain
[GenStepTerrain](scripts/map/map_gen/gen_steps/GenStepTerrain.cs) is the second step in generating the world and currently is still a work in progress. 
as such currently doesn't have an documenting or much to really put here

### Entities from Files
[Things, entities or objects](scripts/thing) have values which can be configured inside of file which are call def files and can be 
found [here](data/core/defs). These files contain values which are given to an object upon it's creation, and allow for an easy
way of defining new things in the game, weather it be terrain, mobs or a biome.

### Planned Features 
Here are planned features in which haven’t been currently implemented and are bound to change:

* save/load system
* inventory system, crafting system, items
* World interaction, placing object, breaking/mining object