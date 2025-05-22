using BitirmeProjesi_ll.Context;
using BitirmeProjesi_ll.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi_ll.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly BitProjContext _context;
        public InvoiceController(BitProjContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Faturalar";
            var borclar = _context.Faturalar.ToList();

            return View(borclar);
        }
        [HttpGet]
        public IActionResult PayInvoice(int id)
        {
            var fatura = _context.Faturalar.FirstOrDefault(x => x.FaturaId == id);
            if (fatura != null && !fatura.Status)
            {
                fatura.Status = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateInvoice()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateInvoice(Fatura fatura)
        {
            _context.Faturalar.Add(fatura);
            _context.SaveChangesAsync();
            fatura.Status = false;
            return RedirectToAction("Index", "Invoice");
        }
    }
}
