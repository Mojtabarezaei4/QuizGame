using System;
using System.Linq;
using MongoDbDataAccess.Models;
using QuizGame.Managers;
using QuizGame.ViewModels;

namespace QuizGame.Services;

public class UpdateQuestionService
{
    private readonly EditQuestionViewModel _editQuestionViewModel;
    private readonly QuizManager _quizManager;

    public UpdateQuestionService(EditQuestionViewModel editQuestionViewModel, QuizManager quizManager)
    {
        _editQuestionViewModel = editQuestionViewModel;
        _quizManager = quizManager;
    }

    public void AddQuestion()
    {
        //if (_quizManager.CurrentQuestion != null)
        //{
            var temp = Array.IndexOf(_quizManager.CurrentQuiz.Questions.ToArray(), _quizManager.CurrentQuestion);
            _quizManager.CurrentQuiz.RemoveQuestion(temp);
            var question = _quizManager.CurrentQuiz.AddQuestion(_editQuestionViewModel.Statement, 0, _editQuestionViewModel.ImageSource,
                _quizManager.currentQuizGenres, _editQuestionViewModel.CorrectAnswer, _editQuestionViewModel.AlternativeAnswer, 
                _editQuestionViewModel.AlternativeAnswerTwo);
            _quizManager.UpdateQuestion(question);
        //}
        //else
        //{
        //    var question = _quizManager.CurrentQuiz.AddQuestion(_editQuestionViewModel.Statement, 0, _editQuestionViewModel.ImageSource,
        //        _quizManager.currentQuizGenres, _editQuestionViewModel.CorrectAnswer, _editQuestionViewModel.AlternativeAnswer, 
        //        _editQuestionViewModel.AlternativeAnswerTwo);
        //    _quizManager.SaveAQuestion(question);
        //}
    }
}