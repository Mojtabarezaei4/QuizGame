using QuizGame.Services;

namespace QuizGame.Commands;

public class CancelCommand : CommandBase
{
    private readonly NavigationService _navigationService;

    public CancelCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}