using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using MongoDB.Driver;
using MongoDbDataAccess.Models;

namespace MongoDbDataAccess.DataAccess;

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
        var quizCollection = ConnectToMongo<Quiz>(QuizCollection);
        var results = await quizCollection.FindAsync(_ => true);

        return results.ToList();
    }

    public async Task<List<Question>> GetAllQuestions()
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

    public Task UpdateAQuiz(Quiz quiz)
    {
        var quizCollection = ConnectToMongo<Quiz>(QuizCollection);
        var filter = Builders<Quiz>.Filter.Eq("Id", quiz.Id);
        return quizCollection.ReplaceOneAsync(filter, quiz, new ReplaceOptions() { IsUpsert = true });
    }

    public Task UpdateAQuestion(Question question)
    {
        var questionCollection = ConnectToMongo<Question>(QuestionCollection);
        var filter = Builders<Question>.Filter.Eq("Id", question.Id);
        return questionCollection.ReplaceOneAsync(filter, question, new ReplaceOptions() { IsUpsert = true });
    }

    public Task DeleteAQuiz(Quiz quiz)
    {
        var quizCollection = ConnectToMongo<Quiz>(QuizCollection);
        return quizCollection.DeleteOneAsync(q => q.Id == quiz.Id);
    }

    public Task DeleteAQuestion(Question question)
    {
        var questionCollection = ConnectToMongo<Question>(QuestionCollection);
        return questionCollection.DeleteOneAsync(q => q.Id == question.Id);
    }

}