using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;
using Genre = QuizGame.DataModels.Genre;

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

    public List<Genre> Genres => Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();

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

    public ICommand CancelCommand { get; }
    public ICommand AddQuestionCommand { get; }

    #endregion

    public MakeANewQuizViewModel(QuizManager quizManager, NavigationService navigateToHome, NavigationService navigateToAddQuestion)
    {
        _quizManager = quizManager;

        CancelCommand = new CancelCommand(navigateToHome);
        AddQuestionCommand = new AddQuestionCommand(_quizManager, this, navigateToAddQuestion);
    }
}