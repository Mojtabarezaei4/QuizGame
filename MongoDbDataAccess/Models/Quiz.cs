using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDataAccess.Models;

public class Quiz
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; }
    public string? ImageSource { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    
    private static List<int> usedIndexes = new List<int>();
    [BsonIgnore]
    public static bool NoQuestionsLeft = false;

    public Quiz(string title, string? imageSource, List<Genre> genres)
    {
        Title = title;
        Genres = genres;
        ImageSource = string.IsNullOrEmpty(imageSource) ? "https://img.icons8.com/ios/100/null/no-image.png" : imageSource;
        Questions = new List<Question>();
    }

    public Question GetRandomQuestion()
    {
        var randomIndex = 0;

        while (Questions.Count() != usedIndexes.Count())
        {
            randomIndex = new Random().Next(0, Questions.Count());
            if (!usedIndexes.Contains(randomIndex))
            {
                usedIndexes.Add(randomIndex);
                break;
            }
        }

        if (Questions.Count() == usedIndexes.Count())
        {
            NoQuestionsLeft = true;
        }
        return Questions.ElementAt(randomIndex);
    }

    public void AddQuestion(string statement, int correctAnswer, string imageSource, params string[] answers)
    {
        List<Question> temp = Questions.ToList();
        temp.Add(new Question(statement, imageSource, answers, 0));
        Questions = temp;
    }

    public void RemoveQuestion(int index)
    {
        List<Question> temp = Questions.ToList();
        temp.Remove(Questions.ElementAt(index));
        Questions = temp;
    }

    public void MixQuestions(IEnumerable<Question> MixedQuestions)
    {
        Questions = MixedQuestions;
    }

    public static void ResetTheUsedIndex()
    {
        usedIndexes = new List<int>();
    }
    public static void ResetNoQuestionsLeft()
    {
        NoQuestionsLeft = false;
    }
}