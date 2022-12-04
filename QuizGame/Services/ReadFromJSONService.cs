using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuizGame.DataModels;

namespace QuizGame.Services;

public class ReadFromJSONService
{
    private readonly string _filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\quizzes";

    public async Task<Quiz?> LoadQuizFromJsonAsync(string filePath)
    {
        string content = string.Empty;
        string? line;

        Console.WriteLine(_filePath);
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        if (Directory.Exists(_filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    content += line;
                }
            }

            var outQuiz = JsonSerializer.Deserialize<Quiz>(content, options);

            return outQuiz;
        }
        return null;
    }
}