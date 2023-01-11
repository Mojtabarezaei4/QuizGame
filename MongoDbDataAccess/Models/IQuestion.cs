using System.Collections.Generic;

namespace MongoDbDataAccess.Models;

public interface IQuestion
{
    public string Id{ get; }
    public string Statement { get; set; }
    public string? ImageSource { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; }
    public List<Genre> Genres { get; set; }
}