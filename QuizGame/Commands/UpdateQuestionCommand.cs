using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;

namespace QuizGame.Commands;

public class UpdateQuestionCommand : CommandBase
{
    private readonly EditQuestionViewModel _editQuestionViewModel;
    private readonly QuizManager _quizManager;
    private readonly NavigationService _navigationService;

    public UpdateQuestionCommand(QuizManager quizManager, EditQuestionViewModel editQuestionViewModel, NavigationService navigationService)
    {
        _editQuestionViewModel = editQuestionViewModel;
        _navigationService = navigationService;
        _quizManager = quizManager;

        _editQuestionViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(EditQuestionViewModel.Statement)
            || e.PropertyName == nameof(EditQuestionViewModel.CorrectAnswer)
            || e.PropertyName == nameof(EditQuestionViewModel.AlternativeAnswer)
            || e.PropertyName == nameof(EditQuestionViewModel.AlternativeAnswerTwo))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_editQuestionViewModel.Statement) &&
               (_editQuestionViewModel.CorrectAnswer != null) &&
               (_editQuestionViewModel.AlternativeAnswer != null) &&
               (_editQuestionViewModel.AlternativeAnswerTwo != null) &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        new UpdateQuestionService(_editQuestionViewModel, _quizManager).AddQuestion();
        
        _navigationService.Navigate();
    }
}