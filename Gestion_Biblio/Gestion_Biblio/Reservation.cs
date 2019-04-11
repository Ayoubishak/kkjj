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
        private DateTime? dateretour; //variable de type date qui accepte une valeur NULL 
        private Usager usager;
        private Oeuvre oeuvre;

        //Getter Setter
        public DateTime Datejour { get => datejour; set => datejour = value; }
        public DateTime? Dateretour { get => dateretour; set => dateretour = value; }
        internal Usager Usager { get => usager; set => usager = value; }
        internal Oeuvre Oeuvre { get => oeuvre; set => oeuvre = value; }

        public Reservation()
        {
        }

        public Reservation(DateTime datejour, DateTime? dateretour, Usager usager, Oeuvre oeuvre)
        {
            this.datejour = datejour;
            this.dateretour = dateretour;
            this.usager = usager;
            this.oeuvre = oeuvre;
        }
    }
}
