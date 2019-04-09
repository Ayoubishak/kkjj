using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Oeuvre
    {
        private int id;
        private string titre;

        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }

        public Oeuvre()
        {
        }

        public Oeuvre(int id, string titre)
        {
            this.id = id;
            this.titre = titre;
        }

        public Oeuvre Identifier(int id)
        {
            return null;
        }
    }
}
