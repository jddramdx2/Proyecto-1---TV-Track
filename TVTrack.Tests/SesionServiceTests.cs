using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVTrackWeb.Services;

namespace TVTrack.Tests
{
    [TestClass]
    public class SesionServiceTests
    {
        [TestMethod]
        public void IniciarSesion_AlmacenaDatosCorrectamente()
        {
            var service = new SesionService();
            service.IniciarSesion("Fabrizio", "admin");

            Assert.AreEqual("Fabrizio", service.Nombre);
            Assert.AreEqual("admin", service.Rol);
            Assert.IsTrue(service.EstaLogueado);
        }

        [TestMethod]
        public void CerrarSesion_LimpiaDatos()
        {
            var service = new SesionService();
            service.IniciarSesion("User", "user");
            service.CerrarSesion();

            Assert.IsFalse(service.EstaLogueado);
            Assert.AreEqual(string.Empty, service.Nombre);
            Assert.AreEqual(string.Empty, service.Rol);
        }
    }
}