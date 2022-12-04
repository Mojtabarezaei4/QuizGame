using System;
using QuizGame.Managers;
using QuizGame.ViewModels;

namespace QuizGame.Services;

public class NavigationService
{
    private readonly NavigationManager _navigationManager;
    private readonly Func<ViewModelBase> _createViewModel;

    public NavigationService(NavigationManager navigationManager, Func<ViewModelBase> createViewModel)
    {
        _navigationManager = navigationManager;
        _createViewModel = createViewModel;
    }
    public void Navigate()
    {
        _navigationManager.CurrentViewModel = _createViewModel();
    }
}