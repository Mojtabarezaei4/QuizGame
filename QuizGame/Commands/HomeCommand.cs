using QuizGame.DataModels;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.Commands;

public class HomeCommand: CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;

    public HomeCommand(QuizManager quizManager, NavigationService navigationService)
    {
        _navigationService = navigationService;
        _quizManager = quizManager;
    }

    public override void Execute(object? parameter)
    {
        Quiz.ResetTheUsedIndex();
        Quiz.ResetNoQuestionsLeft();
        _quizManager.AskedQuestions = 0;
        _quizManager.RightAnswers = 0;

        _navigationService.Navigate();
    }
}