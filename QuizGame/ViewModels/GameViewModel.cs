using System;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class GameViewModel: ViewModelBase
{
    private readonly QuizManager _quizManager;
    private static int _totalQuestions;

    #region BindingProps

    public Question CurrentQuestion { get; }

    public string Title { get; set; }

    public string Statement { get; set; }

    public string? ImageSource { get; set; }

    public string[] Answers { get; set; }

    public string TotalAmountOfQuestions { get { return _quizManager.AskedQuestions + "/" + _totalQuestions; } }

    public string RightAnswers { get { return "Right: " + _quizManager.RightAnswers; } }

    #endregion

    #region Commands

    public ICommand HomeCommand { get; }
    public ICommand SelectCommand { get; }

    #endregion

    public GameViewModel(QuizManager quizManager, NavigationService navigateHome, NavigationService navigateGameView)
    {
        _quizManager = quizManager;
        _totalQuestions = _quizManager.CurrentQuiz.Questions.Count();
        _quizManager.AskedQuestions++;

        CurrentQuestion = _quizManager.CurrentQuiz.GetRandomQuestion();

        Title = _quizManager.CurrentQuiz.Title;
        Statement = CurrentQuestion.Statement;
        Answers = CurrentQuestion.Answers.OrderBy(a => Guid.NewGuid()).ToArray();
        ImageSource = CurrentQuestion.ImageSource;


        SelectCommand = new SelectCommand(_quizManager,this, navigateGameView);
        HomeCommand = new HomeCommand(_quizManager, navigateHome);
    }
}