using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Services.Models;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using Services.Exceptions;

namespace Services.Controllers
{
    public class ArticlesController : ApiController
    {
        private SuperZapatosEntities db = new SuperZapatosEntities();

        // GET: api/Articles
        public HttpResponseMessage GetArticles()
        {
            List<ArticleStoreName> articles = (from article in db.Articles
                                              join store in db.Stores on article.store_id equals store.id
                                              select new ArticleStoreName { id = article.id
                                                                           ,name=article.name
                                                                           ,description=article.description
                                                                           ,price=article.price
                                                                           ,total_in_shelf=article.total_in_shelf
                                                                           ,total_in_vault=article.total_in_vault
                                                                           ,store_id=article.store_id
                                                                           ,store_name=store.name}).ToList();

            HttpResponseMessage response = new HttpResponseMessage();
            var content = new { articles = articles, success = true, total_elements = articles.Count() };

            response.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            return response;
        }

        // GET: api/Articles/Stores
        [Route("services/Articles/Stores/{store_id}")]
        [HttpGet]
        public HttpResponseMessage GetArticlesByStore(string store_id)
        {
            HttpResponseMessage response;
            
            int result;

            try
            {
                if (!int.TryParse(store_id, out result))
                    throw new WrongParametersException();
                else if (db.Stores.Find(int.Parse(store_id)) == null)
                    throw new NoStoreWithThatIdException();
                else
                {
                    List<ArticleStoreName> articles = (from store in db.Stores
                                                       join article in db.Articles on store.id equals article.store_id
                                                       where store.id == int.Parse(store_id)
                                                       select new ArticleStoreName
                                                       {
                                                           id = article.id
                                                                                    ,
                                                           name = article.name
                                                                                    ,
                                                           description = article.description
                                                                                    ,
                                                           price = article.price
                                                                                    ,
                                                           total_in_shelf = article.total_in_shelf
                                                                                    ,
                                                           total_in_vault = article.total_in_vault
                                                                                    ,
                                                           store_id = article.store_id
                                                                                    ,
                                                           store_name = store.name
                                                       }).ToList();

                    response = new HttpResponseMessage();

                    var content = new { articles = articles, success = true, total_elements = articles.Count() };

                    response.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
                }
            }
            catch (WrongParametersException wpe)
            {
                var content = new { error_msg = "Bad Request", error_code = HttpStatusCode.BadRequest, success = false };
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, wpe);
                response.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
                response.ReasonPhrase = "Wrong parameters (id is not a number) ";
            }
            catch (NoStoreWithThatIdException nswtie)
            {
                var content = new { error_msg = "Record not Found", error_code = HttpStatusCode.NotFound, success = false };
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, nswtie);
                response.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
                response.ReasonPhrase = "No store with that ID ";
            }

            return response;
        }

        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.id)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articles
        [ResponseType(typeof(Article))]
        public IHttpActionResult PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articles.Add(article);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = article.id }, article);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(int id)
        {
            return db.Articles.Count(e => e.id == id) > 0;
        }
    }
}