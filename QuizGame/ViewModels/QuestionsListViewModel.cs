using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class QuestionsListViewModel : ViewModelBase
{

    private readonly QuizManager _quizManager;

    #region BindingProps

    public ObservableCollection<Question> Questions { get; }

    private string _selectedQuestionIndex;

    public string SelectedQuestionIndex
    {
        get => _selectedQuestionIndex;
        set
        {
            _selectedQuestionIndex = value;
            _quizManager.CurrentQuestion = _quizManager.CurrentQuiz.Questions.ElementAt(int.Parse(_selectedQuestionIndex));
            OnPropertyChanged(nameof(SelectedQuestionIndex));
        }
    }

    private string _quizName;

    public string QuizName => _quizName;

    #endregion

    #region Commands

    public ICommand EditQuestionCommand { get; }
    public ICommand FinishCommand { get; }
    public ICommand NavigateAddQuestionCommand { get; }
    public ICommand DeleteQuestionCommand { get; }
    public ICommand CancelCommand { get; }

    #endregion


    public QuestionsListViewModel(QuizManager quizManager, NavigationService navigateToEditQuestion, 
        NavigationService navigateHome, NavigationService navigateToAddQuestion, NavigationService navigateQuestionListView)
    {
        _quizManager = quizManager;
        Questions = new ObservableCollection<Question>(_quizManager.CurrentQuiz.Questions);
        _quizName = _quizManager.CurrentQuiz.Title;

        EditQuestionCommand = new EditQuestionCommand(_quizManager, this, navigateToEditQuestion);
        DeleteQuestionCommand = new DeleteQuestionCommand(_quizManager, this, navigateQuestionListView);

        NavigateAddQuestionCommand = new NavigateCommand(navigateToAddQuestion);
        CancelCommand = new CancelCommand(navigateHome, _quizManager);
        FinishCommand = new FinishCommand(_quizManager, this, navigateHome);
    }

}