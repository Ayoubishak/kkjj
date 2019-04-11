using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Emprunt
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private DateTime datejour;
        private DateTime? dateretour; //variable de type date qui accepte une valeur NULL 
        private Usager usager;
        private Exemplaire exemplaire;

        //Getter Setter
        public DateTime Datejour { get => datejour; set => datejour = value; }
        public DateTime? Dateretour { get => dateretour; set => dateretour = value; }
        internal Usager Usager { get => usager; set => usager = value; }
        internal Exemplaire Exemplaire { get => exemplaire; set => exemplaire = value; }

        public Emprunt()
        {
        }

        public Emprunt(DateTime datejour, DateTime dateretour, Usager usager, Exemplaire exemplaire)
        {
            this.Datejour = datejour;
            this.Dateretour = dateretour;
            this.Usager = usager;
            this.Exemplaire = exemplaire;
        }
    }
}
