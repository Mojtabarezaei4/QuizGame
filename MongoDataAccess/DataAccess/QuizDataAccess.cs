using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess;

public class QuizDataAccess
{
    private const string ConnectionString = "mongodb://127.0.0.1:27017";
    private const string DatabaseName = "QuizDb";
    private const string QuizCollection = "Quizzes";
    private const string QuestionCollection = "Questions";
    private const string GenreCollection = "Genres";

    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);

        return db.GetCollection<T>(collection);
    }

    public async Task<List<Quiz>> GetAllQuizzes()
    {
        var quizCollection = ConnectToMongo<Quiz>(QuestionCollection);
        var results = await quizCollection.FindAsync(_ => true);

        return results.ToList();
    }

    public async Task<List<Question>> GetallQuestions()
    {
        var questionCollection = ConnectToMongo<Question>(QuestionCollection);
        var results = await questionCollection.FindAsync(_ => true);

        return results.ToList();
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        var genreCollection = ConnectToMongo<Genre>(GenreCollection);
        var results = await genreCollection.FindAsync(_ => true);

        return results.ToList();
    }

    public Task CreateAQuiz(Quiz quiz)
    {
        var quizCollection = ConnectToMongo<Quiz>(QuizCollection);
        return quizCollection.InsertOneAsync(quiz);
    }

    public Task CreateAQuestion(Question question)
    {
        var questionCollection = ConnectToMongo<Question>(QuestionCollection);
        return questionCollection.InsertOneAsync(question);
    }

    public Task CreateAGenre(Genre genre)
    {
        var genreCollection = ConnectToMongo<Genre>(GenreCollection);
        return genreCollection.InsertOneAsync(genre);
    }
}