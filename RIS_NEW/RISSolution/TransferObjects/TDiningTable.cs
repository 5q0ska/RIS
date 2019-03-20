using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TDiningTable
    {
        [DataMember]
        public int? TableId { get; set; } 

        public TDiningTable(int id)
        {
            this.TableId = id;
        }
    }
}
