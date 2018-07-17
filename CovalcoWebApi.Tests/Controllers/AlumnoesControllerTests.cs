using Microsoft.VisualStudio.TestTools.UnitTesting;
using CovalcoWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CovalcoWebApi.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace CovalcoWebApi.Controllers.Tests
{
    [TestClass()]
    public class AlumnoesControllerTests
    {
        [TestMethod()]
        public void GetAlumnoTest()
        {
            AlumnoesController controller = new AlumnoesController();
            IQueryable<Alumno> alumnos = controller.GetAlumno();
            Assert.IsTrue(alumnos.Count<Alumno>() > 0);
        }

        [TestMethod()]
        public void GetAlumnoById()
        {
            AlumnoesController controller = new AlumnoesController();
            IHttpActionResult actionResult = controller.GetAlumno(1);

            var contentResult = actionResult as
                OkNegotiatedContentResult<Alumno>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod()]
        public void PutAlumnoTest()
        {
            AlumnoesController controller = new AlumnoesController();
            IHttpActionResult actionResult = controller.PutAlumno(1,
                new
                Alumno
                { Id = 1, Nombre = "Ferran", Apellidos = "Lopez", Dni = "23456784J" });

            Assert.IsNotNull(actionResult);
        }

        [TestMethod()]
        public void PostAlumnoTest()
        {
            AlumnoesController controller = new AlumnoesController();
            IHttpActionResult actionResult =
                controller.PostAlumno(
                    new Alumno
                    {
                        Nombre = "Ferran",
                        Apellidos = "Lopez",
                        Dni = "12345266Y"
                    });

            var contentResult =
                actionResult as
                CreatedAtRouteNegotiatedContentResult<Alumno>;

            Assert.IsNotNull(actionResult);
            Assert.IsTrue(contentResult.RouteName == "DefautApi");
        }

        [TestMethod()]
        public void DeleteAlumnoTest()
        {
            AlumnoesController controller = new AlumnoesController();
            IHttpActionResult actionResult =
                controller.PostAlumno(
                    new Alumno
                    {
                        Nombre = "Ferran",
                        Apellidos = "Lopez",
                        Dni = "12345266Y"
                    });

            var contentResult = actionResult as
                CreatedAtRouteNegotiatedContentResult<Alumno>;

            IHttpActionResult actionDeleteResult =
                controller.DeleteAlumno(contentResult.Content.Id);

            var contentDeleteResult = actionDeleteResult as
                OkNegotiatedContentResult<Alumno>;

            Assert.IsNotNull(contentDeleteResult);
            Assert.IsNotNull(contentDeleteResult.Content);
            Assert.IsTrue(contentDeleteResult.Content.Id == contentResult.Content.Id);
        }
    }
}