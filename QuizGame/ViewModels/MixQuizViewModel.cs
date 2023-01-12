using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class MixQuizViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;

    #region BindingProps

    public List<Genre> Genres => _quizManager.Genres.ToList();

    #endregion

    #region Commands

    public ICommand CancelCommand { get; }

    public ICommand MixCommand { get; }

    #endregion

    public MixQuizViewModel(QuizManager quizManager, NavigationService navigateHome, NavigationService navigateGameView)
    {
        _quizManager = quizManager;

        MixCommand = new MixCommand(quizManager, this, navigateGameView);
        CancelCommand = new CancelCommand(navigateHome);
    }
}