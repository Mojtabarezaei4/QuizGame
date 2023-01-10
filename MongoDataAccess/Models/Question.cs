namespace MongoDataAccess.Models;

public class Question : IQuestion
{
    public string Statement { get; set; }
    public string? ImageSource { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; } = 0;

    public Question(string statement, string? imageSource, string[] answers, int correctAnswer)
    {
        Statement = statement;
        ImageSource = string.IsNullOrEmpty(imageSource) ? "https://img.icons8.com/ios/100/null/no-image.png" : imageSource;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}