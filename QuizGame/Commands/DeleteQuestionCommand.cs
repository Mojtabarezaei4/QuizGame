using QuizGame.ViewModels;
using System.ComponentModel;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.Commands;

public class DeleteQuestionCommand: CommandBase
{
    private readonly QuestionsListViewModel _questionsListViewModel;
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;

    public DeleteQuestionCommand(QuizManager quizManager, QuestionsListViewModel questionsListViewModel, NavigationService navigationService)
    {
        _questionsListViewModel = questionsListViewModel;
        _navigationService = navigationService;
        _quizManager = quizManager;

        _questionsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
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
        return (_questionsListViewModel.SelectedQuestionIndex != null) &&
               base.CanExecute(parameter);
    }

    private void RemoveQuestion()
    {
        _quizManager.CurrentQuiz.RemoveQuestion(int.Parse(_questionsListViewModel.SelectedQuestionIndex));
    }

    public override void Execute(object? parameter)
    {
        RemoveQuestion();
        _navigationService.Navigate();
    }
}