using System.ComponentModel;
using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;

namespace QuizGame.Commands;

public class NextCommand: CommandBase
{
    private readonly AddANewQuestionViewModel _addANewQuestionViewModel;
    private readonly QuizManager _quizManager;
    private readonly NavigationService _navigationService;
    

    public NextCommand(QuizManager quizManager, AddANewQuestionViewModel addANewQuestionViewModel, NavigationService navigationService)
    {
        _addANewQuestionViewModel = addANewQuestionViewModel;
        _navigationService = navigationService;
        _quizManager = quizManager;

        _addANewQuestionViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddANewQuestionViewModel.Statement)
            || e.PropertyName == nameof(AddANewQuestionViewModel.CorrectAnswer)
            || e.PropertyName == nameof(AddANewQuestionViewModel.AlternativeAnswer)
            || e.PropertyName == nameof(AddANewQuestionViewModel.AlternativeAnswerTwo))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_addANewQuestionViewModel.Statement) &&
               (_addANewQuestionViewModel.CorrectAnswer != null) &&
               (_addANewQuestionViewModel.AlternativeAnswer != null) &&
               (_addANewQuestionViewModel.AlternativeAnswerTwo != null) &&
               base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        new AddAQuestionService(_addANewQuestionViewModel, _quizManager).AddQuestion();
        
        _navigationService.Navigate();
    }
}