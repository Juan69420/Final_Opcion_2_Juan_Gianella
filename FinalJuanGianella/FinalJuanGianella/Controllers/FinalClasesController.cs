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
using FinalJuanGianella.Models;

namespace FinalJuanGianella.Controllers
{
    [RoutePrefix("api")]
    public class FinalClasesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/FinalClases
        public IQueryable<FinalClase> GetFinalClases()
        {
            return db.FinalClases;
        }

        [HttpGet]
        [Route("{numero:int}")]
        public string operaciones(int numero)
        {
            if(numero < 0)
            {
                return "ERROR";
            }
            if (numero == 0)
            {
                return "Realizado por Juan Gianella";
            }
            return "https://image.freepik.com/vector-gratis/numeros-cartel-imagen_1639-6453.jpg";
        }
        // GET: api/FinalClases/5
        [ResponseType(typeof(FinalClase))]
        public IHttpActionResult GetFinalClase(int id)
        {
            FinalClase finalClase = db.FinalClases.Find(id);
            if (finalClase == null)
            {
                return NotFound();
            }

            return Ok(finalClase);
        }

        // PUT: api/FinalClases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFinalClase(int id, FinalClase finalClase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != finalClase.numero)
            {
                return BadRequest();
            }

            db.Entry(finalClase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinalClaseExists(id))
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

        // POST: api/FinalClases
        [ResponseType(typeof(FinalClase))]
        public IHttpActionResult PostFinalClase(FinalClase finalClase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FinalClases.Add(finalClase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = finalClase.numero }, finalClase);
        }

        // DELETE: api/FinalClases/5
        [ResponseType(typeof(FinalClase))]
        public IHttpActionResult DeleteFinalClase(int id)
        {
            FinalClase finalClase = db.FinalClases.Find(id);
            if (finalClase == null)
            {
                return NotFound();
            }

            db.FinalClases.Remove(finalClase);
            db.SaveChanges();

            return Ok(finalClase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinalClaseExists(int id)
        {
            return db.FinalClases.Count(e => e.numero == id) > 0;
        }
    }
}