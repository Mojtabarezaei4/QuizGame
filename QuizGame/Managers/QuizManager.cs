using System.Collections.Generic;
using System.Linq;
using MongoDbDataAccess.DataAccess;
using MongoDbDataAccess.Models;

namespace QuizGame.Managers;

public class QuizManager
{
    private readonly QuizDataAccess _quizDataAccess;
    public Quiz CurrentQuiz { get; set; }
    public Question CurrentQuestion { get; set; }

    public List<Quiz> Quizzes = new List<Quiz>();
    public List<Genre> currentQuizGenres { get; set; }
    public List<Genre> Genres { get; set; }

    public int AskedQuestions = 0;
    public int RightAnswers = 0;

    public QuizManager(QuizDataAccess quizDataAccess)
    {
        _quizDataAccess = quizDataAccess;

        LoadQuizzes();
    }

    public void ResetStates()
    {
        AskedQuestions = 0;
        RightAnswers = 0;
    }

    private async void LoadQuizzes()
    {
        Quizzes = await _quizDataAccess.GetAllQuizzes();
        foreach (var q in Quizzes.Where(q => !q.Questions.Any()).ToList())
        {
            Quizzes.Remove(q);
            await _quizDataAccess.DeleteAQuiz(q);
        }
        
        Genres = await _quizDataAccess.GetAllGenres();
    }

    public void SaveAQuiz()
    {
        Quizzes.Add(CurrentQuiz);

        if (Quizzes.Any())
        {
            foreach (var quiz in Quizzes)
            {
                _quizDataAccess.CreateAQuiz(quiz);
                //UpdateQuiz(quiz);
            }
        }
    }

    public void SaveAQuestion(Question question)
    {
        _quizDataAccess.CreateAQuestion(question);
    }

    public void UpdateQuestion(Question question)
    {
        _quizDataAccess.UpdateAQuestion(question);
    }

    public void UpdateQuiz(Quiz quiz)
    {
        _quizDataAccess.UpdateAQuiz(quiz);
    }
    public void DeleteQuestion(Question question)
    {
        _quizDataAccess.DeleteAQuestion(question);
    }
}