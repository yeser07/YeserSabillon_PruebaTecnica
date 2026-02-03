using Microsoft.AspNetCore.Mvc;
using YeserSabillon_PruebaTecnica.Repositorios;
using YeserSabillon_PruebaTecnica.Models;
using Microsoft.Data.SqlClient;

namespace YeserSabillon_PruebaTecnica.Controllers
{
    public class FacturaController : Controller
    {
        private readonly FacturaRepositorio _facturaRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly ProductoRepositorio _productoRepositorio;
        public FacturaController
            (
            FacturaRepositorio facturaRepositorio,
            ClienteRepositorio clienteRepositorio,
            ProductoRepositorio productoRepositorio
            )
        {
            _facturaRepositorio = facturaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _productoRepositorio = productoRepositorio;


        }


        public IActionResult FacturaView()
        {
            var vm = new FacturaViewModel
            {
                Clientes = _clienteRepositorio.ListarClientes(),
                Productos = _productoRepositorio.ListarProductos()

            };

            return View("~/Pages/Factura/Factura.cshtml",vm);
        }

        [HttpGet]
        //[Route("/listaProductosJson")]
        public IActionResult ListaProductosJson()
        {
            var productos = _productoRepositorio.ListarProductos();
            return Json(productos);
        }


        [HttpPost]
        public JsonResult GuardarFactura([FromBody] FacturaDetalleRequest request)
        {
            try
            {
                int idFactura = _facturaRepositorio.GuardarFacturaCompleta(request);

                return Json(new
                {
                    success = true,
                    message = "Factura guardada exitosamente.",
                    idFactura
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
