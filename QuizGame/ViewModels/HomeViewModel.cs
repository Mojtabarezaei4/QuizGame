using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;

    #region BindingProps

    private ObservableCollection<Quiz> _quizzes;
    public ObservableCollection<Quiz> Quizzes
    {
        get
        {
            var temp = _quizzes.GroupBy(x => x.Title).Select(y => y.First());

            _quizzes = new ObservableCollection<Quiz>(temp);
            return _quizzes;
        }
    }

    public string? Genres
    {
        get
        {
            return Quizzes.Select(y => y.Genres).ToString();
        }
    }

    private string _selectedQuizIndex;
    public string SelectedQuizIndex
    {
        get => _selectedQuizIndex;
        set
        {
            _selectedQuizIndex = value;
            _quizManager.CurrentQuiz = _quizManager.Quizzes[int.Parse(_selectedQuizIndex)];
            OnPropertyChanged(nameof(SelectedQuizIndex));
        }
    }

    #endregion

    #region Commands

    public ICommand PlayCommand { get; }
    public ICommand EditQuizCommand { get; }
    public ICommand MakeAQuizCommand { get; }
    public ICommand MixAQuizCommand { get; }

    #endregion

    public HomeViewModel(QuizManager quizManager, NavigationService navigateToEditQuiz, NavigationService navigateToPlay, NavigationService navigateToMixQuiz, NavigationService navigateToMakeAQuiz)
    {
        _quizManager = quizManager;

        foreach (var quiz in _quizManager.Quizzes.ToList())
        {
            if (!quiz.Questions.Any())
            {
                _quizManager.Quizzes.Remove(quiz);
            }
        }

        _quizzes = new ObservableCollection<Quiz>(_quizManager.Quizzes);

        EditQuizCommand = new EditQuizCommand(_quizManager,this, navigateToEditQuiz);
        PlayCommand = new PlayCommand(this, navigateToPlay);

        MakeAQuizCommand = new MakeANewQuizCommand(navigateToMakeAQuiz);
        MixAQuizCommand = new MixAQuizCommand(navigateToMixQuiz);
    }
}