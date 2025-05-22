using System;
using System.Globalization;
using BitirmeProjesi_ll.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi_ll.Controllers
{
    public class DefaultController : Controller
    {
        private readonly BitProjContext _context;

        public DefaultController(BitProjContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            DateTime birYilOnce = DateTime.Today.AddYears(-1);
            decimal toplamYillikHarcama = _context.Harcamalars
            .Where(h => h.Date >= birYilOnce)
            .ToList()
            .Sum(h => decimal.TryParse(h.Price, out var val) ? val : 0);

            ViewBag.YillikToplam = toplamYillikHarcama;

            decimal ParsePrice(string price)
            {
                decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
                return result;
            }
            var harcamalar = _context.Harcamalars.ToList();
            var borclar = _context.Borclar.ToList();
            var faturalar = _context.Faturalar.ToList();
            ViewBag.ToplamGider = harcamalar.Sum(x => ParsePrice(x.Price)) +
                                  borclar.Where(b => !b.Status).Sum(b => ParsePrice(b.Price)) +
                                  faturalar.Where(f => !f.Status).Sum(f => ParsePrice(f.Price));
            ViewBag.GunlukHarcama = harcamalar
                .Where(h => h.Date.Date == DateTime.Now.Date)
                .Sum(h => ParsePrice(h.Price));
            ViewBag.AylikHarcama = harcamalar
                .Where(h => h.Date.Month == DateTime.Now.Month && h.Date.Year == DateTime.Now.Year)
                .Sum(h => ParsePrice(h.Price));
            ViewBag.FaturaAdet = faturalar.Count;
            var toplamBorc = _context.Borclar
            .ToList()
            .Sum(b => decimal.TryParse(b.Price, out var val) ? val : 0);

            ViewBag.ToplamBorc = toplamBorc;

            var now = DateTime.Now;
            var aylikData = new List<decimal>();
            var aylikLabels = new List<string>();
            for (int i = 3; i >= 0; i--)
            {
                var date = now.AddMonths(-i);
                var total = harcamalar
                .Where(h => h.Date.Month == date.Month && h.Date.Year == date.Year)
                .Sum(h => ParsePrice(h.Price));
                aylikData.Add(total);
                aylikLabels.Add(date.ToString("MMMM", new CultureInfo("tr-TR")));
            }
            ViewBag.AylikChartLabels = aylikLabels;
            ViewBag.AylikChartData = aylikData;

            var yillikLabels = new List<string>();
            var yillikData = new List<decimal>();

            for (int year = 2019; year <= 2025; year++)
            {
                var total = harcamalar
                .Where(h => h.Date.Year == year)
                .Sum(h => ParsePrice(h.Price));
                yillikLabels.Add(year.ToString());
                yillikData.Add(total);
            }
            ViewBag.YillikChartLabels = yillikLabels;
            ViewBag.YillikChartData = yillikData;

            var haftalikLabels = new List<string>();
            var haftalikData = new List<decimal>();

            for (int i = 6; i >= 0; i--)
            {
                var date = DateTime.Now.Date.AddDays(-i);
                var total = harcamalar
                .Where(h => h.Date.Date == date)
                .Sum(h => ParsePrice(h.Price));
                haftalikLabels.Add(date.ToString("dd MMM", new CultureInfo("tr-TR"))); 
                haftalikData.Add(total);
            }

            ViewBag.HaftalikChartLabels = haftalikLabels;
            ViewBag.HaftalikChartData = haftalikData;

            return View();
        }
    }
}
