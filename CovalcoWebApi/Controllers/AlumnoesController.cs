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
using CovalcoWebApi.Models;

// código que tiene que ir en repository, son consulatas linq

namespace CovalcoWebApi.Controllers
{
    public class AlumnoesController : ApiController
    {
        private CovalcoEntities db = new CovalcoEntities();   // aqui cogemos la conexión

        // GET: api/Alumnoes
        public IQueryable<Alumno> GetAlumno()
        {
            return db.Alumno;   // select
        }

        // GET: api/Alumnoes/5
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult GetAlumno(int id)
        {
            Alumno alumno = db.Alumno.Find(id);  // recuperara alumno por id
            if (alumno == null)
            {
                return NotFound(); // trabajamos con un enum que es results, y ahi hay un valor NotFound que se devuelve por http.
            }

            return Ok(alumno);  // aqui se puede devovler el 200(ok) + alumno encontrado (entidad a devolver)
        }

        // PUT: api/Alumnoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlumno(int id, Alumno alumno)   // en put se envia alumno y id, en post solo el objeto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno.Id)
            {
                return BadRequest();  //validaciones http
            }

            db.Entry(alumno).State = EntityState.Modified;  // se tiene q marcar el objeto como sucio, que se ha modificado

            try
            {
                db.SaveChanges();  
            }
            catch (DbUpdateConcurrencyException)  // se tiene que hacer catch
            {
                if (!AlumnoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent); // NoContent devuelve VOID = todo ha ido bien
        }

        // POST: api/Alumnoes
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult PostAlumno(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alumno.Add(alumno);  // agrega alumno dentro de la persistencia, aqu el alumno no tiene id
            db.SaveChanges(); // aqui se guarda, después del savechanges alumno tiene id

            return CreatedAtRoute("DefaultApi", new { id = alumno.Id }, alumno);  // CreatedAtRoute -> redirecciona as defaultAPi, enviando nuevo id del alumno y el objeto alumno creado
        }

        // DELETE: api/Alumnoes/5
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult DeleteAlumno(int id)
        {
            Alumno alumno = db.Alumno.Find(id);  // Primero hace un select para buscar el objeto en el contectp
            if (alumno == null)
            {
                return NotFound();
            }

            db.Alumno.Remove(alumno);  // se hace remove
            db.SaveChanges();

            return Ok(alumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing); // dispose de la conexion de la base de datos, como un clsoe
        }

        private bool AlumnoExists(int id)   // alumno exists con una lambda expression
        {
            return db.Alumno.Count(e => e.Id == id) > 0;  // cuantame todos los alumnos cuyo id es el que te pongo, si el valor es > 0 te devuelve un true.
        }
    }
}