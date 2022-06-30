using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Models;

namespace WebTicket.Controllers
{
    public class ProdavacController : Controller
    {
        // GET: Prodavac
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DodajManifestaciju()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajManifestaciju(string Naziv, TipManifestacije Tip, int BrojMestaRegular,int BrojMestaParter,int BrojMestaVip, int Cena, DateTime Datum,string Ulica, int Broj, string Grad, int Postanski )
        {
            Korisnik k = (Korisnik)Session["prodavac"];
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();
            foreach (var m in l)
            {
                if (m.DatumIVreme == Datum.ToString() && Ulica == m.Mesto.Ulica && Broj == m.Mesto.Broj && Grad == m.Mesto.Mesto)
                {
                    ViewBag.Error = "Vec postoji manifestaciju na datom mestu u dato vreme";
                    return View();
                }
                if (Naziv == m.Naziv)
                {
                    ViewBag.Error = "Vec postoji manifestacija sa datim nazivom";
                    return View();
                }
                if (!(Tip.Equals(TipManifestacije.Festival) || Tip.Equals(TipManifestacije.Izlozba) || Tip.Equals(TipManifestacije.Koncert) || Tip.Equals(TipManifestacije.Predstava) || Tip.Equals(TipManifestacije.Utakmica) || Tip.Equals(TipManifestacije.Zurka)))
                {
                    ViewBag.Error = "Molimo unesite ispravne podatke";
                    return View();
                }
                if (BrojMestaParter < 0)
                {
                    ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                    return View();
                }
                if (BrojMestaVip < 0)
                {
                    ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                    return View();
                }
                if (BrojMestaRegular < 0)
                {
                    ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                    return View();
                }
                if (Cena < 0)
                {
                    ViewBag.Error = "Cena ne moze biti negativan broj";
                    return View();
                }
                if (Broj < 0)
                {
                    ViewBag.Error = "Broj u adresi ne moze biti negativan broj";
                    return View();
                }
                if (Postanski < 0)
                {
                    ViewBag.Error = "Postanski broj ne moze biti negativan broj";
                    return View();
                }
            }

            Manifestacija man = new Manifestacija(Naziv, Tip,BrojMestaRegular,BrojMestaParter,BrojMestaVip, Datum.ToString(), Cena, false, new MestoOdrzavanja(Grad, Ulica, Broj, Postanski), "", 0, k.Username );

            dic.Add(man.Naziv, man);
            HttpContext.Application["manifestacije"] =dic;



            Data d = new Data();
            Data.SacuvajManifestaciju(dic);
          

            return View("~/Views/Pocetna/Index.cshtml", dic.Values.ToList<Manifestacija>());

 
        }

        public ActionResult AzurirajManifestaciju(string Naziv)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            foreach (var m in l)
            {
                if (Naziv == m.Naziv)
                    ViewBag.m = m;
            }


            return View();
        }

        [HttpPost]
        public ActionResult AzurirajManifestaciju(string StariNaziv, string Naziv, TipManifestacije Tip, int BrojMestaRegular, int BrojMestaParter, int BrojMestaVip, int Cena, DateTime Datum,string Ulica, int Broj, string Grad, int Postanski )
        {
            Korisnik k = (Korisnik)Session["prodavac"];
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

           

            foreach (var m in dic.Values)
            {
               

                if (StariNaziv == m.Naziv)
                {

                    if (m.DatumIVreme == Datum.ToString() && Ulica == m.Mesto.Ulica && Broj == m.Mesto.Broj && Grad == m.Mesto.Mesto)
                    {
                        ViewBag.Error = "Vec postoji manifestaciju na datom mestu u dato vreme";
                        ViewBag.m = m;
                        return View();
                    }

                    if (BrojMestaParter < 0)
                    {
                        ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    if (BrojMestaVip < 0)
                    {
                        ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    if (BrojMestaRegular < 0)
                    {
                        ViewBag.Error = "Broj mesta ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    if (Cena < 0)
                    {
                        ViewBag.Error = "Cena ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    if (Broj < 0)
                    {
                        ViewBag.Error = "Broj u adresi ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    if (Postanski < 0)
                    {
                        ViewBag.Error = "Postanski broj ne moze biti negativan broj";
                        ViewBag.m = m;
                        return View();
                    }
                    m.Naziv = Naziv;
                    m.Tip = Tip;
                    m.BrMestaRegular = BrojMestaRegular;
                    m.BrMestaParter = BrojMestaParter;
                    m.BrMestaVIP = BrojMestaVip;
                    m.CenaRegular = Cena;
                    m.DatumIVreme = Datum.ToString();
                    m.Mesto.Ulica = Ulica;
                    m.Mesto.Broj = Broj;
                    m.Mesto.PostanskiBroj = Postanski;
                    m.Mesto.Mesto = Grad;


              
                    HttpContext.Application["manifestacije"] = dic;
                    Data.SacuvajManifestaciju(dic);
                    ViewBag.Succ = "Uspesno ste izmenili dogadjaj";
                    
                    return View("~/Views/Pocetna/Index.cshtml", dic.Values.ToList<Manifestacija>());
                }
            }


            return View();
        }


        [HttpPost]
        public ActionResult DodajSliku(HttpPostedFileBase file, string Naziv)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();


            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),Path.GetFileName(file.FileName));
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    foreach (var m  in dic.Values)
                    {
                        if (m.Naziv == Naziv)
                        {
                            m.Poster = pic;
                            HttpContext.Application["manifestacije"] = dic;
                            Data.SacuvajManifestaciju(dic);
                            ViewBag.m = m;
                           
                            
                        }
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View("~/Views/Prodavac/AzurirajManifestaciju.cshtml");
        }


        [HttpPost]
        public ActionResult OdobriKomentar(string kom, string naziv)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            foreach (var m in dic.Values)
            {
                if (m.Naziv == naziv)
                {
                    foreach (var k in m.Komentari)
                    {
                        if (kom == k.Tekst)
                        {
                            m.Komentari.Remove(k);
                            k.Odobren = true;
                            m.Komentari.Add(k);
                            HttpContext.Application["manifestacije"] = dic;
                            Data.SacuvajManifestaciju(dic);

                            ViewBag.m = m;
                            return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");


                        }

                    }

                    

                }
            }

            return View();
           

            
        }

        public ActionResult PrikaziKarte()
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();
            List<Karta> ret = new List<Karta>();

            foreach (var m in l)
            {
                foreach (var k in m.RezervisaneKarte)
                {
                    if (k.Status == StatusKarte.Rezervisana)
                    {

                        ret.Add(k);

                    }



                }
                {

                }
            }

            ViewBag.Karte =ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");

        }

    }
}