using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository
{
    public class PositionInTime
    {
        public ObjectId Id { get; set; }
        public Guid Number { get; set; }
        public DateTime When { get; set; }
        public string Position { get; set; }
    }
}