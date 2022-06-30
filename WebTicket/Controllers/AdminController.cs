using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Models;

namespace WebTicket.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DodajProdavca()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajProdavca(string Username, string Lozinka, string Ime, string Prezime, string Datum)
        {

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();


            Dictionary<string, Korisnik> prod = new Dictionary<string, Korisnik>();
            prod = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];
            foreach (var x in prod.Keys)
            {
                if (x == Username)
                {
                    ViewBag.Error = "prodavac sa ovim imenom vec postoji, pokusajte ponovo";
                    return View("~/Views/Admin/DodajProdavca.cshtml");
                }
            }
            Console.WriteLine(Username + Lozinka + Ime + Prezime + Datum);
            Korisnik k = new Korisnik(Username, Lozinka, Ime, Prezime, Datum);
            k.Uloga = Uloga.Prodavac;
            prod.Add(k.Username, k);




            Data d = new Data();
            Data.SacuvajProdavca(prod);
            Dictionary<string, Korisnik> k2 = Data.IscitajProdavca();
            ViewBag.Succ = "Uspesno ste dodali novog prodavca";
            return View("~/Views/Pocetna/Index.cshtml", l);
        }


        public ActionResult PrikaziKorisnike()
        {
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();
            List<Korisnik> ret = new List<Korisnik>();
            ret.AddRange(listkor);
            ret.AddRange(listadmin);
            ret.AddRange(listaprodavci);

            ViewBag.kup = ret;



            return View();
        }

        public ActionResult AktivirajManifestaciju(string Naziv)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            foreach (var m in dic.Values)
            {
                if (m.Naziv == Naziv)
                {
                    m.Aktivno = true;
                    ViewBag.Succ = "Uspesno ste aktivirali manifestaciju";
                    Data d = new Data();
                    Data.SacuvajManifestaciju(dic);
                    List<Manifestacija> l = dic.Values.ToList<Manifestacija>();


                    return View("~/Views/Pocetna/Index.cshtml", l);

                }


            }




            return View();
        }



        public ActionResult Search(string srr, string src)
        {
            List<Korisnik> ret = new List<Korisnik>();
            List<Korisnik> ret2 = new List<Korisnik>();
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();

            ret.AddRange(listkor);
            ret.AddRange(listadmin);
            ret.AddRange(listaprodavci);

            if (src == "Imenu")
            {
                foreach (var k in ret)
                {
                    if (srr == k.Ime)
                        ret2.Add(k);


                }

            }


            else if (src == "Prezimenu")
            {
                foreach (var k in ret)
                {
                    if (srr == k.Prezime)
                        ret2.Add(k);


                }

            }


            else if (src == "Korisnickom imenu")
            {
                foreach (var k in ret)
                {
                    if (srr == k.Username)
                        ret2.Add(k);


                }

            }
            else
            {
                ViewBag.Error = "Molimo Vas unesite validne podatke";
                ViewBag.kup = ret;
                return View("~/Views/Admin/PrikaziKorisnike.cshtml");

            }

          

            ViewBag.kup = ret2;

            return View("~/Views/Admin/PrikaziKorisnike.cshtml");


        }


        public ActionResult Sort(string srr, string src)
        {

            List<Korisnik> ret = new List<Korisnik>();
            List<Korisnik> ret2 = new List<Korisnik>();
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();

            ret.AddRange(listkor);
            ret.AddRange(listadmin);
            ret.AddRange(listaprodavci);



            if (srr == "Rastuci")
            {
                if (src == "Imenu")
                {
                    ret2 = ret.OrderBy(o => o.Ime).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }
                else if (src == "Prezimenu")
                {
                    ret2 = ret.OrderBy(o => o.Prezime).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }
                else if (src == "Korisnickom imenu")
                {
                    ret2 = ret.OrderBy(o => o.Username).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");


                }
                else if (src == "Broju sakupljenih bodova")
                {
                    ret2 = ret.OrderBy(o => o.Bodovi).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");

                }
            }
            else if (srr == "Opadajuci")
            {
                if (src == "Imenu")
                {
                    ret2 = ret.OrderByDescending(o => o.Ime).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }
                else if (src == "Prezimenu")
                {
                    ret2 = ret.OrderByDescending(o => o.Prezime).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }
                else if (src == "Korisnickom imenu")
                {
                    ret2 = ret.OrderByDescending(o => o.Username).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }
                else if (src == "Broju sakupljenih bodova")
                {
                    ret2 = ret.OrderByDescending(o => o.Bodovi).ToList();
                    ViewBag.kup = ret2;
                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");
                }



            }

            ViewBag.Error = "Molimo Vas unesite validne podatke";
            ViewBag.kup = ret;
            return View("~/Views/Admin/PrikaziKorisnike.cshtml");

        }


        public ActionResult Filtriraj(string flt)
        {
            List<Korisnik> ret = new List<Korisnik>();
            List<Korisnik> ret2 = new List<Korisnik>();
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();

            ret.AddRange(listkor);
            ret.AddRange(listadmin);
            ret.AddRange(listaprodavci);



            foreach (var k in ret)
            {
                if (flt == "prikazi admine")
                {
                    if (k.Uloga == Uloga.Administrator)
                        ret2.Add(k);
                }
                else if (flt == "prikazi prodavce")
                {
                    if (k.Uloga == Uloga.Prodavac)
                        ret2.Add(k);
                }
                else if (flt == "prikazi kupce")
                {
                    if (k.Uloga == Uloga.Kupac)
                        ret2.Add(k);
                }
                else
                {
                    ViewBag.kup = ret;
                    ViewBag.Error = "Molimo Vas unesite validne podatke";
                   

                    return View("~/Views/Admin/PrikaziKorisnike.cshtml");

                }


            }

            ViewBag.kup = ret2;

            return View("~/Views/Admin/PrikaziKorisnike.cshtml");




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



                    ret.Add(k);





                }
                {

                }
            }

            ViewBag.Karte = ret;

            return View("~/Views/Kupac/KarteKupca.cshtml");


        }

        [HttpPost]
        public ActionResult ObrisiKorisnika(string Naziv)
        {
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();
            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();

            List<Komentar> KometariKopija = new List<Komentar>();


            foreach (var k in kor.Values)
            {
                if (k.Username == Naziv)
                {


                    k.IsDeleted = true;
                    foreach (var kk in k.Karte)
                    {
                        kk.IsDeleted = true;
                    }
                    HttpContext.Application["kupci"] = kor;
                    Data.SacuvajKupca(kor);

                    foreach (var m in l)
                    {
                        List<Karta> rezKarteKopija = m.RezervisaneKarte.GetRange(0, m.RezervisaneKarte.Count);
                        KometariKopija = m.Komentari.GetRange(0, m.Komentari.Count);

                        foreach (var kart in rezKarteKopija)
                        {
                            if (kart.NazivKupca == k.Username)
                            {
                                Karta k3 = m.RezervisaneKarte.Where(u => u.IdKarte == kart.IdKarte).FirstOrDefault();

                                kart.IsDeleted = true;
                                m.RezervisaneKarte.Remove(k3);
                                dic.Remove(m.Naziv);
                                m.RezervisaneKarte.Add(kart);
                                dic.Add(m.Naziv, m);
                                HttpContext.Application["manifestacije"] = dic;
                                Data.SacuvajManifestaciju(dic);

                            }

                           
                         






                            foreach (var kom in KometariKopija)
                            {
                                if (kom.NazivKorisnika == k.Username)
                                {
                                    Komentar k3 = m.Komentari.Where(u => u.NazivKorisnika == Naziv).FirstOrDefault();
                                    kom.IsDeleted = true;
                                    m.Komentari.Remove(k3);
                                    dic.Remove(m.Naziv);
                                    m.Komentari.Add(kom);
                                    dic.Add(m.Naziv, m);
                                    HttpContext.Application["manifestacije"] = dic;
                                    Data.SacuvajManifestaciju(dic);


                                }



                            }



                        }


                    }


                }

            }


            foreach (var k in kor3.Values)
            {
                if (k.Username == Naziv)
                {
                    k.IsDeleted = true;
                    HttpContext.Application["prodavci"] = kor3;
                    Data.SacuvajProdavca(kor3);

                    foreach (var m in l)
                    {
                        if (m.NazivKreatora == Naziv)
                        {

                            dic.Remove(m.Naziv);
                            m.IsDeleted = true;
                            dic.Add(m.Naziv, m);
                            HttpContext.Application["manifestacije"] = dic;
                            Data.SacuvajManifestaciju(dic);
                            List<Karta> rezKarteKopija2 = m.RezervisaneKarte.GetRange(0, m.RezervisaneKarte.Count);
                            foreach (var kart in rezKarteKopija2)
                            {

                                foreach (var k4 in listkor)
                                {
                                    if (k4.Username == kart.NazivKupca)
                                    {

                                        Karta k3 = k4.Karte.Where(u => u.IdKarte == kart.IdKarte).FirstOrDefault();

                                        kart.IsDeleted = true;
                                        k4.Karte.Remove(k3);
                                        kor.Remove(k4.Username);

                                        k4.Karte.Add(kart);
                                        kor.Add(k4.Username, k4);
                                        HttpContext.Application["kupci"] = kor;
                                        Data.SacuvajKupca(kor);

                                    }



                                }






                                if (kart.NazivManifestacije == m.Naziv)
                                {

                                    Karta k3 = m.RezervisaneKarte.Where(u => u.IdKarte == kart.IdKarte).FirstOrDefault();

                                    kart.IsDeleted = true;
                                    m.RezervisaneKarte.Remove(k3);
                                    dic.Remove(m.Naziv);
                                    m.RezervisaneKarte.Add(kart);
                                    dic.Add(m.Naziv, m);
                                    HttpContext.Application["manifestacije"] = dic;
                                    Data.SacuvajManifestaciju(dic);

                                }


                            }
                        }


                    }
                }
            }


            List<Korisnik> ret = new List<Korisnik>();
            ret.AddRange(listadmin);
            ret.AddRange(listaprodavci);
            ret.AddRange(listkor);
            ViewBag.kup = ret;


            return View("~/Views/Admin/PrikaziKorisnike.cshtml");
        }




        [HttpPost]
        public ActionResult ObrisiManifestaciju(string Naziv)
        {
            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();
            List<Korisnik> lkor = kor.Values.ToList<Korisnik>();


            foreach (var m in l)
            {
                if (m.Naziv == Naziv)
                {


                    m.IsDeleted = true;
                    foreach (var mm in m.RezervisaneKarte)
                    {
                        mm.IsDeleted = true;
                    }
                    dic.Remove(m.Naziv);
                    dic.Add(m.Naziv, m);
                    HttpContext.Application["manifestacije"] = dic;
                    Data.SacuvajManifestaciju(dic);
                    List<Karta> rezKarteKopija = m.RezervisaneKarte.GetRange(0, m.RezervisaneKarte.Count);
                    


                    foreach (var kart in rezKarteKopija)
                    {
                        if (kart.NazivManifestacije == m.Naziv)
                        {
                            Karta k3 = m.RezervisaneKarte.Where(u => u.IdKarte == kart.IdKarte).FirstOrDefault();

                            kart.IsDeleted = true;
                            m.RezervisaneKarte.Remove(k3);
                            dic.Remove(m.Naziv);
                            m.RezervisaneKarte.Add(kart);
                            dic.Add(m.Naziv, m);
                            HttpContext.Application["manifestacije"] = dic;
                            Data.SacuvajManifestaciju(dic);
                            
                        }

                        foreach (var k in lkor)
                        {
                            if (k.Username == kart.NazivKupca)
                            {

                                Karta k3 = k.Karte.Where(u => u.IdKarte == kart.IdKarte).FirstOrDefault();

                                kart.IsDeleted = true;
                                k.Karte.Remove(k3);
                                kor.Remove(k.Username);

                            k.Karte.Add(kart);
                            kor.Add(k.Username, k);
                            HttpContext.Application["kupci"] = kor;
                            Data.SacuvajKupca(kor);

                            }



                    }


                    }

                   


                }
            }

            return RedirectToAction("Index", "Pocetna");

        }




        [HttpPost]
        public ActionResult ObrisiKomentar(string kom, string naziv)
        {
            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];

            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();

            foreach (var m in dic.Values)
            {
                if (m.Naziv == naziv)
                {
                    foreach (var komm in m.Komentari)
                    {
                        if (komm.Tekst == kom)
                        {
                            komm.IsDeleted = true;
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




    }
}
    
