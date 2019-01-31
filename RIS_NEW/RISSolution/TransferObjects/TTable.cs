using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TTable
    {
        [DataMember]
        public int? TableId { get; set; } 

        public TTable(int id)
        {
            this.TableId = id;
        }
    }
}
