using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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


    private string _selectedQuestionIndex;

    public string SelectedQuestionIndex
    {
        get => _selectedQuestionIndex;
        set
        {
            _selectedQuestionIndex = value;
            _selectedQuestion = _quizManager.CurrentQuiz.Questions.ElementAt(int.Parse(_selectedQuestionIndex));
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
    }

    public ICommand PickFromDbCommand { get; }
    public ICommand QuestionsCommand { get; }


    public QuestionBankViewModel(QuizManager quizManager, NavigationService questionBankViewModel, NavigationService navigateToQuestions)
    {
        _quizManager = quizManager;
        _questions = new ObservableCollection<Question>(_quizManager.Questions);


        QuestionsCommand = new QuestionsCommand(navigateToQuestions);
        PickFromDbCommand = new PickFromDbCommand(this, questionBankViewModel, quizManager);
    }

}