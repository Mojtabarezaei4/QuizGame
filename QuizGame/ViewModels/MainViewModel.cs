using QuizGame.Managers;

namespace QuizGame.ViewModels;

public class MainViewModel: ViewModelBase
{
    private readonly NavigationManager _navigationManager;

    public ViewModelBase? CurrentViewModel => _navigationManager.CurrentViewModel;

    public MainViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        _navigationManager.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}