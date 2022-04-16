using AutoMapper;
using Contact.Services.Dtos;
using Contact.Services.Models;
using Contact.Services.Settings;
using Contact.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
    internal class ContactService:IContactService
    {
        private readonly IMongoCollection<Person> _personCollection;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper,IDatabaseSettings databaseSettings)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Person>(databaseSettings.PersonCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var person = await _personCollection.Find(person => true).ToListAsync();

            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(person), 200);

        }

        public async Task<Response<PersonDto>> CreateAsync(Person person)
        {
            await _personCollection.InsertOneAsync(person);
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }

        public async Task<Response<PersonDto>> GetByIdAsync(int Id)
        {
            var person = await _personCollection.Find<Person>(x=>x.ID==Id).FirstOrDefaultAsync();
            if (person== null )
            {
                return Response<PersonDto>.Fail("Person Not Fount", 404);
            }
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(person), 200);
        }
    }
}
