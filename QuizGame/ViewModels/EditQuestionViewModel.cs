using System.Windows.Input;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class EditQuestionViewModel : ViewModelBase
{
    private QuizManager _quizManager;

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
    public ICommand EditCommand { get; }

    #endregion

    public EditQuestionViewModel(QuizManager quizManager,
        NavigationService navigateHome,
        NavigationService navigateQuestionsView,
        NavigationService navigateQuestionListView)
    {
        _quizManager = quizManager;

        var currentQuestion = _quizManager.CurrentQuestion;
        _statement = currentQuestion.Statement;
        _correctAnswer = currentQuestion.Answers[0];
        _alternativeAnswer = currentQuestion.Answers[1];
        _alternativeAnswerTwo = currentQuestion.Answers[2];
        _imageSource = currentQuestion.ImageSource;

        CancelCommand = new CancelCommand(navigateHome);
        QuestionsCommand = new QuestionsCommand(navigateQuestionsView);
        EditCommand = new UpdateQuestionCommand(_quizManager, this, navigateQuestionListView);
    }
}