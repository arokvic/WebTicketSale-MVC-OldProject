using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTicket.Models
{
    [Serializable] 
    public class Manifestacija
    {
        public string Naziv { get; set; }
        public List<Karta> RezervisaneKarte { get; set; }
        public TipManifestacije Tip { get; set; }

        public int BrMestaRegular { get; set; }
        public int BrMestaParter { get; set; }
        public int BrMestaVIP { get; set; }

        public int BrMesta { get; set; }

        public string DatumIVreme { get; set; }

        public double CenaRegular { get; set; }
        public double CenaParter { get; set; }
        public double CenaVIP { get; set; }

        public string NazivKreatora { get; set; }


        public bool Aktivno { get; set; }

        public MestoOdrzavanja Mesto { get; set; }

        public string Poster { get; set; }

        public List<Komentar> Komentari { get; set; }

        public double Ocena { get; set; }

        public int BrKarata { get; set; }

        public bool IsDeleted { get; set; }

        public Manifestacija()
        {

        }

        public Manifestacija(string naz , TipManifestacije tip, int brmR ,int brmP,int brmV, string dat, double cena, bool akt , MestoOdrzavanja mo ,string post, double oc, string nazivk )
        {
            Naziv = naz;
            Tip = tip;
            BrMestaRegular = brmR;
            BrMestaParter = brmP;
            BrMestaVIP = brmV;
            BrMesta = brmR + brmP + brmV;
            DatumIVreme = dat;
            CenaRegular = cena;
            CenaParter = 2 * cena;
            CenaVIP = 4 * cena;
            Aktivno = akt;
            Mesto = mo;
            Poster = post;
            Ocena = 0;
            BrKarata = BrMesta;
            Komentari = new List<Komentar>();
            RezervisaneKarte = new List<Karta>();
            IsDeleted = false;
            NazivKreatora = nazivk;

        }



    }
}