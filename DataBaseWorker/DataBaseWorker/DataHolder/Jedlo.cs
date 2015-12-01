using System;
using System.Runtime.Serialization;


namespace DataHolder
{
    [DataContract]
    public class Jedlo
    {
        int id;
        String nazov;
        int id_typu;
        String nazov_typu;
        int mnozstvo_kalorii;
        int dlzka_pripravy;

        public Jedlo(int id, string nazov, int idTypu, string nazovTypu)
        {
            this.id = id;
            this.nazov = nazov;
            id_typu = idTypu;
            nazov_typu = nazovTypu;
        }

        public Jedlo(int id)
        {
            this.id = id;
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
        public int IdTypu
        {
            get { return id_typu; }
            set { id_typu = value; }
        }

        [DataMember]
        public string NazovTypu
        {
            get { return nazov_typu; }
            set { nazov_typu = value; }
        }

        [DataMember]
        public int MnozstvoKalorii
        {
            get { return mnozstvo_kalorii; }
            set { mnozstvo_kalorii = value; }
        }

        [DataMember]
        public int DlzkaPripravy
        {
            get { return dlzka_pripravy; }
            set { dlzka_pripravy = value; }
        }
    }
}
