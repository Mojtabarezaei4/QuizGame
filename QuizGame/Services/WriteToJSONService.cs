using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDbDataAccess.Models;
using QuizGame.DataModels;

namespace QuizGame.Services;

public class WriteToJSONService
{
    private readonly string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\\quizzes";

    public async Task SaveQuizToJsonAsync(Quiz quiz)
    {
        if (!Directory.Exists(_filePath))
        {
           Directory.CreateDirectory(_filePath);
        }
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        var json = JsonSerializer.Serialize(quiz, options);
        await using StreamWriter sw = new StreamWriter(Path.Combine(_filePath, quiz.Title + ".json"));
        await sw.WriteLineAsync(json);
    }
}