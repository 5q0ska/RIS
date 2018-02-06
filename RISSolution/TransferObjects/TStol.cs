using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TStol
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int? Pocet_Miest { get; set; }

        public TStol(int id, int pocetMiest)
        {
            Id = id;
            Pocet_Miest = pocetMiest;
        }
    }
}
