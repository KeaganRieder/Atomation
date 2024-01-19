
/// <summary>
/// defines  a stat modifer which allows for a stat to have persitant 
/// value change/modifcation
/// </summary>
public partial class StatModifer : StatBase
{
    //todo
    private StatModType modType;

    public StatModifer(){}
    public StatModifer(string name, string description, float baseVal){
        this.name = name;
        this.description = description;
        this.baseValue = baseVal;

    }
}