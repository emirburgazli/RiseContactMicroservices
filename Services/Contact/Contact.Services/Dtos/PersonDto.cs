using Contact.Services.Models;

namespace Contact.Services.Dtos
{
    public class PersonDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public PersonContactInfo personContactInfo { get; set; }
    }
}
