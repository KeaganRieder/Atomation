
using System;

public class ThingDef : IThingDef{
    public string DefName{ get; set; }
    public string Description{get; set;	}
}

public interface IThingDef{
	public string DefName{ get; set; }
	public string Description{get; set;	}
}