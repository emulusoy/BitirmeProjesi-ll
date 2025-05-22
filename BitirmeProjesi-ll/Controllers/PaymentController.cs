using BitirmeProjesi_ll.Context;
using BitirmeProjesi_ll.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        public async Task<IActionResult> Index(string sortOrder, int page = 1)
        {
            ViewBag.ActivePage = "Harcamalar";
            int pageSize = 15;
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date" :
                                    sortOrder == "date" ? "date_desc" : "date";

            var harcamalar = _context.Harcamalars.AsQueryable();

            switch (sortOrder)
            {
                case "date":
                    harcamalar = harcamalar.OrderBy(h => h.Date);
                    break;
                case "date_desc":
                    harcamalar = harcamalar.OrderByDescending(h => h.Date);
                    break;
                default:
                    harcamalar = harcamalar.OrderBy(h => h.HarcamalarId);
                    break;
            }

            var totalItems = await harcamalar.CountAsync();

            var items = await harcamalar
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(items);
        }
        [HttpGet]
        public IActionResult CreatePayment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePayment(Harcamalar harcama)
        {
            _context.Harcamalars.Add(harcama);
            _context.SaveChangesAsync();
            return RedirectToAction("Index", "Payment");
        }
    }
}
