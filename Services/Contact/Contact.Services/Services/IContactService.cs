using Contact.Services.Dtos;
using Contact.Services.Models;
using Contact.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
   public interface IContactService
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto person);
        Task<Response<PersonDto>> GetByIdAsync(string Id);
        Task<Response<NoContent>> UpdatePersonAsync(PersonUpdateDto personUpdateDto);
        Task<Response<NoContent>> DeletePersonAsync(string Id);
        Task<List<Person>> GetExportReportDataByLocation(string Location);

    }
}
