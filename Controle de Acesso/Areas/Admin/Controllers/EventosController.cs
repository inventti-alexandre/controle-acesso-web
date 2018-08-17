using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleAcesso.Data;
using ControleAcesso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Controle_de_Acesso.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public EventosController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // GET: Admin/Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .SingleOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Admin/Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoID,Nome,DataInicio,DataFim,Descricao")] Evento evento, IFormFile Logo, IFormFile Banner)
        {
            if (ModelState.IsValid)
            {
                Guid guid;

                guid = Guid.NewGuid();

                if (Logo.Length > 0)
                {
                    var nameLogo = "evento-logo-" + guid + Path.GetExtension(Logo.FileName);
                    var fileLogo = Path.Combine(_env.WebRootPath + "/arquivos/evento", nameLogo);

                    using (var stream = new FileStream(fileLogo, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    evento.Logo = nameLogo;
                }

                if (Banner.Length > 0)
                {
                    var nameBanner = "evento-banner-" + guid + Path.GetExtension(Banner.FileName);
                    var fileBanner = Path.Combine(_env.WebRootPath + "/arquivos/evento", nameBanner);

                    using (var stream = new FileStream(fileBanner, FileMode.Create))
                    {
                        await Banner.CopyToAsync(stream);
                    }
                    evento.Banner = nameBanner;
                }


                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Admin/Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.SingleOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Admin/Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoID,Nome,DataInicio,DataFim,Descricao,Logo,Banner")] Evento evento, IFormFile LogoAlt, IFormFile BannerAlt)
        {
            if (id != evento.EventoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid guid;
                    guid = Guid.NewGuid();

                    if (LogoAlt != null)
                        if (LogoAlt.Length > 0)
                        {
                            var nameLogo = "evento-logo-" + guid + Path.GetExtension(LogoAlt.FileName);
                            var fileLogo = Path.Combine(_env.WebRootPath + "/arquivos/evento", nameLogo);

                            using (var stream = new FileStream(fileLogo, FileMode.Create))
                            {
                                await LogoAlt.CopyToAsync(stream);
                            }
                            evento.Logo = nameLogo;
                        }

                    if (BannerAlt != null)
                        if (BannerAlt.Length > 0)
                        {
                            var nameBanner = "evento-banner-" + guid + Path.GetExtension(BannerAlt.FileName);
                            var fileBanner = Path.Combine(_env.WebRootPath + "/arquivos/evento", nameBanner);

                            using (var stream = new FileStream(fileBanner, FileMode.Create))
                            {
                                await BannerAlt.CopyToAsync(stream);
                            }
                            evento.Banner = nameBanner;
                        }

                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoID))
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
            return View(evento);
        }

        // GET: Admin/Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .SingleOrDefaultAsync(m => m.EventoID == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Admin/Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.SingleOrDefaultAsync(m => m.EventoID == id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoID == id);
        }


        // GET: Admin/Eventos
        public async Task<object> Cursos(int id)
        {
            List<Curso> tmp = new List<Curso>();
            var cursos = await _context.Cursos.Where(x => x.EventoID == id).ToListAsync();

            foreach (var curso in cursos)
            {
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
                tmp.Add(curso);
            }
            return new { draw = 1, recordsTotal = 10, recordsFiltered = 10, data = tmp };
        }
    }
}
