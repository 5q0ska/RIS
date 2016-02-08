using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataHolder
{
    /// <summary>
    /// Prenosová entita surovina
    /// </summary>
    [DataContract]
    public class Surovina:TransferEntity
    {
        int id;
        String nazov;
        bool alergen;
        String jednotka;

        /// <summary>
        /// Vytvorí novú prenosovú entitu surovinu
        /// </summary>
        /// <param name="id">id suroviny</param>
        /// <param name="nazov">názov suroviny vo vybranom jazyku</param>
        /// <param name="alergen">je surovina alergen</param>
        /// <param name="jednotka">merná jednotka suroviny</param>
        public Surovina(int id, string nazov, bool alergen, string jednotka)
        {
            this.id = id;
            this.nazov = nazov;
            this.alergen = alergen;
            this.jednotka = jednotka;
        }


        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Nazov
        {
            get { return nazov; }
            set { nazov = value; }
        }

        [DataMember]
        public bool Alergen
        {
            get { return alergen; }
            set { alergen = value; }
        }

        [DataMember]
        public string Jednotka
        {
            get { return jednotka; }
            set { jednotka = value; }
        }
    }
}
