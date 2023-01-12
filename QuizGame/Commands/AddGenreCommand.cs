using QuizGame.Managers;
using QuizGame.ViewModels;
using System.ComponentModel;
using QuizGame.Services;

namespace QuizGame.Commands;

public class AddGenreCommand : CommandBase
{
    private readonly QuizManager _quizManager;
    private readonly MakeANewQuizViewModel _makeANewQuizViewModel;
    private readonly NavigationService _navigationService;

    public AddGenreCommand(QuizManager quizManager, MakeANewQuizViewModel makeANewQuizViewModel, NavigationService navigationService)
    {
        _quizManager = quizManager;
        _makeANewQuizViewModel = makeANewQuizViewModel;
        _navigationService = navigationService;

        _makeANewQuizViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }
    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeANewQuizViewModel.NewGenre))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_makeANewQuizViewModel.NewGenre) &&
               base.CanExecute(parameter);
    }
    
    private void CreateGenre()
    {
        if (!string.IsNullOrEmpty(_makeANewQuizViewModel.NewGenre))
        {
            _quizManager.SaveAGenre(_makeANewQuizViewModel.NewGenre!);
        }
    }
    public override void Execute(object? parameter)
    {
        CreateGenre();
        _navigationService.Navigate();
    }

    
}