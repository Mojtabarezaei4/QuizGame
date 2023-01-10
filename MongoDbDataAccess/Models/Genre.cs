using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDataAccess.Models;

public class Genre
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
}