// private void FormatDefFiles()
// 	{
// 		Dictionary<string, TerrainDef> NaturalDefs = new Dictionary<string, TerrainDef>(){
// 			{"Grass", new TerrainDef(){
// 				Name = "Grass",
// 				Description = "It's grassy",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/grass",
// 					Color = Colors.SeaGreen,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,1,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.8f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Forest Grass", new TerrainDef(){
// 				Name = "Forest Grass",
// 				Description = "It's darker grass found in a forest",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/forest_grass",
// 					Color = Colors.DarkGreen,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,1,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.8f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Rich Soil", new TerrainDef(){
// 				Name = "Rich Soil",
// 				Description = "It's soil that crops can grow well one",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/rich_soil",
// 					Color = Colors.RosyBrown,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,1.5f,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Soil", new TerrainDef(){
// 				Name = "Soil",
// 				Description = "It's soil ",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/soil",
// 					Color = Colors.Brown,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,1.5f,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Sand", new TerrainDef(){
// 				Name = "sand",
// 				Description = "it's sand",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/sand",
// 					Color = Colors.Yellow,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0f,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Gravel", new TerrainDef(){
// 				Name = "Gravel",
// 				Description = "it's sand",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/gravel",
// 					Color = Colors.Gray,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0f,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.2f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Ice", new TerrainDef(){
// 				Name = "Ice",
// 				Description = "it's slippery",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/ice",
// 					Color = Colors.White,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0f,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.4f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 		};
	
// 	Dictionary<string, TerrainDef> StoneDefs = new Dictionary<string, TerrainDef>(){
// 			{"Slate", new TerrainDef(){
// 				Name = "Slate",
// 				Description = "It's a nice black rock",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/slate",
// 					Color = Colors.DarkGray,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.7f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Marble", new TerrainDef(){
// 				Name = "Marble",
// 				Description = "It's white rock",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/Marble",
// 					Color = Colors.LightGray,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,1,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.7f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 		};
	
// 	Dictionary<string, TerrainDef> WaterDefs = new Dictionary<string, TerrainDef>(){
// 			{"Marsh", new TerrainDef(){
// 				Name = "Marsh",
// 				Description = "It's a marshy",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/marsh",
// 					Color = Colors.DarkOliveGreen,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,0,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Shallow Ocean", new TerrainDef(){
// 				Name = "Shallow Ocean",
// 				Description = "It's shallow ocean",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/shallow_ocean",
// 					Color = Colors.SeaGreen,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,0,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Shallow Water", new TerrainDef(){
// 				Name = "Shallow Water",
// 				Description = "It's shallow water",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/shallow_water",
// 					Color = Colors.Cyan,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,0,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Deep Ocean Water", new TerrainDef(){
// 				Name = "Shallow Ocean Water",
// 				Description = "It's Deep ocean water",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/shallow_ocean",
// 					Color = Colors.DarkSeaGreen,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,0,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 			{"Deep Water", new TerrainDef(){
// 				Name = "Shallow Water",
// 				Description = "It's Deep ocean water",
// 				GraphicData = new GraphicData(){
// 					TexturePath = "terrain/natural/shallow_water",
// 					Color = Colors.DarkBlue,
// 				},
// 				StatDefs = new Stat[]{
// 					new Stat("Fertility","The fertility of the object",0.1f,0,2),
// 					new Stat("Beauty","The Beauty of the object",0.0f,0,1),
// 				},
// 				StatModifersDefs = new StatModifier[]{
// 					new StatModifier("WalkSpeed","The speed something can walk on this",0.5f,ModifierType.MultiplicativePercentage),
// 				}
// 			}},
// 		};
	
// 	DefFile<TerrainDef> temp = new DefFile<TerrainDef>(NaturalDefs, FilePath.TERRAIN_FOLDER, "terrain_natural" );
// 	DefFile<TerrainDef> temp2 = new DefFile<TerrainDef>(StoneDefs, FilePath.TERRAIN_FOLDER, "terrain_stone" );

// 	DefFile<TerrainDef> temp3 = new DefFile<TerrainDef>(WaterDefs, FilePath.TERRAIN_FOLDER, "terrain_water" );

// 	/*FilePath.TERRAIN_FOLDER
// 		public DefFile(Dictionary<string, defType> contents, string path, string fileName ){
//             defs = contents;
//             FileManger.WriteJsonFile(path, fileName, this);
//         }*/
// 	}