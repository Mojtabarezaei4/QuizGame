using QuizGame.Services;

namespace QuizGame.Commands;

public class MixAQuizCommand: CommandBase
{
    private readonly NavigationService _navigationService;

    public MixAQuizCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}