using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TransferObjects
{
    [DataContract]
    public class TJedlo : TransferEntity
    {
        private ICollection<TSurovina> zoznam_surovin;
        private IDictionary<String, String> _translations;

        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int TypeId { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int? AmountOfCalories { get; set; }

        [DataMember]
        public int? Length { get; set; }
        
        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public ICollection<TSurovina> RawMaterial
        {
            get { return zoznam_surovin; }
            set { zoznam_surovin = value; }
        }

        [DataMember]
        public IDictionary<string, string> Translations
        {
            get { return _translations; }
            set { _translations = value; }
        }

        public TJedlo(int id, string name, int typeId, string typeName, int? length, int? amountOfCalories/*, string image*/)
        {
            this.Id = id;
            this.Name = name;
            TypeId = typeId;
            TypeName = typeName;
            zoznam_surovin = new List<TSurovina>();
            this.AmountOfCalories = amountOfCalories;
            this.Length = length;
            //this.Image = image;
        }



        public TJedlo(int id, string name, int typeId, string typeName, int? amountOfCalories, int? length, ICollection<TSurovina> zoznamSurovin/*, string image*/)
        {

            this.Id = id;
            this.Name = name;
            TypeId = typeId;
            TypeName = typeName;
            this.AmountOfCalories = amountOfCalories;
            this.Length = length;
            this.zoznam_surovin = zoznamSurovin;
            //this.Image = image;
        }

        public TJedlo(int id, string name, int typeId, string typeName, int? amountOfCalories, int? length, ICollection<TSurovina> zoznamSurovin, IDictionary<String, String> _translations/*, string image*/)
        {

            this.Id = id;
            this.Name = name;
            TypeId = typeId;
            TypeName = typeName;
            this.AmountOfCalories = amountOfCalories;
            this.Length = length;
            this.zoznam_surovin = zoznamSurovin;
            this._translations = _translations;
            //this.Image = image;
        }

        public void PridajSurovinu(TSurovina surovina)
        {
            zoznam_surovin.Add(surovina);
        }

        public void PridajPreklad(String kodJazyka, String preklad)
        {
            _translations.Add(kodJazyka,preklad);
        }

        public TJedlo(int id)
        {
            this.Id = id;
        }
    }
}
