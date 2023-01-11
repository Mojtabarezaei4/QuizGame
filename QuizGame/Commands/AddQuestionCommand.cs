using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MongoDbDataAccess.Models;
using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;

namespace QuizGame.Commands;

public class AddQuestionCommand: CommandBase
{
    private readonly MakeANewQuizViewModel _makeANewQuizViewModel;
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;

    public AddQuestionCommand(QuizManager quizManager, MakeANewQuizViewModel makeANewQuizViewModel, NavigationService navigationService)
    {
        _makeANewQuizViewModel = makeANewQuizViewModel;
        _navigationService = navigationService;
        _quizManager = quizManager;
        _quizManager.currentQuizGenres = _makeANewQuizViewModel.Genres;

        _makeANewQuizViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeANewQuizViewModel.Title))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_makeANewQuizViewModel.Title) &&
               (parameter != null) &&
               base.CanExecute(parameter);
    }
    
    private bool CanAddNewQuiz(string title, string? imageSource, IList genres)
    {
        if (_quizManager.Quizzes.Any(q => String.Equals(q.Title, _makeANewQuizViewModel.Title, StringComparison.CurrentCultureIgnoreCase)))
        {
            MessageBox.Show("This title is already available, try another.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }
        else
        {
            var temp = genres.Cast<Genre>().ToList();
            _quizManager.CurrentQuiz = new Quiz(title, imageSource, temp);
            return true;
        }
    }

    public override void Execute(object? parameter)
    {
        if (CanAddNewQuiz(_makeANewQuizViewModel.Title, _makeANewQuizViewModel.ImageSource, parameter as IList)) _navigationService.Navigate();
        else
        {
            return;
        }
    }
}