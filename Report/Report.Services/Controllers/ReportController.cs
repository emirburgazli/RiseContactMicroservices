using Microsoft.AspNetCore.Mvc;
using Report.Services.Models;
using Report.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Services.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly Context _context;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("/api/[controller]/GetExcel/{location}")]
        public async Task<IActionResult> GetExcel(string location)
        {
            var stream = await _reportService.GetExcelReporting(location);
            Reports reports = new Reports()
            {
                CreatedDate = DateTime.Now,
                State = "Oluşturuldu."
            };

            await _context.Reports.AddAsync(reports);
            await _context.SaveChangesAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "contactReport.xlsx");
        }
    }
}
