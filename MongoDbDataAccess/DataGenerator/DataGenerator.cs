using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using MongoDbDataAccess.Models;
using MongoDbDataAccess.DataAccess;

namespace MongoDbDataAccess.DataGenerator;

public class DataGenerator
{
    private readonly QuizDataAccess _quizDataAccess;
    //DataAccess 

    public DataGenerator(QuizDataAccess quizDataAccess)
    {
        _quizDataAccess = quizDataAccess;
    }
    public void GenerateData()
    {
        string[] genres = { "Cat", "Animal", "Dog" };
        
        Question[] animalQuestions = { 
            new Question(
                "How long is an elephant pregnant before it gives birth?",
             "https://imgs.mongabay.com/wp-content/uploads/sites/20/2022/09/12085002/elephants-in-Zimbabwe-768x512.jpg",
                new String[3]{
                            "22 months",
                            "20 months",
                            "18 months"
                            },
            0,
                new List<string>{ "Animal" }
            ),
            new Question(
                "What is the size of a newborn kangaroo?",
                "https://www.thoughtco.com/thmb/OqANUu4U3tNL1Uo2n19uX6PzbM8=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/red_kangaroo-24c18ab08dc145f1a798abd4b820390a.jpg",
                new String[3]{
                    "One inch",
                    "Three inch",
                    "Four inch"
                },
                0,
                new List<string>{ "Animal" }
            ),
            new Question(
                "How far away can a wolf smell its prey?",
                "https://images.squarespace-cdn.com/content/v1/618c0d1b6885dc2952648de8/1645313043921-BTVJFUCT0L6J31B4V3B9/arrows-gaze.jpeg?format=2500w",
                new String[3]{
                    "Almost two miles",
                    "One mile",
                    "Half a mile"
                },
                0,
                new List<string>{ "Animal" }
            ),
        };

        Question[] dogQuestions =
        {
            new Question(
                "What breed of dog is Lady in The Lady and the Tramp?",
                "https://images.immediate.co.uk/production/volatile/sites/3/2019/10/lady-and-the-tramp-224a4cc.jpg?quality=90\u0026resize=620,413",
                new String[3]{
                    "American Cocker Spaniel",
                    "Poodle",
                    "Pomeranian"
                },
                0,
                new List<string>{ "Dog", "Animal" }
            ),
            new Question(
                "In which movie animated cartoon can you see a dog named Dug?",
                "https://img.icons8.com/ios/100/null/no-image.png",
                new String[3]{
                    "Up",
                    "Wall-E",
                    "Inside Out"
                },
                0,
                new List<string>{ "Dog", "Animal" }
            ),
            new Question(
                "How many teeth does an adult dog have?",
                "https://assets3.thrillist.com/v1/image/2536470/1200x600/crop;",
                new String[3]{
                    "42",
                    "40",
                    "44"
                },
                0,
                new List<string>{ "Dog", "Animal" }
            ),
            new Question(
                "What is a group of puppies called?",
                "https://images.prismic.io/wisdom/2fabb777-59dc-4963-a38f-8bd8132c7e10_shutterstock_1048123303_cropped.jpg?auto=compress%2Cformat\u0026rect=241%2C0%2C1440%2C1080\u0026w=820\u0026h=615",
                new String[3]{
                    "A litter",
                    "A pack",
                    "Puppies"
                },
                0,
                new List<string>{ "Dog", "Animal" }
            )

        };

        Question[] catQuestions =
        {
            new Question(
                "At which age do cats become fertile?",
                "https://icatcare.org/app/uploads/2018/07/Cat-pregnancy.png",
                new String[3]{
                    "Female 5-7 months; male at 6 months.",
                    "Female 2-3 months; male at 2 months.",
                    "Female 4-5 months; male at 3 months."
                },
                0,
                new List<string>{ "Cat", "Animal" }
            ),
            new Question(
                "A cat is pregnant for how long?", 
                "https://static.onecms.io/wp-content/uploads/sites/47/2020/08/13/pregnant-cat-1165219581-2000.jpg",
                new String[3]{
                    "Between 60 and 70 days.",
                    "Between 50 and 70 days.",
                    "Between 50 and 60 days."
                },
                0,
                new List<string>{ "Cat", "Animal" }
            ),
            new Question(
                "How long does a mother cat care for her kittens?",
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-dcS0QG2odD6gB3jjsLBvuwjSi-KT6riDDg\u0026usqp=CAU",
                new String[3]{
                    "9 to 10 weeks old.",
                    "5 to 7 weeks old.",
                    "6 to 9 weeks old."
                },
                0,
                new List<string>{ "Cat", "Animal" }
            ),
            new Question(
                "How many kittens are in a litter?",
                "https://www.rover.com/blog/wp-content/uploads/2021/04/kittens-in-litter-box-960x540.jpg",
                new String[3]{
                    "Average 6 kittens in a litter.",
                    "average 4 kittens in a litter.",
                    "average 5 kittens in a litter."
                },
                0,
                new List<string>{ "Cat", "Animal" }
            ),
            new Question(
                "Are cats able to swim?",
                "https://img.icons8.com/ios/100/null/no-image.png",
                new String[3]{
                    "Some, if necessary.",
                    "No.",
                    "Cats hate water"
                },
                0,
                new List<string>{ "Cat", "Animal" }
            )
        };

        Quiz[] quizzes =
        {
            new Quiz(
                "Cat",
                "https://media.wired.co.uk/photos/60c8730fa81eb7f50b44037e/3:2/w_3329,h_2219,c_limit/1521-WIRED-Cat.jpeg",
                new List<string>{"Cat", "Animal"}
            ), 
            new Quiz(
                "Dogs are happy gods",
                "https://ggsc.s3.amazonaws.com/images/uploads/The_Science-Backed_Benefits_of_Being_a_Dog_Owner.jpg",
                new List<string>{"Dog", "Animal"}
                ),
            new Quiz(
                "Wild",
                "https://img.icons8.com/ios/100/null/no-image.png",
                new List<string>{"Animal"}
                )
        };

        foreach (var genre in genres)
        {
            var temp = new Genre
            {
                Name = genre
            };
            _quizDataAccess.CreateAGenre(temp);
        }

        foreach (var quiz in quizzes)
        {
            if (quiz.Title == "Cat")
            {
                foreach (var catQuestion in catQuestions)
                {
                    _quizDataAccess.CreateAQuestion(catQuestion);
                    quiz.AddQuestion(catQuestion);
                }

                _quizDataAccess.CreateAQuiz(quiz);
            }
            if (quiz.Title == "Dogs are happy gods")
            {
                foreach (var dogQuestion in dogQuestions)
                {
                    _quizDataAccess.CreateAQuestion(dogQuestion);
                    quiz.AddQuestion(dogQuestion);
                }
                _quizDataAccess.CreateAQuiz(quiz);
            }
            if (quiz.Title == "Wild")
            {
                foreach (var animalQuestion in animalQuestions)
                {
                    _quizDataAccess.CreateAQuestion(animalQuestion);
                    quiz.AddQuestion(animalQuestion);
                }
                _quizDataAccess.CreateAQuiz(quiz);
            }
        }
    }
}