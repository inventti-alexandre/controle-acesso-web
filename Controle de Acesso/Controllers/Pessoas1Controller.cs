using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleAcesso.Data;
using ControleAcesso.Models;

namespace Controle_de_Acesso.Controllers
{
    public class Pessoas1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Pessoas1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pessoas1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pessoas.Include(p => p.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pessoas1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(p => p.Status)
                .SingleOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas1/Create
        public IActionResult Create()
        {
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID");
            return View();
        }

        // POST: Pessoas1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaID,CPF,Nome,NomeCracha,CNPJ,Empresa,Cargo,CEP,Endereco,Numero,Complemento,Bairro,Cidade,UF,Telefone,Email,StatusID")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", pessoa.StatusID);
            return View(pessoa);
        }

        // GET: Pessoas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.SingleOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", pessoa.StatusID);
            return View(pessoa);
        }

        // POST: Pessoas1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaID,CPF,Nome,NomeCracha,CNPJ,Empresa,Cargo,CEP,Endereco,Numero,Complemento,Bairro,Cidade,UF,Telefone,Email,StatusID")] Pessoa pessoa)
        {
            if (id != pessoa.PessoaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaID))
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
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusID", pessoa.StatusID);
            return View(pessoa);
        }

        // GET: Pessoas1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(p => p.Status)
                .SingleOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.SingleOrDefaultAsync(m => m.PessoaID == id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.PessoaID == id);
        }
    }
}
