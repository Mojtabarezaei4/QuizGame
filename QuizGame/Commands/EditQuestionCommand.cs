using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace QuizGame.Commands;

public class EditQuestionCommand : CommandBase
{
    private readonly QuestionsListViewModel _questionsListViewModel;
    private readonly NavigationService _navigationService;
    private readonly QuizManager _quizManager;

    public EditQuestionCommand(QuizManager quizManager, QuestionsListViewModel questionsListViewModel, NavigationService navigationService)
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
    public override void Execute(object? parameter)
    {
        if (_quizManager.CurrentQuiz.Questions.Any())
        {
            _quizManager.CurrentQuestion =
                _quizManager.CurrentQuiz.Questions.ElementAt(int.Parse(_questionsListViewModel.SelectedQuestionIndex));
        }
        _navigationService.Navigate();
    }
}