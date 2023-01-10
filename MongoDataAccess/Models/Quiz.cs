using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDataAccess.Models;

public class Quiz
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; }
    public string? ImageSource { get; set; }
    public List<Question> Questions { get; set; }
}