using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebTicket.Models;

namespace WebTicket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Dictionary<string, Korisnik> kupci = WebTicket.Models.Data.IscitajKupca();
            HttpContext.Current.Application["kupci"] = kupci;
           // Dictionary<string, Korisnik> d = new Dictionary<string, Korisnik>();
          //  Data.SacuvajKupca(d);

            // Dictionary<string, Manifestacija> d = new Dictionary<string, Manifestacija>();
            //  Manifestacija m = new Manifestacija("srbija turska", TipManifestacije.Utakmica, 10, 13, 12, "12.12.2012", 1500, false, new MestoOdrzavanja("Beograd", "Humska", 3, 25000), "", 0);
            // d.Add(m.Naziv, m);
            //Data.SacuvajManifestaciju(d);

            Dictionary<string, Manifestacija> manifestacije = WebTicket.Models.Data.IscitajManifestacije();
            HttpContext.Current.Application["manifestacije"] = manifestacije;

               

            Dictionary<string, Korisnik> admini = WebTicket.Models.Data.IscitajAdmina();
            HttpContext.Current.Application["admini"] = admini;

            
            Dictionary<string, Korisnik> prodavci = WebTicket.Models.Data.IscitajProdavca();
            HttpContext.Current.Application["prodavci"] = prodavci;


        }
    }
}
