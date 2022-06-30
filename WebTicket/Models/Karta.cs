using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace WebTicket.Models
{
    [Serializable]
    public class Karta
    {
        [StringValidator (MaxLength =10 , MinLength =10)]
        public string IdKarte { get; set; }

        public string NazivManifestacije { get; set; }

        public string DatumIVreme { get; set; }

        public double Cena { get; set; }

        public string NazivKupca { get; set; }

        public StatusKarte Status { get; set; }

        public TipKarte Tip { get; set; }

        public bool IsDeleted { get; set; }


        public Karta(string id,string m , string dv , double c , string k , StatusKarte s , TipKarte tp)
        {
            /* var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
             var stringChars = new char[10];
             var random = new Random();

             for (int i = 0; i < stringChars.Length; i++)
             {
                 stringChars[i] = chars[random.Next(chars.Length)];
             }*/



            IdKarte = id;
            NazivManifestacije = m;
            DatumIVreme = dv;
            Cena = c;
            NazivKupca = k;
            Status = s;
            Tip = tp;
            IsDeleted = false;
        }

        public Karta()
        {

        }

    }
}