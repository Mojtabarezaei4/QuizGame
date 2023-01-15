using QuizGame.Commands;
using System.Windows.Input;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class AddANewQuestionViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;

    #region BindingProps

    private string _statement;

    public string Statement
    {
        get
        {
            return _statement;
        }
        set
        {
            _statement = value;
            OnPropertyChanged(nameof(Statement));
        }
    }

    private string _correctAnswer;

    public string CorrectAnswer
    {
        get
        {
            return _correctAnswer;
        }
        set
        {
            _correctAnswer = value;
            OnPropertyChanged(nameof(CorrectAnswer));
        }
    }

    private string _alternativeAnswer;

    public string AlternativeAnswer
    {
        get
        {
            return _alternativeAnswer;
        }
        set
        {
            _alternativeAnswer = value;
            OnPropertyChanged(nameof(AlternativeAnswer));
        }
    }

    private string _alternativeAnswerTwo;

    public string AlternativeAnswerTwo
    {
        get
        {
            return _alternativeAnswerTwo;
        }
        set
        {
            _alternativeAnswerTwo = value;
            OnPropertyChanged(nameof(AlternativeAnswerTwo));
        }
    }

    private string? _imageSource;

    public string? ImageSource
    {
        get
        {
            return _imageSource;
        }
        set
        {
            _imageSource = value;
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    #endregion

    #region Commands

    public ICommand CancelCommand { get; }
    public ICommand QuestionsCommand { get; }
    public ICommand PickFromDbCommand { get; }
    public ICommand NextCommand { get; }

    

    #endregion

    public AddANewQuestionViewModel(QuizManager quizManager, NavigationService navigateHome, NavigationService navigateQuestionsView, 
        NavigationService navigateAddQuestionView, NavigationService navigateToQuestionBank)
    {
        _quizManager = quizManager;
        
        PickFromDbCommand = new PickFromDbCommand(navigateToQuestionBank);
        CancelCommand = new CancelCommand(navigateHome);
        QuestionsCommand = new QuestionsCommand(navigateQuestionsView);
        NextCommand = new NextCommand(_quizManager, this, navigateAddQuestionView);
    }
}