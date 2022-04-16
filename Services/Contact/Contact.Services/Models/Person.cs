using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.Services.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public PersonContactInfo personContactInfo { get; set; }
    }
}
