using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository
{
    public class Client
    {
        public ObjectId Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
