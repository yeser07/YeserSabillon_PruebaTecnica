using Microsoft.AspNetCore.Mvc;
using YeserSabillon_PruebaTecnica.Repositorios;
using YeserSabillon_PruebaTecnica.Models;

namespace YeserSabillon_PruebaTecnica.Controllers
{
    public class FacturaController : Controller
    {
        private readonly FacturaHeaderRepositorio _facturaHeaderRepositorio;
        private readonly FacturaDetalleRepositorio _facturaDetalleRepositorio;
        private readonly ClienteRepositorio _clienteRepositorio;
        private readonly ProductoRepositorio _productoRepositorio;
        public FacturaController
            (
            FacturaHeaderRepositorio facturaHeaderRepositorio, 
            FacturaDetalleRepositorio facturaDetalleRepositorio,
            ClienteRepositorio clienteRepositorio,
            ProductoRepositorio productoRepositorio
            )
        {
            _facturaHeaderRepositorio = facturaHeaderRepositorio;
            _facturaDetalleRepositorio = facturaDetalleRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _productoRepositorio = productoRepositorio;


        }


        public IActionResult FacturaView()
        {
            List<Cliente> ListaClientes = _clienteRepositorio.ListarClientes();

            return View("~/Pages/Factura/Factura.cshtml",ListaClientes);
        }
    }
}
