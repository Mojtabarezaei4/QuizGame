using System.Linq;
using System.Windows.Input;
using QuizGame.Commands;
using QuizGame.Managers;
using QuizGame.Services;

namespace QuizGame.ViewModels;

public class ResultViewModel : ViewModelBase
{
    private readonly QuizManager _quizManager;

    #region BindingProps

    public string Result { get; set; }

    #endregion

    #region Commands

    public ICommand HomeCommand { get; }

    #endregion

    public ResultViewModel(QuizManager quizManager, NavigationService navigateHome)
    {
        _quizManager = quizManager;
        Result = _quizManager.RightAnswers + "/" + _quizManager.CurrentQuiz.Questions.Count();

        HomeCommand = new HomeCommand(_quizManager, navigateHome);
    }
}