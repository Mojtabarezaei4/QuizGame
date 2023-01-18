using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MongoDbDataAccess.DataAccess;
using MongoDbDataAccess.DataGenerator;
using MongoDbDataAccess.Models;

namespace QuizGame.Managers;

public class QuizManager
{
    private readonly QuizDataAccess _quizDataAccess;
    //public QuizDataAccess QuizDataAccess => _quizDataAccess;
    public Quiz CurrentQuiz { get; set; }
    public Question CurrentQuestion { get; set; }

    public List<Quiz> Quizzes = new List<Quiz>();
    public List<string> CurrentQuizGenres { get; set; }
    public ObservableCollection<Genre> Genres { get; set; }
    public IEnumerable<Question> Questions { get; set; }

    public int AskedQuestions = 0;
    public int RightAnswers = 0;

    public QuizManager(QuizDataAccess quizDataAccess)
    {
        _quizDataAccess = quizDataAccess;
    }
    
    public async Task LoadQuizzes()
    {
        Quizzes = await _quizDataAccess.GetAllQuizzes();

        if (!Quizzes.Any())
        {
            new DataGenerator(_quizDataAccess).GenerateData();
            await LoadQuizzes();
        }
        foreach (var q in Quizzes.Where(q => !q.Questions.Any()).ToList())
        {
            Quizzes.Remove(q);
            await _quizDataAccess.DeleteAQuiz(q);
        }

        Questions = await _quizDataAccess.GetAllQuestions();

        Genres = new ObservableCollection<Genre>(await _quizDataAccess.GetAllGenres());


    }

    public async Task SaveAQuiz()
    {
        Quizzes.Add(CurrentQuiz);

        if (Quizzes.Any())
        {
            foreach (var quiz in Quizzes)
            {
                _quizDataAccess.CreateAQuiz(quiz);
                UpdateQuiz(quiz);
            }
        }

        await LoadQuizzes();
    }

    public void SaveAQuestion(Question question)
    {
        _quizDataAccess.CreateAQuestion(question);
    }

    public void UpdateQuestion(Question question)
    {
        _quizDataAccess.UpdateAQuestion(question);
    }

    public void UpdateQuiz(Quiz quiz)
    {
        _quizDataAccess.UpdateAQuiz(quiz);
    }
    public void DeleteQuestion(Question question)
    {
        _quizDataAccess.DeleteAQuestion(question);
    }

    public void SaveAGenre(string newGenre)
    {
        var genre = new Genre()
        {
            Name = newGenre
        };
        if (Genres.ToList().FirstOrDefault(g => g.Name.ToLower() == genre.Name.ToLower()) == null)
        {
            _quizDataAccess.CreateAGenre(genre);
            Genres.Add(genre);
        }
    }
}