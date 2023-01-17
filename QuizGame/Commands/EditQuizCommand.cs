using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;
using QuizGame.Managers;

namespace QuizGame.Commands;

public class EditQuizCommand : CommandBase
{
    private readonly QuizManager _quizManager;
    private readonly HomeViewModel _homeViewModel;
    private readonly NavigationService _navigationService;

    public EditQuizCommand(QuizManager quizManager ,HomeViewModel homeViewModel, NavigationService navigationService)
    {
        _quizManager = quizManager;
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

        _quizManager.CurrentQuizGenres = _quizManager.Quizzes[int.Parse(_homeViewModel.SelectedQuizIndex)].Genres;
        _navigationService.Navigate();
    }
}