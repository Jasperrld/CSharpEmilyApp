using System;
using System.Collections.Generic;
using CSharpEmilyApp.API;
using System.Linq;
using System.Windows.Input;

namespace CSharpEmilyApp.ViewModels;

public class PlanningCell
{
    public string MachineName { get; set; } = "";
    public List<PlanningJob> Jobs { get; set; } = new();
}

public class PlanningJob
{
    public string OrderNumber { get; set; } = "";
    public string Company { get; set; } = "";
    public string Description { get; set; } = "";
    public double TotalHours { get; set; }
    public string MachineName  { get; set; } = "";
}

public class PlanningRow
{
    public string Date { get; set; } = "";
    public List<PlanningCell> Cells { get; set; } = new();
    
    public string DayName => DateTime.TryParse(Date, out var d)
        ? d.ToString("dddd d MMMM", new System.Globalization.CultureInfo("nl-NL"))
        : Date;
}

public class PlanningViewModel : ViewModelBase
{
    private GetPlanning.PlanningApiResponse? _planningResponse;
    private bool _isLoading = true;
    private string? _error;
    public bool HasError => !string.IsNullOrEmpty(_error);
    
    private List<string> _machineNames = new();
    private List<PlanningRow> _planningRows = new();
    
    private DateTime _weekStart = GetMonday(DateTime.Today);
    
    public ICommand GoToPreviousWeekCommand { get; }
    public ICommand GoToNextWeekCommand { get; }
    
    private static string getBaseMachineName(string name)
    {
        var idx = name.IndexOf('(');
        return idx > 0 ? name[..idx].Trim() : name;
    }
    
    public DateTime WeekStart
    {
        get => _weekStart;
        set
        {
            _weekStart = value;
            OnPropertyChanged(nameof(WeekStart));
            OnPropertyChanged(nameof(WeekLabel));
            LoadPlanning();
        }
    }

    public string WeekLabel =>
        $"{WeekStart:d MMM} - {WeekStart.AddDays(6):d MMM yyyy}";

    public void GoToPreviousWeek() => WeekStart = WeekStart.AddDays(-7);
    public void GoToNextWeek() => WeekStart = WeekStart.AddDays(7);

    private static DateTime GetMonday(DateTime date)
        => date.AddDays(-(int)date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1);

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

    public PlanningViewModel()
    {
        GoToPreviousWeekCommand = new RelayCommand(_ => GoToPreviousWeek());
        GoToNextWeekCommand = new RelayCommand(_ => GoToNextWeek());
        LoadPlanning();
    }

    private async void LoadPlanning()
    {
        try
        {
            isLoading = true;
            PlanningResponse = await new GetPlanning().GetAsync(WeekStart, WeekStart.AddDays(6));
            if (PlanningResponse?.Results != null)
                BuildGrid(PlanningResponse.Results);
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

    public List<string> MachineNames
    {
        get => _machineNames;
        set { _machineNames = value; OnPropertyChanged(nameof(MachineNames)); }
    }

    public List<PlanningRow> PlanningRows
    {
        get => _planningRows;
        set { _planningRows = value; OnPropertyChanged(nameof(PlanningRows)); }
    }

    private void BuildGrid(List<GetPlanning.PlanningResponse> results)
    {
        var machines = results
            .SelectMany(r => r.Machines)
            .Select(m => m.Name ?? "")
            .Distinct()
            .GroupBy(getBaseMachineName)
            .Select(g => g.Key)
            .OrderBy(n => n)
            .ToList();

        var dates = Enumerable.Range(0, 7)
            .Select(i => WeekStart.AddDays(i).ToString("yyyy-MM-dd"))
            .ToList();

        var rows = dates.Select(date => new PlanningRow
        {
            Date = date,
            Cells = machines.Select(baseName => new PlanningCell
            {
                MachineName = baseName,
                Jobs = results
                    .Where(r => r.Machines.Any(m =>
                        getBaseMachineName(m.Name ?? "") == baseName &&
                        m.Planning.Any(p => p.Date == date)))
                    .SelectMany(r => r.Machines
                        .Where(m =>
                            getBaseMachineName(m.Name ?? "") == baseName &&
                            m.Planning.Any(p => p.Date == date))
                        .Select(m => new PlanningJob
                        {
                            OrderNumber = r.Number ?? "",
                            Company = r.Company ?? "",
                            Description = r.Description ?? "",
                            TotalHours = m.TotalHours ?? 0,
                            MachineName = m.Name ?? ""
                        }))
                    .ToList()
            }).ToList()
        }).ToList();

        MachineNames = machines;
        PlanningRows = rows;
    }

   
}