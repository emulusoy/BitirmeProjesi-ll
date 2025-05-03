using BitirmeProjesi_ll.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi_ll.Controllers
{
    public class PaymentController : Controller
    {
        private readonly BitProjContext _context;

        public PaymentController(BitProjContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 25;
            int totalItems = await _context.Harcamalars.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedHarcamalar = await _context.Harcamalars
                .OrderBy(h => h.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;

            return View(pagedHarcamalar);
        }
    }
}
