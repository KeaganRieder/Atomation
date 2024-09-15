# Atomation

## Table of Contents
1. [Project Overview](#Project-Overview)
3. [Feature Overview](#Features)

## Project Overview
Atomation is a passion project of mine. I'm currently developing the game in the godot engine, mainly as a hobby and to practice/learn new skills. due to the natur eof this project being for fun/a hobby I work it when i'm able to and as such currently have no planned release date.

Atomation is currently planed to be a sort of cross between an Atomation game and a colony sim, taking inspiration form games 
like Rimworld and Factorio. 

Atomation takes place on a randomly generate world, which was once a colony of humanity however 
it's now a nuclear wasteland scatter with ruins. the player find themselves arriving to the planet
onboard a colony ship which arrived many years to late due to a unknown error. so they are forced to 
adapt and begin rebuilding and reclaiming planet, and solving many mystery along the way. 

## Features 
This section covers features which have currently been added and will be 
updated regularly to include more as they come 

1. [Procedural World](#Procedural-World)

## Procedural World
The game world in which the game has the player play in, is procedurally generated, this section will cover 
the aspects of teh world as well as the techniques/methods used to create it.

### Map Structure
The game world is divided into two standard units [Tiles](#Tiles) and [Chunks](#Chunks)

#### Tiles
A tile is the smallest possible unit in the game being around 32 X 32 pixels in size. these are used to determine
the size of entities, which are measure in units of tile x tile.

todo add picture 

#### Chunks 

Update some of this

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

### Map Generators and Generation
Atomation's map is procedurally generated using a variety of techniques, such as perlin noise

## Inventory System
Todo

## Planned features
there a many planned features which section will cover:
