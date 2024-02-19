namespace Atomation.Thing
{
    /// <summary>
    /// defines  a stat modifer which allows for a stat to have persitant 
    /// value change/modifcation
    /// </summary>
    public  class StatModifier : StatBase
    {
        //todo
        private StatModType modType;

        public StatModifier() { }
        public StatModifier(string name, string description, float baseVal)
        {
            this.name = name;
            this.description = description;
            this.baseValue = baseVal;

        }
    }
}