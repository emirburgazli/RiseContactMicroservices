using Microsoft.AspNetCore.Mvc;
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

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> GetExcel()
        {
            var stream = await _reportService.GetExcelReporting();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "contactReport.xlsx");
        }
    }
}
