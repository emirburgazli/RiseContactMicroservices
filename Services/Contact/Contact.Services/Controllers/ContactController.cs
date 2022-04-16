using Contact.Services.Dtos;
using Contact.Services.Services;
using Contact.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contact.Services.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : CustomBaseController
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactService.GetAllAsync();
          
            return CreateActionResultInstance(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string  Id)
        {
            var response = await _contactService.DeletePersonAsync(Id);
            return CreateActionResultInstance(response);
        }


    }
}
