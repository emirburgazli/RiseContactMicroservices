using Contact.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Services.Dtos
{
    public class PersonUpdateDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public PersonContactInfo personContactInfo { get; set; }
    }
}
