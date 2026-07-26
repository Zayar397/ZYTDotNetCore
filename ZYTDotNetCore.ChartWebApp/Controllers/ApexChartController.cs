using Microsoft.AspNetCore.Mvc;

namespace ZYTDotNetCore.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
