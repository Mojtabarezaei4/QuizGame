using QuizGame.Services;

namespace QuizGame.Commands;

public class QuestionsCommand: CommandBase
{
    private readonly NavigationService _navigationService;

    public QuestionsCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }

}