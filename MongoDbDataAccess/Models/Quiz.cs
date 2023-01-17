using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDataAccess.Models;

public class Quiz
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public List<string> Genres { get; set; }
    public string? ImageSource { get; set; }
    public IEnumerable<Question> Questions { get; set; }
    
    private static List<int> _usedIndexes = new List<int>();
    [BsonIgnore]
    public static bool NoQuestionsLeft = false;

    public Quiz(string title, string? imageSource, List<string> genres)
    {
        Title = title;
        Genres = genres;
        ImageSource = string.IsNullOrEmpty(imageSource) ? "https://img.icons8.com/ios/100/null/no-image.png" : imageSource;
        Questions = new List<Question>();
    }

    public Question GetRandomQuestion()
    {
        var randomIndex = 0;

        while (Questions.Count() != _usedIndexes.Count())
        {
            randomIndex = new Random().Next(0, Questions.Count());
            if (!_usedIndexes.Contains(randomIndex))
            {
                _usedIndexes.Add(randomIndex);
                break;
            }
        }

        if (Questions.Count() == _usedIndexes.Count())
        {
            NoQuestionsLeft = true;
        }
        return Questions.ElementAt(randomIndex);
    }

    public Question AddQuestion(string statement, int correctAnswer, string imageSource, List<string> genres, params string[] answers)
    {
        List<Question> temp = Questions.ToList();
        var question = new Question(statement, imageSource, answers, 0, genres);
        temp.Add(question);
        Questions = temp;

        return question;
    }
    public Question AddQuestion(Question question)
    {
        List<Question> temp = Questions.ToList();
        temp.Add(question);
        Questions = temp;

        return question;
    }


    public void RemoveQuestion(int index)
    {
        List<Question> temp = Questions.ToList();
        temp.Remove(Questions.ElementAt(index));
        Questions = temp;
    }

    public void MixQuestions(IEnumerable<Question> mixedQuestions)
    {
        Questions = mixedQuestions;
    }

    public static void ResetTheUsedIndex()
    {
        _usedIndexes = new List<int>();
    }
    public static void ResetNoQuestionsLeft()
    {
        NoQuestionsLeft = false;
    }
}