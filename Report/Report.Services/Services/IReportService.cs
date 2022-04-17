using Contact.Services.Dtos;
using Contact.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Services.Services
{
   public  interface IReportService
    {
        Task<MemoryStream> GetExcelReporting();
    }
}
