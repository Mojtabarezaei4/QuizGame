﻿using QuizGame.Managers;
using QuizGame.ViewModels;
using System.Windows;
using MongoDbDataAccess.DataAccess;
using MongoDbDataAccess.Models;
using QuizGame.Services;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;

        public App()
        {
            var quizDataAccess = new QuizDataAccess();
            _quizManager = new QuizManager(quizDataAccess);
            _navigationManager = new NavigationManager();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _quizManager.LoadQuizzes();
            _navigationManager.CurrentViewModel = CreateHomeViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationManager)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        #region ViewModel-creations

        private HomeViewModel CreateHomeViewModel()
        {
            _quizManager.LoadQuizzes();
            return new HomeViewModel(_quizManager,
                new NavigationService(_navigationManager, CreateQuestionsListViewModel),
                new NavigationService(_navigationManager, CreateGameViewModel),
                new NavigationService(_navigationManager, CreateMixQuizViewModel),
                new NavigationService(_navigationManager, CreateMakeANewQuizViewModel));
        }

        private MixQuizViewModel CreateMixQuizViewModel()
        {
            return new MixQuizViewModel(_quizManager, new NavigationService(_navigationManager, CreateHomeViewModel), 
                new NavigationService(_navigationManager, CreateGameViewModel));
        }

        private MakeANewQuizViewModel CreateMakeANewQuizViewModel()
        {
            return new MakeANewQuizViewModel(_quizManager,
                new NavigationService(_navigationManager, CreateHomeViewModel),
                new NavigationService(_navigationManager, CreateAddANewQuestionViewModel), 
                new NavigationService(_navigationManager, CreateMakeANewQuizViewModel));
        }

        private ViewModelBase CreateAddANewQuestionViewModel()
        {
            return new AddANewQuestionViewModel(_quizManager,
                new NavigationService(_navigationManager, CreateHomeViewModel),
                new NavigationService(_navigationManager, CreateQuestionsListViewModel),
                new NavigationService(_navigationManager, CreateAddANewQuestionViewModel),
                new NavigationService(_navigationManager, CreateQuestionBankViewModel));
        }

        private ViewModelBase CreateQuestionBankViewModel()
        {
            return new QuestionBankViewModel(_quizManager,
                new NavigationService(_navigationManager, CreateAddANewQuestionViewModel),
                new NavigationService(_navigationManager, CreateQuestionsListViewModel));
        }

        private EditQuestionViewModel CreateEditQuizViewModel()
        {
            return new EditQuestionViewModel(_quizManager, new NavigationService(_navigationManager, CreateHomeViewModel),
                new NavigationService(_navigationManager, CreateQuestionsListViewModel),
                new NavigationService(_navigationManager, CreateQuestionsListViewModel));
        }

        private ViewModelBase CreateGameViewModel()
        {
            if (Quiz.NoQuestionsLeft)
            {
                return CreateResultViewModel();
            }
            return new GameViewModel(_quizManager, new NavigationService(_navigationManager, CreateHomeViewModel), 
                new NavigationService(_navigationManager, CreateGameViewModel));
        }

        private QuestionsListViewModel CreateQuestionsListViewModel()
        {
            return new QuestionsListViewModel(_quizManager,
                new NavigationService(_navigationManager, CreateEditQuizViewModel),
                new NavigationService(_navigationManager, CreateHomeViewModel),
                new NavigationService(_navigationManager, CreateAddANewQuestionViewModel),
                new NavigationService(_navigationManager, CreateQuestionsListViewModel));
        }

        private ResultViewModel CreateResultViewModel()
        {
            return new ResultViewModel(_quizManager, new NavigationService(_navigationManager, CreateHomeViewModel));
        }

        #endregion

    }
}
