using System;
using MongoDbDataAccess.Models;
using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame.Commands;

public class PickFromQuestionBankCommand : CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;
    private readonly QuestionBankViewModel _questionBankViewModel;

    public PickFromQuestionBankCommand(QuestionBankViewModel questionBankViewModel, NavigationService navigationService, QuizManager quizManager)
    {
        _navigationService = navigationService;
        _quizManager = quizManager;
        _questionBankViewModel = questionBankViewModel;

        _questionBankViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }


    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    { 
        if (e.PropertyName == nameof(QuestionsListViewModel.SelectedQuestionIndex))
        {
            OnCanExecutedChanged();
        }
    }


    public override bool CanExecute(object? parameter)
    {
        return (_questionBankViewModel.SelectedQuestionIndex != null) &&
               base.CanExecute(parameter);
    }
    private void AddTheQuestions(Question question)
    {
        if (_quizManager.CurrentQuiz.Questions.All(q => q.Id != question.Id))
        {
            _quizManager.CurrentQuiz.AddQuestion(question);
            return;
        }
        MessageBox.Show("This question is already included, try another.", "Error", MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
    public override void Execute(object? parameter)
    {
        AddTheQuestions(_questionBankViewModel.SelectedQuestion);
        _navigationService.Navigate();
    }
}