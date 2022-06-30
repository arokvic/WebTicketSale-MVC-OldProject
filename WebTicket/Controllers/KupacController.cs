using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebTicket.Models;

namespace WebTicket.Controllers
{
    public class KupacController : Controller
    {
        // GET: Kupac
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Potvrda(string TipKarte, int Broj, int CenaReg, string naz)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var m in dic.Values)
            {
                if (m.Naziv == naz)
                {
                    int c;
                    if (TipKarte == "Regular")
                    {
                        c = CenaReg;
                        if (Broj > m.BrMestaRegular)
                        {
                            ViewBag.Error = "Nema dovoljno zeljenih karata";
                            ViewBag.m = m;
                            return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");
                        }
                            
                    }
                    else if (TipKarte == "VIP")
                    {
                        c = 4 * CenaReg;
                        if (Broj > m.BrMestaVIP)
                        {
                            ViewBag.Error = "Nema dovoljno zeljenih karata";
                            ViewBag.m = m;
                            return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");
                        }
                    }
                    else if (TipKarte == "Fanpit")
                    {
                        if (Broj > m.BrMestaParter)
                        {
                            ViewBag.Error = "Nema dovoljno zeljenih karata";
                            ViewBag.m = m;
                            return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");
                        }

                        c = 2 * CenaReg;
                    }
                    else
                    {
                        ViewBag.Error = "Unesite validne vrednosti";
                        ViewBag.m = m;
                        return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");


                    }

                    if (Broj <= 0)
                    {

                        ViewBag.Error = "Unesite validne vrednosti";
                        ViewBag.m = m;
                        return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");

                    }

                    




                    ViewBag.Mess = "Da li ste sigurni da zelite da rezervisete " + Broj + " " + TipKarte + " karte po ceni od " + c  + " dinara?";
                    ViewBag.naz = naz;
                    ViewBag.broj = Broj;
                    ViewBag.tip = TipKarte;
                    //return RedirectToAction("Kupac", "Prodavac", new { TipKarte = TipKarte, Broj = Broj }); 
                    return View();

                }

            }

            return View();
        }


        [HttpPost]
        public ActionResult Rezervisi(string TipKarte, int Broj, string Naziv)
        {
            
            Korisnik k = (Korisnik)Session["kupac"];

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();
            foreach (var m in dic.Values)
            {
                if (m.Naziv == Naziv)
                {
                    double c;
                    if (TipKarte == "Regular")
                    {
                        c = m.CenaRegular;
                    }
                    else if (TipKarte == "VIP")
                    {
                        c = 4 * m.CenaRegular;
                    }
                    else 
                    {
                        c = 2 * m.CenaRegular;
                    }
                   


                    while (Broj > 0) {

                        string str_build = Guid.NewGuid().ToString("N").Substring(0,10);



                        TipKarte pet = (TipKarte)Enum.Parse(typeof(TipKarte), TipKarte);
                        Karta k1 = new Karta(str_build.ToString(),m.Naziv, m.DatumIVreme, c, k.Username, StatusKarte.Rezervisana, pet);
                            Broj--;
                        if (TipKarte == "Regular")
                        {
                            m.BrMestaRegular--;
                        }
                        else if (TipKarte == "VIP")
                        {
                            m.BrMestaVIP--;
                        }
                        else 
                        {
                            m.BrMestaParter--;
                        }
                        k.Karte.Add(k1);
                        m.RezervisaneKarte.Add(k1);
                        m.BrKarata--;
                        m.BrMesta--;
                      
                        k.Bodovi += (int)(c / 1000 * 133);


                            }
                }
            }


            HttpContext.Application["manifestacije"] = dic;
            Data.SacuvajManifestaciju(dic);

            Dictionary<string, Korisnik> kup = new Dictionary<string, Korisnik>();
            kup = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];
            kup.Remove(k.Username);
            kup.Add(k.Username, k);
            HttpContext.Application["kupci"] = kup;
            Data.SacuvajKupca(kup);
            ViewBag.Karte = k.Karte;

            ViewBag.Succ = "Uspesno ste rezervisali karte";

            return View("~/Views/Kupac/KarteKupca.cshtml");

        }

        public ActionResult PrikaziKarte() {

            Korisnik k = (Korisnik)Session["kupac"];
            ViewBag.Karte = k.Karte;




            return View("~/Views/Kupac/KarteKupca.cshtml");
        }


        public ActionResult Search(string srr)
        {
            List<Karta> ret = new List<Karta>();
            Korisnik k1 = (Korisnik)Session["kupac"];
            foreach (var k in k1.Karte)
            {
                if (k.NazivManifestacije == srr)
                {
                    ret.Add(k);

                }
            }
            ViewBag.Karte = ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");


        }


        public ActionResult SearchCena(string srr, string src)
        {
            Korisnik k1 = (Korisnik)Session["kupac"];
            if (k1 == null)
            
            if (Int32.Parse(srr)<0 || Int32.Parse(srr)<0 || Int32.Parse(srr) > Int32.Parse(src))
            {
                ViewBag.Error = "Molimo vas unesite validne vrednosti";
                ViewBag.Karte = k1.Karte;
                return View("~/Views/Kupac/KarteKupca.cshtml");


            }

            List<Karta> ret = new List<Karta>();
           
            foreach (var k in k1.Karte)
            {
                if (k.Cena >= Int32.Parse(srr) && k.Cena<=Int32.Parse(src))
                {
                    ret.Add(k);
                  

                }
            }
            ViewBag.Karte = ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");


        }


        public ActionResult SearchDatum(string srr, string src)
        {
            List<Karta> ret = new List<Karta>();
            Korisnik k1 = (Korisnik)Session["kupac"];
            foreach (var k in k1.Karte)
            {
                if (DateTime.ParseExact(k1.Datum,"dd.MM.yyyy",null) >= DateTime.ParseExact(srr, "dd.MM.yyyy", null) && DateTime.ParseExact(k1.Datum, "dd.MM.yyyy", null) <= DateTime.ParseExact(src, "dd.MM.yyyy", null))
                {
                    ret.Add(k);

                }
            }
            ViewBag.Karte = ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");


        }


        public ActionResult Sort(string srr, string src)
        {

           
            Korisnik k1 = (Korisnik)Session["kupac"];
            if (srr == "Rastuci")
            {
                if (src == "Nazivu manifestacije")
                {
                    List<Karta> ret = k1.Karte.OrderBy(o => o.NazivManifestacije).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }

                if (src == "Datumu")
                {
                    List<Karta> ret = k1.Karte.OrderBy(o => DateTime.ParseExact(o.DatumIVreme, "dd.MM.yyyy", null)).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }
                if (src == "Ceni")
                {
                    List<Karta> ret = k1.Karte.OrderBy(o => o.Cena).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }



            }


            if (srr == "Opadajuci")
            {
                if (src == "Nazivu manifestacije")
                {
                    List<Karta> ret = k1.Karte.OrderByDescending(o => o.NazivManifestacije).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }

                if (src == "Datumu")
                {
                    List<Karta> ret = k1.Karte.OrderByDescending(o => DateTime.ParseExact(o.DatumIVreme, "dd.MM.yyyy", null)).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }
                if (src == "Ceni")
                {
                    List<Karta> ret = k1.Karte.OrderByDescending(o => o.Cena).ToList();
                    ViewBag.Karte = ret;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }
                

               

            }


            ViewBag.Karte = k1.Karte;
            ViewBag.Error = "Molimo vas unesite validnu vrednost";
            return View("~/Views/Kupac/KarteKupca.cshtml");
        }



        public ActionResult Filtriraj(string flt)
        {
            List<Karta> ret = new List<Karta>();
            Korisnik k1 = (Korisnik)Session["kupac"];
            


            foreach (var k in k1.Karte)
            {
                if (flt == "prikazi FanPit karte")
                {
                    if (k.Tip == TipKarte.Fanpit)
                        ret.Add(k);
                }
                else if (flt == "prikazi Regular karte")
                {
                    if (k.Tip == TipKarte.Regular)
                        ret.Add(k);
                }
                else if (flt == "prikazi VIP karte")
                {
                    if (k.Tip == TipKarte.VIP)
                        ret.Add(k);
                }
                else if (flt == "prikazi rezervisane karte")
                {
                    if (k.Status == StatusKarte.Rezervisana)
                        ret.Add(k);
                }
                else if (flt == "prikazi otkazane karte")
                {
                    if (k.Status == StatusKarte.Odustanak)
                        ret.Add(k);
                }
                else
                {
                    ViewBag.Error = "Molimo vas unesite validan izraz";
                    ViewBag.Karte = k1.Karte;
                    return View("~/Views/Kupac/KarteKupca.cshtml");
                }
               
            }

            ViewBag.Karte = ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");



        }


        public ActionResult OtkaziKartu(string id, string nazman)
        {
            Dictionary<string, Manifestacija> dic2 = new Dictionary<string, Manifestacija>();
            dic2 = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l2 = dic2.Values.ToList<Manifestacija>();


            Dictionary<string, Korisnik> dic = new Dictionary<string, Korisnik>();
            dic = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];
            List<Korisnik> l = dic.Values.ToList<Korisnik>();

            Korisnik k1 = (Korisnik)Session["kupac"];
            foreach (var k in k1.Karte)
            {
                if (id == k.IdKarte)
                {
                    k1.Bodovi -= (int)(k.Cena / 1000 * 133 * 4);
                    if (k1.Bodovi <= 0)
                        k1.Bodovi = 0;
                   
                    foreach (var m in l2)
                    {
                        if (m.Naziv == nazman)
                        {
                            Karta k3 = m.RezervisaneKarte.Where(u => u.IdKarte == id).FirstOrDefault();
                            m.RezervisaneKarte.Remove(k3);
                            dic2.Remove(m.Naziv);
                           
                            k.Status = StatusKarte.Odustanak;
                            m.RezervisaneKarte.Add(k);
                           
                            dic2.Add(m.Naziv, m);
                            HttpContext.Application["manifestacije"] = dic2;
                            Data.SacuvajManifestaciju(dic2);

                        }
                    }

                    
                }
            }
           
            dic.Remove(k1.Username);
            dic.Add(k1.Username, k1);
            HttpContext.Application["kupci"] = dic;
            Data.SacuvajKupca(dic);

            ViewBag.Karte = k1.Karte;

            return View("~/Views/Kupac/KarteKupca.cshtml");




        }


        public ActionResult Komentarisi(string naziv)
        {
            ViewBag.naziv = naziv;

            return View();
        }
        [HttpPost]
        public ActionResult Komentarisi(string naziv, string komentar, string ocena)
        {
            if (komentar == "")
            {
                ViewBag.Error = "Molim Vas unesite komentar";
                ViewBag.naziv = naziv;
                return View("~/Views/Kupac/Komentarisi.cshtml");
            }

            if (Int32.Parse(ocena) < 1 || Int32.Parse(ocena) > 5)
            {
                ViewBag.naziv = naziv;
                ViewBag.Error = "Molim Vas unesite validnu ocenu";
                return View("~/Views/Kupac/Komentarisi.cshtml");


            }


            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            Korisnik k1 = (Korisnik)Session["kupac"];
           
            

            foreach (var m in l)
            {
                if (m.Naziv == naziv)
                {
                    Komentar k = new Komentar(k1.Username, m.Naziv, komentar, Int32.Parse(ocena));
                    m.Komentari.Add(k);
                    dic.Remove(m.Naziv);
                    dic.Add(m.Naziv, m);
                    HttpContext.Application["Manifestacije"] = dic;
                    Data.SacuvajManifestaciju(dic);
                    


                }


            }


            HttpContext.Application["manifestcije"] = dic;
            Data.SacuvajManifestaciju(dic);




            ViewBag.naziv = naziv;

            return RedirectToAction("Index", "Pocetna");
        }


    }
}
