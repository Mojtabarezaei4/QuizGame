using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDbDataAccess.Models;

public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Statement { get; set; }
    public string? ImageSource { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; } = 0;
    public List<string> Genres { get; set; }

    public Question(string statement, string? imageSource, string[] answers, int correctAnswer, List<string> genres)
    {
        Statement = statement;
        ImageSource = string.IsNullOrEmpty(imageSource) ? "https://img.icons8.com/ios/100/null/no-image.png" : imageSource;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        Genres = genres;
    }
}