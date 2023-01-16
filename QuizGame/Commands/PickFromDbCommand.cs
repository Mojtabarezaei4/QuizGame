using System;
using MongoDbDataAccess.Models;
using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows;

namespace QuizGame.Commands;

public class PickFromDbCommand : CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;
    private QuestionBankViewModel _questionBankViewModel;

    public PickFromDbCommand(QuestionBankViewModel questionBankViewModel, NavigationService navigationService, QuizManager quizManager)
    {
        _navigationService = navigationService;
        _quizManager = quizManager;
        _questionBankViewModel = questionBankViewModel;

        //_questionsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }


    //private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    //{
    //    if (e.PropertyName == nameof(QuestionsListViewModel.SelectedQuestionIndex))
    //    {
    //        OnCanExecutedChanged();
    //    }
    //}


    //public override bool CanExecute(object? parameter)
    //{
    //    return (_questionsListViewModel.SelectedQuestionIndex != null) &&
    //           base.CanExecute(parameter);
    //}
    private bool AddTheQuestions(Question question)
    {
        if (!_quizManager.CurrentQuiz.Questions.Any(q => q.Id == question.Id))
        {
            //var tempGenres = genres.Cast<Genre>().ToList();
            //_quizManager.CurrentQuiz.Questions.T
            //_quizManager.CurrentQuizGenres = tempGenres;
            //new AddAQuestionService(_addANewQuestionViewModel, _quizManager).AddQuestion();
            _quizManager.CurrentQuiz.AddQuestion(question);
            return true;
        }
        MessageBox.Show("This question is already included, try another.", "Error", MessageBoxButton.OK,
            MessageBoxImage.Error);
        return false;

    }
    public override void Execute(object? parameter)
    {
        AddTheQuestions(_questionBankViewModel.SelectedQuestion);
        _navigationService.Navigate();
    }
}