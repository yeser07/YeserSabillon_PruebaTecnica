using Microsoft.AspNetCore.Mvc;
using YeserSabillon_PruebaTecnica.Repositorios;
using YeserSabillon_PruebaTecnica.Models;

namespace YeserSabillon_PruebaTecnica.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoRepositorio _repo;

        public ProductoController(ProductoRepositorio repo)
        {
            _repo = repo;
        }


        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            List<Producto> listaProductos = _repo.ListarProductos();

            int totalProductos = listaProductos.Count;
            int totalPages = (int)Math.Ceiling((double)totalProductos / pageSize);
            var productosPaginados = listaProductos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;



            return View("~/Pages/Producto/Index.cshtml", productosPaginados  );
        }

        public IActionResult Create()
        {
            return View("~/Pages/Producto/CrearProducto.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _repo.AgregarProducto(producto);
                return RedirectToAction("Index");
            }
            return View("~/Pages/Producto/CrearProducto.cshtml", producto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producto = _repo.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View("~/Pages/Producto/EditarProducto.cshtml", producto);
        }

        [HttpPost]
        public IActionResult Edit(int IdProducto, Producto producto)
        {
            if (ModelState.IsValid)
            {
                _repo.ActualizarProducto(IdProducto, producto);
                return RedirectToAction("Index");
            }
            return View("~/Pages/Producto/EditarProducto.cshtml", producto);
        }

        [HttpPost]
        public IActionResult Delete(int IdProducto)
        {
            _repo.EliminarProducto(IdProducto);
            return RedirectToAction("Index");
        }
    }
}
