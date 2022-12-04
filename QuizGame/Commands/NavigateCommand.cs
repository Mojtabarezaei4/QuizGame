using System;
using QuizGame.Services;
using QuizGame.ViewModels;

namespace QuizGame.Commands;

public class NavigateCommand: CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly Func<ViewModelBase> _createViewModel;

    public NavigateCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}