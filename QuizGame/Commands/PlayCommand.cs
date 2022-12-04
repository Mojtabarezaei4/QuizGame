using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;

namespace QuizGame.Commands;

public class PlayCommand: CommandBase
{
    private readonly HomeViewModel _homeViewModel;
    private readonly NavigationService _navigationService;

    public PlayCommand(HomeViewModel homeViewModel, NavigationService navigationService)
    {
        _homeViewModel = homeViewModel;
        _navigationService = navigationService;

        _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(HomeViewModel.SelectedQuizIndex))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return (_homeViewModel.SelectedQuizIndex != null) &&
               base.CanExecute(parameter);
    }
    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}