using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MongoDbDataAccess.Models;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class QuestionBankViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;

    private Question _selectedQuestion;
    public Question SelectedQuestion => _selectedQuestion;



    private string _search;

    public string Search
    {
        get => _search;
        set
        {
            _search = value;
            _questions = new ObservableCollection<Question>(_quizManager.CurrentQuizGenres.SelectMany(genre =>
                _quizManager.Questions.Where(q => q.Genres.Contains(genre))).ToList()
                .Where(q => q.Statement.ToLower().Contains(_search.ToLower())).ToList());
            OnPropertyChanged(nameof(Questions));
        }
    }

    private string _selectedQuestionIndex;

    public string SelectedQuestionIndex
    {
        get => _selectedQuestionIndex;
        set
        {
            _selectedQuestionIndex = value;
            if (_questions.Count() >= int.Parse(_selectedQuestionIndex) && int.Parse(_selectedQuestionIndex) >= 0)
            {
                _selectedQuestion = Questions.ElementAt(int.Parse(_selectedQuestionIndex));
            }
            else
            {
                _selectedQuestionIndex = null;
            }
            OnPropertyChanged(nameof(SelectedQuestionIndex));
        }
    }


    private ObservableCollection<Question> _questions;

    public ObservableCollection<Question> Questions
    {
        get
        {
            var temp = _questions.GroupBy(q => q.Statement).Select(y => y.First());
            _questions = new ObservableCollection<Question>(temp);
            return _questions;

        }
        set => OnPropertyChanged(nameof(Search));
    }

    public ICommand PickFromDbCommand { get; }
    public ICommand QuestionsCommand { get; }


    public QuestionBankViewModel(QuizManager quizManager, NavigationService questionBankViewModel, NavigationService navigateToQuestions)
    {
        _quizManager = quizManager;

        var temp = _quizManager.CurrentQuizGenres.SelectMany(genre =>
        _quizManager.Questions.Where(q => q.Genres.Contains(genre))).ToList();

        _questions = new ObservableCollection<Question>(temp);

        QuestionsCommand = new QuestionsCommand(navigateToQuestions);
        PickFromDbCommand = new PickFromQuestionBankCommand(this, questionBankViewModel, quizManager);
    }

}