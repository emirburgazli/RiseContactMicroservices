using AutoMapper;
using Contact.Services.Dtos;
using Contact.Services.Models;

namespace Contact.Services.Mapping
{
    public class GeneralMapping:Profile
    {

        public GeneralMapping()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<PersonContactInfo, PersonContactInfoDto>().ReverseMap();


            CreateMap<Person, PersonCreateDto>().ReverseMap();
            CreateMap<Person, PersonUpdateDto>().ReverseMap();
            
            //CreateMap<PersonCreateDto,Person>().ReverseMap();
            //CreateMap<PersonUpdateDto, Person>().ReverseMap();


        }
    }
}
