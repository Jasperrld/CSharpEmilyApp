using System.Collections.Generic;

namespace CSharpEmilyApp.Models;

public class Machine
{
    public string Name { get; set; }
    public List<Job> Jobs { get; set; }
}