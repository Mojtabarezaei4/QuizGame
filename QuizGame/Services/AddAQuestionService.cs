using QuizGame.Managers;
using QuizGame.ViewModels;

namespace QuizGame.Services;

public class AddAQuestionService
{
    private readonly AddANewQuestionViewModel _addANewQuestionViewModel;
    private readonly QuizManager _quizManager;

    public AddAQuestionService(AddANewQuestionViewModel addANewQuestionViewModel, QuizManager quizManager)
    {
        _addANewQuestionViewModel = addANewQuestionViewModel;
        _quizManager = quizManager;
    }
    public void AddQuestion()
    {
            _quizManager.CurrentQuiz.AddQuestion(_addANewQuestionViewModel.Statement, 0, _addANewQuestionViewModel.ImageSource,
                _addANewQuestionViewModel.CorrectAnswer, _addANewQuestionViewModel.AlternativeAnswer, _addANewQuestionViewModel.AlternativeAnswerTwo);
    }
}