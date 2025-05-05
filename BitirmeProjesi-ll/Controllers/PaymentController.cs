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
            int pageSize = 15;
            var totalItems = await _context.Harcamalars.CountAsync();

            var items = await _context.Harcamalars
                .OrderBy(h => h.HarcamalarId) // Sıralamayı burada sağlıyoruz
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(items);
        }
    }
}
