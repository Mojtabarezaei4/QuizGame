using QuizGame.Services;

namespace QuizGame.Commands;

public class MakeANewQuizCommand: CommandBase
{
    private readonly NavigationService _navigationService;

    public MakeANewQuizCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}