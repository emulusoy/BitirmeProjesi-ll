using BitirmeProjesi_ll.Context;
using BitirmeProjesi_ll.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi_ll.Controllers
{
    public class DebtController : Controller
    {
        private readonly BitProjContext _context;

        public DebtController(BitProjContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ActivePage = "Borclar";
            var borclar = _context.Borclar.ToList();

            return View(borclar);
        }

        [HttpGet]
        public IActionResult PayDebt(int id)
        {
            var borc = _context.Borclar.FirstOrDefault(x => x.BorcId == id);
            if (borc != null && !borc.Status)
            {
                borc.Status = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateDebt()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDebt(Borc borc)
        {
            _context.Borclar.Add(borc);
            _context.SaveChangesAsync();
            borc.Status = false;
            return RedirectToAction("Index", "Debt");
        }
    }
}
