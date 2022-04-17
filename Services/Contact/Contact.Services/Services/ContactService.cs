using AutoMapper;
using Contact.Services.Dtos;
using Contact.Services.Models;
using Contact.Services.Settings;
using Contact.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
    public class ContactService : IContactService
    {
        public readonly IMongoCollection<Person> _personCollection;
        public readonly IMongoCollection<PersonContactInfo> _personContactInfoCollection;
        public readonly IMapper _mapper;

        public ContactService(IMapper mapper, IDatabaseSettings databaseSettings)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _personContactInfoCollection = database.GetCollection<PersonContactInfo>(databaseSettings.PersonInfoCollectionName);
            _mapper = mapper;
            _mapper = mapper;
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await _personCollection.Find(person => true).ToListAsync();
            if (!persons.Any())
            {
                persons = new List<Person>();
            }

            foreach (var person in persons)
            {
                person.personContactInfo = await _personContactInfoCollection.Find(x => x.ID == person.ID).FirstOrDefaultAsync();
            }
            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);

        }

        public async Task<Response<PersonDto>> CreateAsync(PersonCreateDto person)
        {
            var newPerson = _mapper.Map<Person>(person);

            await _personCollection.InsertOneAsync(newPerson);
            PersonContactInfo personContactInfo = new PersonContactInfo
            {
                ID = newPerson.ID,
                Email = person.personContactInfo.Email,
                Location = person.personContactInfo.Location,
                PhoneNumber = person.personContactInfo.PhoneNumber
            };
            await _personContactInfoCollection.InsertOneAsync(personContactInfo);
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(newPerson), 200);
        }

        public async Task<Response<PersonDto>> GetByIdAsync(string Id)
        {
            var person = await _personCollection.Find<Person>(x => x.ID == Id).FirstOrDefaultAsync();
            if (person == null)
            {
                return Response<PersonDto>.Fail("Person Not Fount", 404);
            }
            person.personContactInfo = await _personContactInfoCollection.Find<PersonContactInfo>(x => x.ID == person.ID).FirstOrDefaultAsync();
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }

        public async Task<Response<NoContent>> UpdatePersonAsync(PersonUpdateDto personUpdateDto)
        {
            var updatePerson = _mapper.Map<Person>(personUpdateDto);
            var updatePersonInfo = _mapper.Map<PersonContactInfo>(personUpdateDto.personContactInfo);
            var result = await _personCollection.FindOneAndReplaceAsync(x => x.ID == personUpdateDto.ID, updatePerson);
            await _personContactInfoCollection.FindOneAndReplaceAsync(x => x.ID == updatePerson.ID, updatePersonInfo);
            if (result == null)
            {
                return Response<NoContent>.Fail("Person Not Fount", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeletePersonAsync(string Id)
        {
            var deletePerson = await _personCollection.DeleteOneAsync(x => x.ID == Id);

            if (deletePerson.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Person Not Found", 404);
            }
        }


        //grpc ile iki mikroservis arası iletişimi sağlayamadığımdan raporlama işlemini contact mikroservisine taşımış oldum.
        public async Task<List<Person>> GetExportReportDataByLocation(string Location)
        {
            //ilgili şehirde ki contactları listeleyen rapor... !

            var data = _personCollection.Find(x => x.personContactInfo.Location == Location).ToListAsync();
            return await data;

        }
    }
}
