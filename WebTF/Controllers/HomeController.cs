using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Modelirovanie.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace Modelirovanie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Calc(int num1, int num2, int operationType)
        //{
        //    var result = operationType switch
        //    {
        //        1 => num1 + num2,
        //        2 => num1 - num2,
        //        3 => num1 * num2,
        //        4 => num1 / num2,
        //        _ => throw new Exception()
        //    };
        //    ViewData["result"] = result;
        //    return View();
        //}

        //public IActionResult Model(int av, double H0, double wg, double Cg)
        //{
        //    var result = new List<double>();
        //    for (double h = 0; h <= H0; h += 0.5)
        //    {
        //        result.Add(av * h / (wg * Cg) / 1000);
        //    }
        //    ViewBag.Result = result;
        //    return View();
        //}
    

    public IActionResult Model(int av, double H0, double wg, double Cg, double Gm, double Cm, int t, int T, int d)
        {
            var results = new List<Dictionary<string, double>>();
            var f1 = new List<double>();

            var S = Math.PI * Math.Pow(d / 2, 2);

            for (double y = 0; y <= H0; y += 0.5)
            {
                var result = new Dictionary<string, double>();
                var m = Gm * Cm / (wg * S * Cg);

                var Y0 = av * H0 / (wg  * Cg);

                var otn_vis = 1 - m * Math.Exp(-(1 - m) * Y0 / m);

                //Формула 1
                var Y = av * y / (wg * Cg) / 1000;
                f1.Add(Y);

                //Формула 2

                var f2 = 1 - Math.Exp((m - 1) * Y / m);

                //Формула 3

                var f3 = 1 - m * Math.Exp((m - 1) * Y / m);

                //Формула 4

                var f4 = f2 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                //Формула 5

                var f5 = f3 / (1 - m * Math.Exp((m - 1) * Y0 / m));

                //Формула 6

                var f6 = t + (T - t) * f4;

                //Формула 7

                var f7 = t + (T - t) * f5;

                //Формула 8

                var f8 = f6 - f7;
            }
            ViewBag.Result = f1;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
