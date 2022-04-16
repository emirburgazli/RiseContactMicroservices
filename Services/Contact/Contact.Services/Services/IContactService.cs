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

        Task<Response<PersonDto>> CreateAsync(Person person);

        Task<Response<PersonDto>> GetByIdAsync(int Id);
    }
}
