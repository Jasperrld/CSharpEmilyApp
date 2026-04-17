using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CSharpEmilyApp.Models;
using System.ComponentModel;
public enum OrderProgress
{
    [Description("Niet begonnen")]
    NietBegonnen,

    [Description("Mee bezig")]
    MeeBezig,

    [Description("Afgerond")]
    Afgerond,
}

public enum OrderPriority
{
    [Description("Laag")]
    Laag,
    [Description("Normaal")]
    Normaal,
    [Description("Hoog")]
    Hoog,
}
    
public class Job
{
    public string OrderDescription { get; set; } = "";
    public OrderProgress OrderProgress { get; set; } = OrderProgress.NietBegonnen;
    public string OrderNumber { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public int AmountProduced { get; set; } = 0;
    public string OrderComment { get; set; } = "";
    public int DurationMinutes { get; set; } = 0;
    public OrderPriority OrderPriority { get; set; } =  OrderPriority.Normaal; 
    public List<string> AssignedEmployees { get; set; } = new();
    
    // Formatted helpers
    public string DurationFormatted =>
        $"{DurationMinutes / 60:D2}:{DurationMinutes % 60:D2}";
    
}