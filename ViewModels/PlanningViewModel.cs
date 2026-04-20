using System;
using CSharpEmilyApp.API;

namespace CSharpEmilyApp.ViewModels;

public class PlanningViewModel : ViewModelBase
{
    private GetPlanning.PlanningResponse _planningResponse;
    private bool _isLoading = true;
    private string? _error;

    public GetPlanning.PlanningResponse? PlanningResponse
    {
        get => _planningResponse;
        set
        {
            _planningResponse = value;
            OnPropertyChanged(nameof(PlanningResponse));
        }
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

    public PlanningViewModel()
    {
        LoadPlanning();
    }

    private async void LoadPlanning()
    {
        try
        {
            IsLoading = true;
            PlanningResponse = await new GetPlanning().GetAsync();
            Console.WriteLine($"Got response: {PlanningResponse?.id ?? "null"}");

        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
}