using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly SistemaContext _context;

        //contructor recibe el contexto del modelo de datos
        public CategoriasController(SistemaContext context)
        {
            _context = context;
        }

        // GET: Categorias
        //muestra el listado de todas las categorias
        public async Task<IActionResult> Index(String orderSort, string search)//mod| para ordenarlos de manera asc o desc pasamos un parametro
        {

            ViewData["NombreSort"] = String.IsNullOrEmpty(orderSort) ? "nombre_desc" : "";
            ViewData["DescripcionParam"] = orderSort == "descripcion_asc" ? "descripcion_desc" : "descripcion_asc";
            ViewData["CurrentFilter"] = search;

            //tabla temporal
            var categorias = from s in _context.Categoria select s;

            //para le buscador
            if (!String.IsNullOrEmpty(search))
            {
                //busca por el nombre o descripcion
                categorias = categorias.Where(s => s.nombre.Contains(search) || s.descripcion.Contains(search));
            }

            
            switch (orderSort)
            {
                case "nombre_desc":
                    categorias = categorias.OrderByDescending(s => s.nombre);
                    break;

                case "descripcion_desc":
                    categorias = categorias.OrderByDescending(s => s.descripcion);
                    break;

                case "descripcion_asc":
                    categorias = categorias.OrderBy(s => s.descripcion);
                    break;

                default:
                    categorias = categorias.OrderBy(s => s.nombre);
                    break;
            }

            return View(await categorias.AsNoTracking().ToListAsync());
            //return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categorias/Details/5
        //muestra el detalle de un registro seleccionado
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.categoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        //muestra la vista para crear
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //este metodo almacena lo que recibe el formulario de la vista create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoriaId,nombre,descripcion,estado")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoriaId,nombre,descripcion,estado")] Categoria categoria)
        {
            if (id != categoria.categoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.categoriaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.categoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.categoriaId == id);
        }
    }
}
