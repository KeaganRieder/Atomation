namespace Atomation.Thing
{
    /// <summary>
    /// defines  a stat modifer which allows for a stat to have persitant 
    /// value change/modifcation
    /// </summary>
    public  class StatModifierOld : StatBase
    {
        //todo
        private StatModType modType;

        public StatModifierOld() { }
        public StatModifierOld(string name, string description, float baseVal)
        {
            this.name = name;
            this.description = description;
            this.baseValue = baseVal;

        }
    }
}