
/*
	ThingDef is the file epresntation of thing, 
    this conatins value which are consttant and 
    won't change throughout the game
*/
public class ThingDef : IThingDef{
    public string DefName{ get; set; }
    public string Description{ get; set;	}
}

public interface IThingDef{
	public string DefName{ get; set; }
	public string Description{get; set;	}
}