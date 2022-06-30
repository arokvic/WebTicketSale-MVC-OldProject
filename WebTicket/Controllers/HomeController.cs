using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTicket.Models;

namespace WebTicket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Register()
        {
            

            return View("~/Views/Auth/Reg.cshtml");
        }


        [HttpPost]
        public ActionResult Register(string Username, string Lozinka, string Ime, string Prezime, string Datum)
        {

            

            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
         
            kor = (Dictionary<string,Korisnik>)HttpContext.Application["kupci"];
            foreach (var x in kor.Keys)
            {
                if (x== Username)
                {
                    ViewBag.Error = "korisnik sa ovim imenom vec postoji, pokusajte ponovo";
                    return View("~/Views/Auth/Reg.cshtml");
                }
            }
          

            Console.WriteLine(Username + Lozinka + Ime + Prezime + Datum);
            Korisnik k = new Korisnik(Username, Lozinka, Ime, Prezime, Datum);
            k.Uloga = Uloga.Kupac;
            kor.Add(k.Username, k);
               
        

            
            Data d = new Data();
            Data.SacuvajKupca(kor);
            Dictionary<string,Korisnik> k2 = Data.IscitajKupca();

            return RedirectToAction("Login", "Home");
            
        }



        public ActionResult Login()
        {


            return View("~/Views/Auth/Login.cshtml");
        }


        [HttpPost]
        public ActionResult Login(string Username, string Lozinka)
        {

            Dictionary<string, Manifestacija> dic = new Dictionary<string, Manifestacija>();
            dic = (Dictionary<string, Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> l = dic.Values.ToList<Manifestacija>();


            Dictionary<string, Korisnik> kor = new Dictionary<string, Korisnik>();
            kor = (Dictionary<string, Korisnik>)HttpContext.Application["kupci"];

            Dictionary<string, Korisnik> kor2 = new Dictionary<string, Korisnik>();
            kor2 = (Dictionary<string, Korisnik>)HttpContext.Application["admini"];


            Dictionary<string, Korisnik> kor3 = new Dictionary<string, Korisnik>();
            kor3 = (Dictionary<string, Korisnik>)HttpContext.Application["prodavci"];

            List<Korisnik> listkor = kor.Values.ToList<Korisnik>();
            List<Korisnik> listadmin = kor2.Values.ToList<Korisnik>();
            List<Korisnik> listaprodavci = kor3.Values.ToList<Korisnik>();

            foreach (var k in listkor)
            {
                if (k.Username == Username && k.Lozinka == Lozinka)
                {
                    if (k.IsDeleted == false)
                    {
                        Session["kupac"] = k;
                        ViewBag.Succ = "Dobrodosao, " + k.Username;
                        return View("~/Views/Pocetna/Index.cshtml", l);
                    }
                    else
                    {
                        ViewBag.Error = "Pogresno korisnicko ime ili sifra, pokusajte ponovo";
                        return View("~/Views/Auth/Login.cshtml");
                    }

                }
            }

            foreach (var k in listadmin)
            {
                if (k.Username == Username && k.Lozinka == Lozinka)
                {
                    Session["admin"] = k;
                    ViewBag.Succ = "Dobrodosao, " + "admine " + k.Username;
                    return View("~/Views/Pocetna/Index.cshtml",l);


                }
            }
           
            foreach (var k in listaprodavci)
            {
                if (k.Username == Username && k.Lozinka == Lozinka)
                {
                    if (k.IsDeleted == false)
                    {
                        Session["prodavac"] = k;
                        ViewBag.Succ = "Dobrodosao, " + "kupac " + k.Username;
                        return View("~/Views/Pocetna/Index.cshtml", l);
                    }
                    else
                    {
                        ViewBag.Error = "Pogresno korisnicko ime ili sifra, pokusajte ponovo";
                        return View("~/Views/Auth/Login.cshtml");
                    }


                }
            }



            ViewBag.Error = "Pogresno korisnicko ime ili sifra, pokusajte ponovo";
            return View("~/Views/Auth/Login.cshtml");
        }


        public ActionResult Logout()
        {
           


            Session["prodavac"] = null;
            Session["kupac"] = null;
            Session["admin"] = null;

            return View("~/Views/Auth/Login.cshtml");


        }



    }
}