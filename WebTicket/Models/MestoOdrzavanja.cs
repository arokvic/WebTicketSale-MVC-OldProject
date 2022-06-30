using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    public class MestoOdrzavanja
    {
        public string Mesto { get; set; }
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public int PostanskiBroj { get; set;  }

        public MestoOdrzavanja()
        {

        }

        public MestoOdrzavanja(string m , string u , int b, int p)
        {
            Mesto = m;
            Ulica = u;
            Broj = b;
            PostanskiBroj = p;
        }

        


    }
}