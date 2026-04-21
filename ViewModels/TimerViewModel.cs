using System;
using CSharpEmilyApp.API;

namespace CSharpEmilyApp.ViewModels;

public class TimerViewModel : ViewModelBase
{
    private GetTimers.TimerApiResponse? _timerResponse;
    private bool _isLoading = true;
    private string? _error;

    public GetTimers.TimerApiResponse? TimerResponse
    {
        get => _timerResponse;
        set { _timerResponse = value; OnPropertyChanged(nameof(TimerResponse)); }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
    }

    public string? Error
    {
        get => _error;
        set { _error = value; OnPropertyChanged(nameof(Error)); }
    }

    public TimerViewModel()
    {
        LoadTimers();
    }

    private async void LoadTimers()
    {
        try
        {
            IsLoading = true;
            TimerResponse = await new GetTimers().GetAsync();
            Console.WriteLine($"Got {TimerResponse?.Results.Count ?? 0} items");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            Console.WriteLine($"ERROR: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}