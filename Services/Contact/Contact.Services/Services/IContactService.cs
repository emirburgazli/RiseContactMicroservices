using Contact.Services.Dtos;
using Contact.Services.Models;
using Contact.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
    interface IContactService
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto person);
        Task<Response<PersonDto>> GetByIdAsync(int Id);
        Task<Response<NoContent>> UpdatePersonAsync(PersonUpdateDto personUpdateDto);
        Task<Response<NoContent>> DeletePersonAsync(int Id);

    }
}
