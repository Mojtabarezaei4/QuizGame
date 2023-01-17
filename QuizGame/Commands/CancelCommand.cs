using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.Commands;

public class CancelCommand : CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;

    public CancelCommand(NavigationService navigationService, QuizManager quizManager)
    {
        _navigationService = navigationService;
        _quizManager = quizManager;
    }
    
    public override void Execute(object? parameter)
    {
        _quizManager.LoadQuizzes();
        _navigationService.Navigate();
    }
}