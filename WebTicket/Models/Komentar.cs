using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    public class Komentar
    {
        public string NazivKorisnika { get; set; }

        public string NazivManifestacije { get; set; }

        public string Tekst { get; set; }

        public int Ocena { get; set; }
        public bool Odobren { get; set; }

        public bool IsDeleted { get; set; }


        public Komentar()
        {

        }

        public Komentar(string k , string m , string t , int o)
        {
            NazivKorisnika = k;
            NazivManifestacije = m;
            Tekst = t;
            Ocena = o;
            Odobren = false;
            IsDeleted = false;
        }

    }


}