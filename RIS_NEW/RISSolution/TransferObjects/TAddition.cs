using System;
using System.Runtime.Serialization;

namespace TransferObjects
{
    /// <summary>
    /// Prenosová entita surovina
    /// </summary>
    [DataContract]
    public class TAddition : TransferEntity
    {
        /// <summary>
        /// Vytvorí novú prenosovú entitu surovinu
        /// </summary>
        /// <param name="id">id suroviny</param>
        /// <param name="nazov">názov suroviny vo vybranom jazyku</param>
        /// <param name="alergen">je surovina alergen</param>
        /// <param name="jednotka">merná jednotka suroviny</param>
        public TAddition(int AdditionId, string Name, double Weight, double Price, string Description)
        {
            this.AdditionId = AdditionId;
            this.Name = Name;
            this.Weight = Weight;
            this.Price = Price;
            this.Description = Description;
        }

        [DataMember]
        public int AdditionId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Weight { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
