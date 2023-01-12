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
            var question = _quizManager.CurrentQuiz.AddQuestion(_addANewQuestionViewModel.Statement, 0, 
                _addANewQuestionViewModel.ImageSource, _quizManager.CurrentQuizGenres, 
                _addANewQuestionViewModel.CorrectAnswer, _addANewQuestionViewModel.AlternativeAnswer, 
                _addANewQuestionViewModel.AlternativeAnswerTwo);
            _quizManager.SaveAQuestion(question);
    }
}