using System;
using CSharpEmilyApp.API;

namespace CSharpEmilyApp.ViewModels;

public class PlanningViewModel : ViewModelBase
{
    private GetPlanning.PlanningApiResponse? _planningResponse;
    private bool _isLoading = true;
    private string? _error;
    public bool HasError => !string.IsNullOrEmpty(_error);

    public GetPlanning.PlanningApiResponse? PlanningResponse
    {
        get => _planningResponse;
        set
        {
            _planningResponse = value;
            OnPropertyChanged(nameof(PlanningResponse));
        }
    }

    public bool isLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(isLoading));
        }
    }

    public string? Error
    {
        get => _error;
        set
        {
            _error = value;
            OnPropertyChanged(nameof(Error));
            OnPropertyChanged(nameof(HasError));
        }
    }

    // load planning
    public PlanningViewModel()
    {
        LoadPlanning();
    }

    private async void LoadPlanning()
    {
        try
        {
            isLoading = true;
            PlanningResponse = await new GetPlanning().GetAsync();
            Console.WriteLine($"Got {PlanningResponse.Results.Count} results");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            Console.WriteLine(ex);
        }
        finally
        {
            isLoading = false;
        }
    }
    
}