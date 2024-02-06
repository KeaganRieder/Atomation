# Atomation

## About
Atomation is a game I'm making and currently have no set relaese date. this becasue right now i'm using it
as way top pratice/learn Godot and c# along with reinofrcing what i learn during classes.

Besides this as a way of learning, the game is eventaually plan on beinga combination of Atomation and 
colony mangement, with posibbly a story and other element coming later on.

## Feture Overveiw
The current fetures are as followed:

### Object Config Through File
the ability to define things like terrain, stats, biuldings ect through a json file which is read 
upon game start/load

### World Generation
The game world is generated using multiple steps

The first step is to generate 4 noise maps and how to do so is defined in GenStepNoise.cs
which utlizes differnt forumlas and a simplex nosie generation defined in Godot's FastNoiseLite Class
to achive this. the differnt parts of this step are as followed:

* noise maps 
    * a nosie map is a 2d array that contains a range of float values between either -1 to 1
    or 0 to 1.
    * there are total of 4 noise maps which are generaetd during this step
        * Elevation/Height map (top Left)
        * Heat map (top Left)
        * Moisture map (top Left)
        * Biome map (top Left)

    Todo Add example imanges

    * Elevation Map
        * the base of the map is created through using a Simplex noise algrothim 
       
        * TODO explain other modifcations
        * Values of the map range from -1 to 1, where 1 is tallest and -1 is lowest

    * Heat map
        * First a nosie map which simultes tempeture based on postion from the equator (map Center) is created
        * that inital noise map is then applied to the elevation map, inorder to ensure higher places like mountain peaks 
        are colder thne lower ground
        * Values of the map range from -1 to 1, where 1 is warmest and -1 is coldest

    *  Moisture map  
        * TODO
        
    *  Biome map  
        * TODO
* finalization

Todo next gensteps - when complete

## Planned fetures
* interaction with terrain/world
* inventory systems
* more planned to be added later
