using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MongoDbDataAccess.Models;
using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;

namespace QuizGame.Commands;

public class MixCommand : CommandBase
{
    private readonly MixQuizViewModel _mixQuizViewModel;
    private readonly QuizManager _quizManager;
    private readonly NavigationService _navigationService;

    private readonly List<Question> _mixedQuestions = new List<Question>();

    public MixCommand(QuizManager quizManager, MixQuizViewModel mixQuizViewModel, NavigationService navigationService)
    {
        _mixQuizViewModel = mixQuizViewModel;
        _quizManager = quizManager;

        _navigationService = navigationService;


        _mixQuizViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (CanExecute(sender as IList))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return (parameter != null) &&
               base.CanExecute(parameter);
    }

    private bool MakeAMixQuiz(IList? genres)
    {
        var genreExist = false;
        var foundGenres = string.Empty;
        var missingGenre = string.Empty;
        var tempGenres = new List<string>();

        if (genres != null && genres.Count > 0)
        {
            foreach (var genre in genres.Cast<Genre>().ToList())
            {
                var matchedQuizzes = _quizManager.Quizzes.Where(q => q.Genres.
                    Any(g => g.Equals(genre.Name))).ToList();

                foreach (var matchedQuiz in matchedQuizzes)
                {
                    var questions = matchedQuiz.Questions.ToList();
                    
                    _mixedQuestions.AddRange(questions);
                    _mixedQuestions.Concat(questions);
                    genreExist = true;
                }

                foundGenres += !string.IsNullOrEmpty(foundGenres) ? ", " + genre.ToString() : genre.ToString();

                tempGenres.Add(genre.Name);

                if (!genreExist)
                {
                    if (!missingGenre.Contains(genre.ToString()))
                    {
                        missingGenre += genres[^1].Equals(genre) ? genre.ToString() : genre.ToString() + ", ";
                    }
                }
                genreExist = false;
            }

            var mixedQuiz = new Quiz(($"Mix of: {foundGenres}"), null, tempGenres);
            mixedQuiz.MixQuestions(_mixedQuestions);
            if (mixedQuiz.Questions.Any())
            {
                _quizManager.CurrentQuiz = mixedQuiz;
                return true;
            }
            else
            {
                MessageBox.Show($"There is no quiz for {missingGenre}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        MessageBox.Show("You need to choose something.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
    }

    public override void Execute(object? parameter)
    {
        if (MakeAMixQuiz(parameter as IList))
        {
            _navigationService.Navigate();
        }
        return;
    }
}