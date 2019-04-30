using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Reservation
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private DateTime datejour;
        private DateTime? dateannulation; //variable de type date qui accepte une valeur NULL 
        private Usager usager;
        private Oeuvre oeuvre;

        //Getter Setter
        public DateTime Datejour { get => datejour; set => datejour = value; }
        public DateTime? Dateannulation { get => dateannulation; set => dateannulation = value; }
        internal Usager Usager { get => usager; set => usager = value; }
        internal Oeuvre Oeuvre { get => oeuvre; set => oeuvre = value; }

        public Reservation()
        {
        }

        public Reservation(DateTime datejour, DateTime? dateannulation, Usager usager, Oeuvre oeuvre)
        {
            this.datejour = datejour;
            this.dateannulation = dateannulation;
            this.usager = usager;
            this.oeuvre = oeuvre;
        }
    }
}
