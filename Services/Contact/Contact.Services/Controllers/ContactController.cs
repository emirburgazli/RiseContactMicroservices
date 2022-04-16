using Contact.Services.Services;
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

        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _contactService.GetByIdAsync(Id);
            return CreateActi onResultInstance(response);
        }
    }
}
