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

## Features

1. [Game World](#game-world)
2. [Entities from Files](#Entities-from-Files)
3. [Save and Load Game](#Save-and-Load-Game)

## Game World
The game world is the space in which the player play in. The following sections describes the game's:

[Map Structure](#Map-Structure)
[Map Generation](#Map-Generation)

### Map Structure
Map structure describes how the games structure with it being divided into standard units:
[Tiles](#Tile) and [Chunks](#Chunk)

#### Tile
A tile is the smallest possible unit in the game being around 16 X 16 pixels in size. these are used to determine
the size of entities, which are measure in units of tile x tile.

#### Chunks
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

### Map Generation
Map Generation is the procedure by which the games landscape is generated. In short: a number of settings  which are editable at the start of a new world allows the player to define what the world.

1. [Generation Steps](#Generation_Steps)
2. [Generation Settings](#Generation_Settings)


#### Generation Steps
The generation of the games landscape is divided into 

todo
#### Generation Settings
todo



## Entities from Files
[Things, entities or objects](scripts/thing) have values which can be configured inside of file which are call def files and can be 
found [here](data/core/defs). These files contain values which are given to an object upon it's creation, and allow for an easy
way of defining new things in the game, weather it be terrain, mobs or a biome.

## Save and Load Game
this section will eventual describe the process of how saving and loading works however it's still a work 
in progress and as such theres nothing here to report on.

## Planned Features 
Here are planned features in which haven’t been currently implemented and are bound to change:

* save/load system
* inventory system, crafting system, items
* World interaction, placing object, breaking/mining object