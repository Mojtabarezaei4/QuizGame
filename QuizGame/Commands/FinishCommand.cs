using QuizGame.Managers;
using QuizGame.Services;
using QuizGame.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QuizGame.Commands;

public class FinishCommand: CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly QuestionsListViewModel _questionsListViewModel;
    private readonly QuizManager _quizManager;

    public FinishCommand(QuizManager quizManager, QuestionsListViewModel questionsListViewModel, NavigationService navigationService)
    {
        _navigationService = navigationService;
        _questionsListViewModel = questionsListViewModel;
        _quizManager = quizManager;

        _questionsListViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(QuestionsListViewModel.Questions))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return true;
    }


    private async Task SaveChanges()
    {
        await _quizManager.SaveAQuiz();
    }

    public override void Execute(object? parameter)
    {
        SaveChanges();
        _navigationService.Navigate();
    }
}