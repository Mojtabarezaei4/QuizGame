namespace QuizGame.DataModels;

public interface IQuestion
{
    public string Statement { get; set; }
    public string? ImageSource { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; }
}