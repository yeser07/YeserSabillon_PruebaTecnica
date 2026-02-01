using Microsoft.AspNetCore.Mvc;
using YeserSabillon_PruebaTecnica.Models;
using YeserSabillon_PruebaTecnica.Repositorios;

namespace YeserSabillon_PruebaTecnica.Controllers
{

    public class ClienteController : Controller
    {
        private readonly ClienteRepositorio _repo;

        public ClienteController(ClienteRepositorio repo)
        {
            _repo = repo;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            List<Cliente> listaClientes = _repo.ListarClientes();

            int totalClientes = listaClientes.Count;
            int totalPages = (int)Math.Ceiling(totalClientes / (double)pageSize);

            // Clientes de la página actual
            var clientesPaginados = listaClientes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View("~/Pages/Cliente/Index.cshtml", clientesPaginados);
        }

        public IActionResult Create()
        {
            return View("~/Pages/Cliente/CrearCliente.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repo.AgregarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View("~/Pages/Cliente/CrearCliente.cshtml", cliente);
        }

        [HttpGet]
        [Route("Cliente/Edit/{IdCliente}")]
        public IActionResult Edit(int IdCliente)
        {
            Cliente cliente = _repo.ObtenerCliente(IdCliente);
            if (cliente == null)
            {
                return NotFound();
            }
            return View("~/Pages/Cliente/EditarCliente.cshtml", cliente);
        }

        [HttpPost]
        [Route("Cliente/Edit/{IdCliente}")]
        public IActionResult Edit(int IdCliente, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repo.ActualizarCliente(IdCliente, cliente);
                return RedirectToAction("Index");
            }
            return View("~/Pages/Cliente/EditarCliente.cshtml", cliente);
        }

        [HttpPost]
        //[Route("Cliente/Delete/{IdCliente}")]
        public IActionResult Delete(int IdCliente)
        {
            _repo.EliminarCliente(IdCliente);
            return RedirectToAction("Index");
        }
    }
}
