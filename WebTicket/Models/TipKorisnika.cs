using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    public class TipKorisnika
    {
        public string ImeTipa { get; set; }

        public double Popust { get; set; }

        public int PotrebanBrBodova { get; set; }

        public TipKorisnika()
        {

        }

        public TipKorisnika(string im , double p , int pb)
        {
            ImeTipa = im;
            Popust = p;
            PotrebanBrBodova = pb;
        }



    }
}