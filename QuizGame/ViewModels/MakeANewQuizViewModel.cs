using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class MakeANewQuizViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;
    public Quiz CurrentQuiz { get; set; }

    #region BindingProps

    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    private string _newGenre;
    public string NewGenre
    {
        get => _newGenre;
        set
        {
            _newGenre = value;
            OnPropertyChanged(nameof(NewGenre));
        }
    }
    
    public ObservableCollection<Genre> Genres { get; }

    private string? _imageSource;

    public string? ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    #endregion

    #region Commands

    public ICommand AddGenreCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddQuestionCommand { get; }

    #endregion

    public MakeANewQuizViewModel(QuizManager quizManager, NavigationService navigateToHome,
        NavigationService navigateToAddQuestion, NavigationService navigateToNewQuizViewModel)
    {
        _quizManager = quizManager;
        Genres = new ObservableCollection<Genre>(_quizManager.Genres);

        AddGenreCommand = new AddGenreCommand(_quizManager, this, navigateToNewQuizViewModel);
        CancelCommand = new CancelCommand(navigateToHome);
        AddQuestionCommand = new AddQuestionCommand(_quizManager, this, navigateToAddQuestion);
    }
}