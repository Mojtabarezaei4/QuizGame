using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace QuizGame.DataModels;

public class Quiz
{
    private IEnumerable<Question> _questions;
    private string _title = string.Empty;
    private List<Genre> _genres;
    
    public string Title => _title;
    public List<Genre> Genres => _genres;
    public string? ImageSource { get; set; }

    [JsonInclude]
    public IEnumerable<Question> Questions
    {
        get => _questions;
        init => _questions = value;
    }

    private static List<int> usedIndexes = new List<int>();
    public static bool NoQuestionsLeft = false;

    public Quiz(string title, string? imageSource, List<Genre> genres)
    {
        _title = title;
        _genres = genres;
        ImageSource = string.IsNullOrEmpty(imageSource) ? "https://img.icons8.com/ios/100/null/no-image.png" : imageSource;
        _questions = new List<Question>();
    }

    public Question GetRandomQuestion()
    {
        var randomIndex = 0;

        while (_questions.Count() != usedIndexes.Count())
        {
            randomIndex = new Random().Next(0, _questions.Count());
            if (!usedIndexes.Contains(randomIndex))
            {
                usedIndexes.Add(randomIndex);
                break;
            }
        }

        if (_questions.Count() == usedIndexes.Count())
        {
            NoQuestionsLeft = true;
        }
        return _questions.ElementAt(randomIndex);
    }

    public void AddQuestion(string statement, int correctAnswer, string imageSource, params string[] answers)
    {
        List<Question> temp = _questions.ToList();
        temp.Add(new Question(statement, imageSource, answers, 0));
        _questions = temp;
    }

    public void RemoveQuestion(int index)
    {
        List<Question> temp = _questions.ToList();
        temp.Remove(_questions.ElementAt(index));
        _questions = temp;
    }

    public void MixQuestions(IEnumerable<Question> MixedQuestions)
    {
        _questions = MixedQuestions;
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