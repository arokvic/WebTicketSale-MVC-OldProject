using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Models;

namespace WebTicket.Controllers
{
    public class PocetnaController : Controller
    {
        // GET: Pocetna
        public ActionResult Index()
        {
            /* Manifestacija m = new Manifestacija("Exit", "Festival", 1000, "07.07.2020", 6000, true, new MestoOdrzavanja("Novi sad", "Petrovaradin", 20), "asd", 0, 100000);
             Manifestacija m2 = new Manifestacija("Sea Dance", "Festival", 900, "11.08.2020", 2000, true, new MestoOdrzavanja("Budva", "Tolstojeva", 13), "adsd", 0, 1000);
             Manifestacija m3 = new Manifestacija("Veciti Derbi", "Utakmica", 1020, "09.07.2020", 3000, true, new MestoOdrzavanja("Beograd", "Humska", 1), "assd", 0, 30000);
             Dictionary<string, Manifestacija> l =new Dictionary<string, Manifestacija>();
             l.Add(m.Naziv,m);
             l.Add(m2.Naziv,m2);
             l.Add(m3.Naziv,m3);
             Data.SacuvajManifestaciju(l);*/
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();



           
            return View(l);
        }


        public ActionResult Search(string srr, string src)
        {
            Dictionary<string, Manifestacija> manifestacijee = new Dictionary<string, Manifestacija>();
            manifestacijee = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> manifestacije = manifestacijee.Values.ToList<Manifestacija>();

            if (src == "Nazivu manifestacije")
            {
                List<Manifestacija> ret = new List<Manifestacija>();
                foreach (var man in manifestacije)
                {
                    if (man.Naziv.ToLower().Equals(srr.ToLower()))
                        ret.Add(man);
                }
                if (ret.Count.Equals(0))
                    ViewBag.Message = "Ne postoji manifestacija takvog imena";
                ViewBag.Manifestacija = ret;
                return View("~/Views/Pocetna/Index.cshtml", ret);

            }

            if (src == "Mesto odrzavanja(Grad/Drzava)")
            {
                List<Manifestacija> ret = new List<Manifestacija>();
                foreach (var man in manifestacije)
                {
                    if (man.Mesto.Mesto.ToLower().Equals(srr.ToLower()))
                        ret.Add(man);
                }
                if (ret.Count.Equals(0))
                    ViewBag.Message = "Ne postoji manifestacija u datom gradu";
                ViewBag.Manifestacija = ret;
                return View("~/Views/Pocetna/Index.cshtml", ret);

            }

            if (src == "Datumu")
            {
                List<Manifestacija> ret = new List<Manifestacija>();
                foreach (var man in manifestacije)
                {
                    if (man.DatumIVreme.Equals(srr))
                        ret.Add(man);
                }
                if (ret.Count.Equals(0))
                    ViewBag.Message = "Ne postoji manifestacija tog dana";
                ViewBag.Manifestacija = ret;
                return View("~/Views/Pocetna/Index.cshtml", ret);

            }



            ViewBag.Error = "Unesite validne vrednosti";

            return View("~/Views/Pocetna/Index.cshtml", manifestacije);
        }

        public ActionResult SearchCena(string srr, string src)
        {



            Dictionary<string, Manifestacija> manifestacijee = new Dictionary<string, Manifestacija>();
            manifestacijee = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> manifestacije = manifestacijee.Values.ToList<Manifestacija>();

            List<Manifestacija> ret = new List<Manifestacija>();

            if (Int32.Parse(srr) < 0 || Int32.Parse(srr) < 0 || Int32.Parse(srr) > Int32.Parse(src))
            {
                ViewBag.Error = "Molimo vas unesite validne vrednosti";
                
                return View("~/Views/Pocetna/Index.cshtml", manifestacije);


            }



            foreach (var man in manifestacije)
            {
                if (man.CenaRegular >= Int32.Parse(srr) && man.CenaRegular <= Int32.Parse(src))
                    ret.Add(man);
            }
            if (ret.Count.Equals(0))
                ViewBag.Message = "Ne postoji manifestacija u datom rasponu cene";
            ViewBag.Manifestacija = ret;
            return View("~/Views/Pocetna/Index.cshtml", ret);


        }


        public ActionResult SearchDatum(DateTime srr, DateTime src)
        {
            Dictionary<string, Manifestacija> manifestacijee = new Dictionary<string, Manifestacija>();
            manifestacijee = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> l = manifestacijee.Values.ToList<Manifestacija>();
            List<Manifestacija> ret = new List<Manifestacija>();

            string[] trazenjeDatumi1 = srr.ToString("dd.MM.yyyy").Split('.');
            string[] trazenjeDatumi2 = src.ToString("dd.MM.yyyy").Split('.');
            foreach (var man in l)
            {
                string[] manDatumi = man.DatumIVreme.Split('.');
               


                if (Int32.Parse(manDatumi[2]) > Int32.Parse(trazenjeDatumi1[2]) && Int32.Parse(manDatumi[2]) < Int32.Parse(trazenjeDatumi2[2]))
                {
                    ret.Add(man);
                }
                else if (Int32.Parse(manDatumi[2]) == Int32.Parse(trazenjeDatumi1[2]) && Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi1[1]))
                {
                    if (Int32.Parse(manDatumi[1]) > Int32.Parse(trazenjeDatumi1[1]) && (Int32.Parse(manDatumi[1]) < Int32.Parse(trazenjeDatumi2[1])))
                    {
                        ret.Add(man);
                    }
                    else if (Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi1[1]) && (Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi2[1])))
                    {
                        if (Int32.Parse(manDatumi[0]) >= Int32.Parse(trazenjeDatumi1[0]) && Int32.Parse(manDatumi[0]) <= Int32.Parse(trazenjeDatumi2[0]))
                            ret.Add(man);
                    }
                    else if (Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi1[1])) { 

                        if (Int32.Parse(manDatumi[0]) >= Int32.Parse(trazenjeDatumi1[0])) {
                            ret.Add(man);
                        }
                        if (Int32.Parse(manDatumi[0]) <= Int32.Parse(trazenjeDatumi2[0]))
                        {
                            ret.Add(man);
                        }
                    }

                    break;

                }
                else if (Int32.Parse(manDatumi[2]) == Int32.Parse(trazenjeDatumi1[2]))
                {
                    if (Int32.Parse(manDatumi[1]) > Int32.Parse(trazenjeDatumi1[1]))
                    {
                        ret.Add(man);
                    }
                    else if (Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi1[1]))
                    {
                        if (Int32.Parse(manDatumi[0]) >= Int32.Parse(trazenjeDatumi1[0]))
                        {
                            ret.Add(man);
                        }
                    }



                }
                else if (Int32.Parse(manDatumi[2]) == Int32.Parse(trazenjeDatumi2[2]))
                {
                    if (Int32.Parse(manDatumi[1]) < Int32.Parse(trazenjeDatumi2[1]))
                    {
                        ret.Add(man);
                    }
                    else if (Int32.Parse(manDatumi[1]) == Int32.Parse(trazenjeDatumi2[1]))
                    {
                        if (Int32.Parse(manDatumi[0]) <= Int32.Parse(trazenjeDatumi2[0]))
                        {
                            ret.Add(man);
                        }
                    }



                }



            }
            if (ret.Count.Equals(0))
                ViewBag.Message = "Ne postoji manifestacija u datom rasponu datuma";
            ViewBag.Manifestacija = ret;
            return View("~/Views/Pocetna/Index.cshtml", ret);



        }

        public ActionResult Sort(string srr, string src)
        {
            Dictionary<string, Manifestacija> manifestacijee = new Dictionary<string, Manifestacija>();
            manifestacijee = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> manifestacije = manifestacijee.Values.ToList<Manifestacija>();

          

            if (srr == "Rastuci")
            {
                if (src== "Nazivu manifestacije")
                {
                    List<Manifestacija> sorted = manifestacije.OrderBy(o => o.Naziv).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Mesto odrzavanja")
                {
                    List<Manifestacija> sorted = manifestacije.OrderBy(o => o.Mesto.Mesto).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Datumu")
                {
                    List<Manifestacija> sorted = manifestacije.OrderBy(o => DateTime.ParseExact(o.DatumIVreme, "dd.MM.yyyy", null)).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Ceni")
                {
                    List<Manifestacija> sorted = manifestacije.OrderBy(o => o.CenaRegular).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }



            }


            if (srr == "Opadajuci")
            {
                if (src == "Nazivu manifestacije")
                {
                    List<Manifestacija> sorted = manifestacije.OrderByDescending(o => o.Naziv).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Mesto odrzavanja")
                {
                    
                    List<Manifestacija> sorted = manifestacije.OrderByDescending(o => o.Mesto.Mesto).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Datumu")
                {
                    List<Manifestacija> sorted = manifestacije.OrderByDescending(o => DateTime.ParseExact(o.DatumIVreme, "dd.MM.yyyy",null)).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }
                if (src == "Ceni")
                {
                    List<Manifestacija> sorted = manifestacije.OrderByDescending(o => o.CenaRegular).ToList();
                    ViewBag.Manifestacija = sorted;
                    return View("~/Views/Pocetna/Index.cshtml", sorted);
                }



            }

            ViewBag.Error = "Unesite validne vrednosti";

             return View("~/Views/Pocetna/Index.cshtml", manifestacije);
        }


        public ActionResult Filtriraj(string flt)
        {
            Dictionary<string, Manifestacija> manifestacijee = new Dictionary<string, Manifestacija>();
            manifestacijee = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> l = new List<Manifestacija>();


            foreach (var man in manifestacijee.Values)
            {
                if (flt == "prikazi nerasprodate manifestacije")
                {
                    if (man.BrKarata > 0)
                        l.Add(man);
                }
                else if (flt == "prikazi utakmice")
                {
                    if (man.Tip == TipManifestacije.Utakmica)
                        l.Add(man);
                }
                else if (flt == "prikazi festivale")
                {
                    if (man.Tip == TipManifestacije.Festival)
                        l.Add(man);
                }
                else if (flt == "prikazi zurke")
                {
                    if (man.Tip == TipManifestacije.Zurka)
                        l.Add(man);
                }
                else if (flt == "prikazi predstave")
                {
                    if (man.Tip == TipManifestacije.Predstava)
                        l.Add(man);
                }
                else if (flt == "prikazi izlozbe")
                {
                    if (man.Tip == TipManifestacije.Izlozba)
                        l.Add(man);
                }
                else if (flt == "prikazi koncerte")
                {
                    if (man.Tip == TipManifestacije.Koncert)
                        l.Add(man);
                }
            }

            ViewBag.Error = "Unesite validne vrednosti";

            return View("~/Views/Pocetna/Index.cshtml", manifestacijee.Values);



        }

        public ActionResult IzmeniProfil()
        {
            Korisnik k = (Korisnik)Session["prodavac"];
            if (k == null)
                k = (Korisnik)Session["admin"];
            if (k == null)
                k = (Korisnik)Session["kupac"];


            return View(k);
        }


        [HttpPost]
        public ActionResult IzmeniProfil(string Username, string Password, string Ime, string Prezime, string Datum, string StariNaziv) {

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();


            Korisnik prethodniKupac = (Korisnik)Session["kupac"];
            Korisnik prethodniAdmin = (Korisnik)Session["admin"];
            Korisnik prethodniProdavac = (Korisnik)Session["prodavac"];

            Dictionary<string, Korisnik> kup = new Dictionary<string, Korisnik>();
            kup = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];
            List<Korisnik> listakup = kup.Values.ToList<Korisnik>();

            Dictionary<string, Korisnik> adm = new Dictionary<string, Korisnik>();
            adm = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];
            List<Korisnik> listadm = adm.Values.ToList<Korisnik>();

            Dictionary<string, Korisnik> prod = new Dictionary<string, Korisnik>();
             prod= (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];
            List<Korisnik> listaprod = prod.Values.ToList<Korisnik>();
            Korisnik k1 = new Korisnik(Username, Password, Ime, Prezime, Datum);

            foreach (var k in kup.Values)
            {
                if (prethodniKupac != null && prethodniKupac.Username == k.Username)
                {
                    // kup.Remove(Username);
                    // kup.Add(k1.Username, k1);
                    k.Username = Username;
                    k.Ime = Ime;
                    k.Lozinka = Password;
                    k.Prezime = Prezime;
                    k.Datum = Datum;
                    HttpContext.Application["kupci"] = kup;
                    Data.SacuvajKupca(kup);
                    ViewBag.Succ = "Uspesno ste izmenili profil";
                    Session["kupac"] = k1;
                    return View("~/Views/Pocetna/Index.cshtml", l);
                }
               

                   



            }
            

            foreach (var k in adm.Values)
            {
                if (prethodniAdmin!=null && prethodniAdmin.Username == k.Username)
                {
                    //  adm.Remove(Username);
                    // adm.Add(k1.Username, k1);
                    k.Username = Username;
                    k.Ime = Ime;
                    k.Lozinka = Password;
                    k.Prezime = Prezime;
                    k.Datum = Datum;
                    HttpContext.Application["admini"] = adm;
                    Data.SacuvajAdmina(adm);
                    ViewBag.Succ = "Uspesno ste izmenili profil";
                    Session["admin"] = k1;

                    return View("~/Views/Pocetna/Index.cshtml", l);

                }
            }

            foreach (var k in prod.Values)
            {
                if (prethodniProdavac != null && prethodniProdavac.Username == k.Username)
                {
                    // prod.Remove(Username);
                    //prod.Add(k1.Username, k1);
                    k.Username = Username;
                    k.Ime = Ime;
                    k.Lozinka = Password;
                    k.Prezime = Prezime;
                    k.Datum = Datum;
                    HttpContext.Application["prodavci"] = prod;
                    Data.SacuvajProdavca(prod);
                    ViewBag.Succ = "Uspesno ste izmenili profil";
                    Session["prodavac"] = k1;

                    return View("~/Views/Pocetna/Index.cshtml", l);

                }
            }



            return View();
        }

        public ActionResult PrikaziManifestaciju(string Name)
        {

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            foreach (var m in l)
            {
                if (Name == m.Naziv)
                {
                    ViewBag.m = m;
                    return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");

                }
            }


            return View();
        }
        [HttpPost]
        public ActionResult PrikaziManifestaciju(string Name, string x)
        {

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            foreach (var m in l)
            {
                if (Name == m.Naziv)
                {
                    ViewBag.m = m;
                    return View("~/Views/Pocetna/PrikaziManifestaciju.cshtml");

                }
            }


            return View();
        }


    }
}