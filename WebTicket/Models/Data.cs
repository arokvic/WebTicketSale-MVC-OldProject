using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace WebTicket.Models
{
    public class Data
    {


        public static void SacuvajKupca(Dictionary<string,Korisnik> dic)
        {
            List<Korisnik> lista = dic.Values.ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextWriter tw = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + @"korisnici.xml");

            serializer.Serialize(tw, lista);

            tw.Close();





        }


        public static Dictionary<string, Korisnik> IscitajKupca()
        {
            Dictionary<string, Korisnik> dicc = new Dictionary<string, Korisnik>();

            List<Korisnik> lista = new List<Korisnik>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextReader tr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"korisnici.xml");

            lista = (List<Korisnik>)serializer.Deserialize(tr);

            tr.Close();

            foreach (Korisnik k in lista)
            {
                dicc.Add(k.Username, k);
            }

            return dicc;

        }

        public static void SacuvajManifestaciju(Dictionary<string, Manifestacija> dic)
        {
            List<Manifestacija> lista = dic.Values.ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Manifestacija>));

            TextWriter tw = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + @"manifestacije.xml");

            serializer.Serialize(tw, lista);

            tw.Close();





        }

        public static Dictionary<string, Manifestacija> IscitajManifestacije()
        {
            Dictionary<string, Manifestacija> dicc = new Dictionary<string, Manifestacija>();

            List<Manifestacija> lista = new List<Manifestacija>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Manifestacija>));

            TextReader tr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"manifestacije.xml");

            lista = (List<Manifestacija>)serializer.Deserialize(tr);

            tr.Close();

            foreach (Manifestacija k in lista)
            {
                dicc.Add(k.Naziv, k);
            }

            return dicc;

        }


        public static void SacuvajAdmina(Dictionary<string, Korisnik> dic)
        {
            List<Korisnik> lista = dic.Values.ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextWriter tw = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + @"admini.xml");

            serializer.Serialize(tw, lista);

            tw.Close();





        }


        public static Dictionary<string, Korisnik> IscitajAdmina()
        {
            Dictionary<string, Korisnik> dicc = new Dictionary<string, Korisnik>();

            List<Korisnik> lista = new List<Korisnik>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextReader tr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"admini.xml");

            lista = (List<Korisnik>)serializer.Deserialize(tr);

            tr.Close();

            foreach (Korisnik k in lista)
            {
                dicc.Add(k.Username, k);
            }

            return dicc;

        }

        public static void SacuvajProdavca(Dictionary<string, Korisnik> dic)
        {
            List<Korisnik> lista = dic.Values.ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextWriter tw = new StreamWriter(HostingEnvironment.ApplicationPhysicalPath + @"prodavci.xml");

            serializer.Serialize(tw, lista);

            tw.Close();





        }


        public static Dictionary<string, Korisnik> IscitajProdavca()
        {
            Dictionary<string, Korisnik> dicc = new Dictionary<string, Korisnik>();

            List<Korisnik> lista = new List<Korisnik>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Korisnik>));

            TextReader tr = new StreamReader(HostingEnvironment.ApplicationPhysicalPath + @"prodavci.xml");

            lista = (List<Korisnik>)serializer.Deserialize(tr);

            tr.Close();

            foreach (Korisnik k in lista)
            {
                dicc.Add(k.Username, k);
            }

            return dicc;

        }





    }
}