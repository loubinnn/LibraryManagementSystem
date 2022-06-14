using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

namespace LibraryWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LendingTicketsController : Controller
    {
        private readonly libraryContext _context;

        public LendingTicketsController(libraryContext context)
        {
            _context = context;
        }

        // GET: Admin/LendingTickets
        public async Task<IActionResult> Index(string Keyword)
        {
            string abc = Keyword;
            ViewBag.Keyword = abc;
            if (!string.IsNullOrEmpty(Keyword))
            return View(await _context.LendingTickets.Where(t => t.BookId.Contains(abc)).ToListAsync());
            var libraryContext = _context.LendingTickets.Include(l => l.Book).Include(l => l.StatusNavigation).Include(l => l.Student);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Admin/LendingTickets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendingTicket = await _context.LendingTickets
                .Include(l => l.Book)
                .Include(l => l.StatusNavigation)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.LendingTicketId == id);
            if (lendingTicket == null)
            {
                return NotFound();
            }

            return View(lendingTicket);
        }

        // GET: Admin/LendingTickets/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["Status"] = new SelectList(_context.Statuses, "Status1", "Status1");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Admin/LendingTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LendingTicketId,StudentId,BorrowedDate,ReturnedDate,Status,BookId")] LendingTicket lendingTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lendingTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendingTicket.BookId);
            ViewData["Status"] = new SelectList(_context.Statuses, "Status1", "Status1", lendingTicket.Status);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", lendingTicket.StudentId);
            return View(lendingTicket);
        }

        // GET: Admin/LendingTickets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendingTicket = await _context.LendingTickets.FindAsync(id);
            if (lendingTicket == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendingTicket.BookId);
            ViewData["Status"] = new SelectList(_context.Statuses, "Status1", "Status1", lendingTicket.Status);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", lendingTicket.StudentId);
            return View(lendingTicket);
        }

        // POST: Admin/LendingTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LendingTicketId,StudentId,BorrowedDate,ReturnedDate,Status,BookId")] LendingTicket lendingTicket)
        {
            if (id != lendingTicket.LendingTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lendingTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendingTicketExists(lendingTicket.LendingTicketId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendingTicket.BookId);
            ViewData["Status"] = new SelectList(_context.Statuses, "Status1", "Status1", lendingTicket.Status);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", lendingTicket.StudentId);
            return View(lendingTicket);
        }

        // GET: Admin/LendingTickets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendingTicket = await _context.LendingTickets
                .Include(l => l.Book)
                .Include(l => l.StatusNavigation)
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.LendingTicketId == id);
            if (lendingTicket == null)
            {
                return NotFound();
            }

            return View(lendingTicket);
        }

        // POST: Admin/LendingTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lendingTicket = await _context.LendingTickets.FindAsync(id);
            _context.LendingTickets.Remove(lendingTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendingTicketExists(string id)
        {
            return _context.LendingTickets.Any(e => e.LendingTicketId == id);
        }
    }
}
