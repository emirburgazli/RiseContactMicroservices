using Contact.Services.Dtos;
using Contact.Services.Services;
using Contact.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contact.Services.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    internal class ContactController : CustomBaseController
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        public async Task<IActionResult> GetAll()
        {
            var response = await _contactService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _contactService.GetByIdAsync(Id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            var response = await _contactService.CreateAsync(personCreateDto);
            return CreateActionResultInstance(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(PersonUpdateDto personUpdateDto)
        {
            var response = await _contactService.UpdatePersonAsync(personUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> Delete(int  Id)
        {
            var response = await _contactService.DeletePersonAsync(Id);
            return CreateActionResultInstance(response);
        }


    }
}
