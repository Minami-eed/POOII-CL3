using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using POOII_CL3.Models;

namespace POOII_CL3.Controllers
{
    public class ProductoController : Controller
    {
        // ADD DATA BASE -----------------------------------------------------------
        BDProducto bdp = new BDProducto();
        
        // INDEX -------------------------------------------------------------------
        public IActionResult Inicio()
        {
            return View();
        }

        // LISTAR ------------------------------------------------------------------
        public IActionResult Listado()
        {
            List<Producto> listaProductos = bdp.ObtenerTodos();
            return View(listaProductos);
        }

        public IActionResult BuscarPorID(int id)
        {
            // Obtener cliente por su dni
            //Cliente cliente = bdc.ObtenerPorDNI(dni);
            Producto producto = bdp.BuscarPorId(id);
            List<Producto> listaProductos = new List<Producto>();
            listaProductos.Add(producto);
            return View("Buscar", listaProductos);
        }

        // CREAR --------------------------------------------------------------------
        [HttpGet]
        public ActionResult Crear()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Crear(string Nombre, int idTipo, decimal Precio, DateTime fecha)
        {
            string fechaFormateada = fecha.ToString("yyyy-MM-dd");

            int nroRegistros = bdp.Crear(Nombre, idTipo, Precio, fecha);
            ViewBag.mensaje = "Producto creado correctamente";
            ViewBag.fechaFormateada = fechaFormateada;

            return View();
        }

        // ACTUALIZAR ----------------------------------------------------------------
        [HttpGet]
        public IActionResult Editar(int id)
        {
            Producto producto = bdp.BuscarPorId(id);
            return View(producto);
        }
        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            int nroRegistros = bdp.Actualizar(producto);
            if (nroRegistros == 1)
            {
                ViewBag.mensaje = "Cliente actualizado correctamente";
            }
            else
            {
                ViewBag.mensaje = "Cliente NO actualizado correctamente";
            }

            return View(producto);
        }

        // ELIMINAR -------------------------------------------------------------------
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            Producto producto = bdp.BuscarPorId(id);
            return View(producto);
        }
        [HttpPost]
        public IActionResult Eliminar(Producto producto)
        {
            Producto productoEliminar = bdp.BuscarPorId(producto.id);

            return View(productoEliminar);
        }
    }
}
