using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Contact.Services.Models
{
    public class PersonContactInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int ID { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

    }

}
