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
    
    public ObservableCollection<String> Genres { get; }

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
        var temp = new List<String>();
        foreach (var genre in _quizManager.Genres)
        {
            temp.Add(genre.Name);
        }
        Genres = new ObservableCollection<string>(temp);

        AddGenreCommand = new AddGenreCommand(_quizManager, this, navigateToNewQuizViewModel);
        CancelCommand = new CancelCommand(navigateToHome, _quizManager);
        AddQuestionCommand = new AddQuestionCommand(_quizManager, this, navigateToAddQuestion);
    }
}