using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository
{
    public class Vehicle
    {
        public ObjectId Id { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }

        public int NumberSeats { get; set; }

        public int NumberDoors { get; set; }
    }
}
