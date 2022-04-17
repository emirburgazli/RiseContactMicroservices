using Contact.Services.Dtos;
using Contact.Services.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Report.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IContactService _contactService;

        public ReportService(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<MemoryStream> GetExcelReporting(string location)
        {
            var contactList = _contactService.GetExportReportDataByLocation(location);
            //gRPC ile contact mikroservisine istek atılıp gelen datayı  
            // var contactList = GetAllAsyc() çağırılmalıdır.

            var stream = new MemoryStream();

            using (var xlPackage = new ExcelPackage(stream))
            {
                // Define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("ContactReport");

                // Styling
                var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");
                customStyle.Style.Font.UnderLine = true;
                customStyle.Style.Font.Color.SetColor(Color.Red);

                // First row
                var startRow = 5;
                var row = startRow;

                worksheet.Cells["A1"].Value = "Sample Contact Export";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.Green);
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Name";
                worksheet.Cells["B4"].Value = "Surname";
                worksheet.Cells["C4"].Value = "Email";
                worksheet.Cells["D4"].Value = "PhoneNumber";
                worksheet.Cells["A4:D4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A4:D4"].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                row = 5;
                foreach (var contact in await contactList)
                {
                    worksheet.Cells[row, 1].Value = contact.Name;
                    worksheet.Cells[row, 2].Value = contact.Surname;
                    worksheet.Cells[row, 3].Value = contact.personContactInfo.Email;
                    worksheet.Cells[row, 4].Value = contact.personContactInfo.PhoneNumber;

                    row++; // row = row + 1;
                }

                xlPackage.Workbook.Properties.Title = "Contact list";
                xlPackage.Workbook.Properties.Author = "Emir";

                xlPackage.Save();
            }

            stream.Position = 0;

            return stream;
        }
    }
}