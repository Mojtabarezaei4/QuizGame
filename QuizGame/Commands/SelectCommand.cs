using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;

namespace QuizGame.Commands;

public class SelectCommand: CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;
    private readonly GameViewModel _gameViewModel;

    public SelectCommand(QuizManager quizManager, GameViewModel gameViewModel, NavigationService navigationService)
    {
        _navigationService = navigationService;
        _quizManager = quizManager;
        _gameViewModel = gameViewModel;
    }

    private bool IsCorrectAnswer(string answer)
    {
        bool result = false;
        if (_gameViewModel.CurrentQuestion.Answers[0] != answer) return result;
        result = true;
        _quizManager.RightAnswers++;
        return result ;
    }

    public override void Execute(object? parameter)
    {
        IsCorrectAnswer(parameter as string);
        
        _navigationService.Navigate();
    }
}