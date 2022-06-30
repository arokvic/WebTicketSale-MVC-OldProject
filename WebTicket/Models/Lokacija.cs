using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    public class Lokacija
    {
        public double GeogDuzina { get; set; }
        public double GeogSirina { get; set; }
        public MestoOdrzavanja Mesto { get; set; }


        public Lokacija()
        {

        }

        public Lokacija(double gd , double gs , MestoOdrzavanja m)
        {
            GeogDuzina = gd;
            GeogSirina = gs;
            Mesto = m;
        }


        
    }
}