using CSharpEmilyApp.Models;

namespace CSharpEmilyApp.ViewModels;

public class JobViewModel
{
    private readonly Job _job;
    
    public JobViewModel(Job job)
    {
        _job = job;
    }
    
    public string OrderNumber => _job.OrderNumber;
    public string OrderDescription => _job.OrderDescription;
    public OrderProgress OrderProgress => _job.OrderProgress;
    public string CustomerName => _job.CustomerName;
    public int AmountProduced => _job.AmountProduced;
    public OrderPriority OrderPriority => _job.OrderPriority;
    public string AssignedEmployees => string.Join(", ", _job.AssignedEmployees);
    public string DurationFormatted  => _job.DurationFormatted;

    public OrderProgress Status
    {
        get => _job.OrderProgress;
        set => _job.OrderProgress = value;
    }
    
}