using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuizGame.DataModels;
using QuizGame.Services;

namespace QuizGame.Managers;

public class QuizManager
{
    private readonly string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\quizzes";

    public Quiz CurrentQuiz { get; set; }
    public Question CurrentQuestion { get; set; }

    public List<Quiz> Quizzes = new List<Quiz>();

    private readonly ReadFromJSONService _readFromJsonService = new ReadFromJSONService();
    private readonly WriteToJSONService _writeToJsonService = new WriteToJSONService();

    public int AskedQuestions = 0;
    public int RightAnswers = 0;

    public QuizManager()
    {
        LoadQuizzes();

    }

    public void ResetStates()
    {
        AskedQuestions = 0;
        RightAnswers = 0;
    }

    private void LoadQuizzes()
    {
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
        else
        {
            var files = Directory.GetFiles(_filePath);

            foreach (var file in files)
            {
                var openedFile = _readFromJsonService.LoadQuizFromJsonAsync(file);

                if (openedFile.Result != null && !openedFile.Result.Questions.Any()) { File.Delete(file); }

                else
                {
                    if (openedFile.Result != null) Quizzes.Add(openedFile.Result);
                }
            }
        }
    }

    public void SaveAQuiz()
    {
        Quizzes.Add(CurrentQuiz);

        if (Quizzes != null)
        {
            foreach (Quiz quiz in Quizzes)
            {
                _writeToJsonService.SaveQuizToJsonAsync(quiz);
            }
        }
    }
}