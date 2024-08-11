// namespace Atomation.Things;

// using Newtonsoft.Json;



// /// <summary>
// /// base class of all simple things like stats and stat modifiers, as well 
// /// as def file used to create complex things like terrain and structures 
// /// </summary>
// public abstract class Def : IDef
// {
//     [JsonProperty(Order = 1)]
//     protected string defName { get; set; }

//     [JsonProperty(Order = 1)]
//     public string description { get; set; }

//     [JsonConstructor]
//     protected Def() { }

//     protected Def(string name, string description)
//     {
//         DefName = name;
//         this.description = description;
//     }

//     public virtual string GetKey()
//     {
//         if (DefName == "" || DefName == null)
//         {
//             DefName = "Undefined_Def";
//         }

//         return DefName;
//     }
// }