using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    [Serializable]
    public class Korisnik
    {
        public string Username { get; set; }

        public string Lozinka { get; set; }


        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Datum { get; set; }

        public Uloga Uloga { get; set; }

        public List<Karta> Karte { get; set; } //kupac

        public List<Manifestacija> Manifestacije { get; set; } //korisnik

        public int Bodovi { get; set; }

        public TipKorisnika Tip { get; set; }
        public bool IsDeleted { get; set; }

        public Korisnik()
        {
            Manifestacije = new List<Manifestacija>();
            Karte = new List<Karta>();
            IsDeleted = false;
        }

        public Korisnik(string u , string ps, string I , string p , string d )
        {
            Username = u;
            Lozinka = ps;
            Ime = I;
            Prezime = p;
            Datum = d;
            Manifestacije = new List<Manifestacija>();
            Karte = new List<Karta>();
            Bodovi = 0;
            IsDeleted = false;
        }

      




    }
}