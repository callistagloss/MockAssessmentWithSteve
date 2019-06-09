using MockAssessment7WithSteve.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MockAssessment7WithSteve.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        //[HttpPost]
        public ActionResult Search(int Id)
        {
            string donutURL = $"https://grandcircusco.github.io/demo-apis/donuts/{Id}.json";

            HttpWebRequest request = WebRequest.CreateHttp(donutURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string APIText = rd.ReadToEnd();

            JToken donutToken = JToken.Parse(APIText);

            Doughnut d = new Doughnut();

            d.Name = donutToken["name"].ToString();
            d.Calories = (int)donutToken["calories"];

            if (donutToken["photo"] != null)
            {
                d.PhotoURL = donutToken["photo"].ToString();
            }

            List<JToken> jt = new List<JToken>();

            List<string> donutExtras = new List<string>();

            if(donutToken["extras"] != null)
                {
                foreach(JToken j in donutToken["extras"])
                {
                    donutExtras.Add(j.ToString());
                }

                d.Extras = donutExtras.ToArray();

            }
                ViewBag.Donut = d;
                return View(d);

        }
    }
}