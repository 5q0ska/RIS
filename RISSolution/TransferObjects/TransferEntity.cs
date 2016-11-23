using System;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public abstract class TransferEntity
    {
        [DataMember]
        public string LanguageCode { get; set; }
    }
}
