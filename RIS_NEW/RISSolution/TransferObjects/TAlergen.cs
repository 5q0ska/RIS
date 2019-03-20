using System;
using System.Runtime.Serialization;

namespace TransferObjects
{
    /// <summary>
    /// Prenosová entita surovina
    /// </summary>
    [DataContract]
    public class TAlergen : TransferEntity
    {
        /// <summary>
        /// Vytvorí novú prenosovú entitu surovinu
        /// </summary>
        public TAlergen(int AlergenId, string Description)
        {
            this.AlergenId = AlergenId;
            this.Description = Description;
        }

        [DataMember]
        public int AlergenId { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
