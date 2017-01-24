using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperZapatos.Models;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SuperZapatos.Controllers
{
    public class StoreController : Controller
    {
        private String BASE_URL = "http://localhost:64470/services/Stores/";

        // GET: Store
        public ActionResult Index()
        {
            return View("StoreView");
        }

        public JsonResult GetStores()
        {
            WebClient storeClient = new WebClient();
            var content = storeClient.DownloadString(this.BASE_URL);
            Object data = JsonConvert.DeserializeObject<Object>(content);

            //JToken outer = JToken.Parse(data.ToString());
            //JObject inner = outer["stores"].Value<JObject>();

            //List<string> keys = inner.Properties().Select(p => p.Name).ToList();

            //foreach (string k in keys)
            //{
            //    Console.WriteLine(k);
            //}



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
